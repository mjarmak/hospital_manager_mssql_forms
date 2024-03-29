﻿using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using hospital_manager_models.Util_Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_bl.Service
{
    public class AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;
        private readonly OAuthService oAuthService;
        private readonly EmailService emailService;
        public int halfDayHour = 13;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter(_unitOfWork);
            oAuthService = new OAuthService();
            emailService = new EmailService();
        }

        public AppointmentResponse GetAppointment(long id)
        {
            if (!AppointmentExists(id))
            {
                throw new NotFoundAppointment("Appointment with ID " + id + " doesn't exist.");
            }
            return modelConverter.ResponseOf(_unitOfWork.Appointment.Get(id));
        }
        public void DeleteAppointment(long id)
        {
            if (!AppointmentExists(id))
            {
                throw new NotFoundAppointment("Appointment with ID " + id + " doesn't exist.");
            }
            AppointmentData appointmentData = _unitOfWork.Appointment.Get(id);
            _unitOfWork.Appointment.Remove(appointmentData);
            _unitOfWork.Save();

            emailService.SendEmail(
                oAuthService.GetUserEmail(appointmentData.PatientUsername),
                "Appoitment Deleted with Doctor " + appointmentData.DoctorUsername,
                "At " + appointmentData.From + " to " + appointmentData.To + " in room " + _unitOfWork.Room.Get(appointmentData.RoomId).Name + " for " + appointmentData.Description);

        }

        public List<AppointmentResponse> GetAppointments()
        {
            return _unitOfWork.Appointment.All()?.Select(appointment => modelConverter.ResponseOf(appointment)).ToList();
        }

        public List<AppointmentResponse> GetAppointmentByDoctorUsername(string doctorUsername, DateTime from, DateTime to)
        {
            return _unitOfWork.Appointment.GetAppointmentByDoctorUsername(doctorUsername, from, to)?.Select(appointment => modelConverter.ResponseOf(appointment)).ToList();
        }

        public List<AppointmentResponse> GetAppointmentByPatientUsername(string patientUsername, DateTime from, DateTime to)
        {
            return _unitOfWork.Appointment.GetAppointmentByPatientUsername(patientUsername, from, to)?.Select(appointment => modelConverter.ResponseOf(appointment)).ToList();
        }

        public List<AppointmentResponse> GetAppointmentsByHospitalAndSpeciality(int hospitalId, int specialityId, DateTime dateFrom, DateTime dateTo)
        {
            return _unitOfWork.Appointment.GetAppointmentsByHospitalAndSpeciality(hospitalId, specialityId, dateFrom, dateTo)?.Select(appointment => modelConverter.ResponseOf(appointment)).ToList();
        }

        public List<AppointmentResponse> GetAppointmentsByHospital(int hospitalId, DateTime dateFrom, DateTime dateTo)
        {
            return _unitOfWork.Appointment.GetAppointmentsByHospital(hospitalId, dateFrom, dateTo)?.Select(appointment => modelConverter.ResponseOf(appointment)).ToList();
        }

        public List<AppointmentRequest> GetAppointmentSuggestions(int hospitalId, int specialityId, DateTime dateFrom, DateTime dateTo)
        {
            if (!HospitalExists(hospitalId))
            {
                throw new NotFoundHospital("Hospital with ID " + hospitalId + " doesn't exist.");
            }
            if (!SpecialityExists(specialityId))
            {
                throw new NotFoundSpeciality("Speciality with ID " + specialityId + " doesn't exist.");
            }
            SpecialityData speciality = _unitOfWork.Speciality.Get(specialityId);
            List<AppointmentRequest> appointmentSuggestions = new List<AppointmentRequest>();
            List<RoomFromTo> roomFromTosFree = GetFreeRooms(hospitalId, specialityId, dateFrom, dateTo);
            List<DoctorData> doctors = _unitOfWork.Doctor.GetDoctorsByConsultationHospitalIdAndSpecialityId(hospitalId, specialityId);
            foreach(RoomFromTo roomFromTo in roomFromTosFree)
            {
                double availabeMinutes = (roomFromTo.To - roomFromTo.From).TotalMinutes;
                List<DoctorData> availableDoctors = doctors.Where(doctor => doctor.Consultations.Any(consultation => consultation.Duration <= availabeMinutes)).ToList();
                foreach (DoctorData availableDoctor in availableDoctors)
                {
                    foreach (ConsultationData availableConsultations in availableDoctor.Consultations.Where(consultation => consultation.HospitalId == hospitalId && consultation.SpecialityId == specialityId))
                    {
                        appointmentSuggestions.Add(new AppointmentRequest { RoomId = roomFromTo.RoomId, Description = speciality.Name, DoctorUsername = availableDoctor.Username, From = roomFromTo.From, To = roomFromTo.From.AddMinutes(availableConsultations.Duration) });
                    }
                }
            }
            return appointmentSuggestions;
        }
        public List<DateTime> GetFreeDays(int hospitalId, int specialityId, DateTime dateFrom, DateTime dateTo)
        {
            if (!HospitalExists(hospitalId))
            {
                throw new InvalidAppointment("Hospital with ID " + hospitalId + " doesn't exist.");
            }
            if (!SpecialityExists(specialityId))
            {
                throw new InvalidAppointment("Speciality with ID " + specialityId + " doesn't exist.");
            }
            List<AppointmentRequest> appointmentSuggestions = GetAppointmentSuggestions(hospitalId, specialityId, dateFrom, dateTo);
            List<DateTime> availableDays = new List<DateTime>();
            foreach(AppointmentRequest appointmentSuggestion in appointmentSuggestions)
            {
                DateTime day_temp = appointmentSuggestion.From;
                if (!availableDays.Any(day => day.DayOfYear == day_temp.DayOfYear))
                {
                    DateTime availableDay = new DateTime(day_temp.Year, day_temp.Month, day_temp.Day, 0, 0, 0);
                    availableDays.Add(availableDay);
                }
            }

            return availableDays;
        }

        public List<RoomFromTo> GetFreeRooms(int hospitalId, int specialityId, DateTime dateFrom, DateTime dateTo)
        {
            if (!HospitalExists(hospitalId))
            {
                throw new InvalidAppointment("Hospital with ID " + hospitalId + " doesn't exist.");
            }
            if (!SpecialityExists(specialityId))
            {
                throw new InvalidAppointment("Speciality with ID " + specialityId + " doesn't exist.");
            }
            HospitalData hospital = _unitOfWork.Hospital.GetHospital(hospitalId);
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = dateFrom; date <= dateTo; date = date.AddDays(1))
            {
                if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == date.DayOfWeek.ToString().ToUpper()).Closed == false) {
                    allDates.Add(date);
                }
            }

            List<AppointmentResponse> appointmentsTaken = _unitOfWork.Appointment.GetAppointmentsByHospitalAndSpeciality(hospitalId, specialityId, dateFrom, dateTo)?.Select(appointment => modelConverter.ResponseOf(appointment)).ToList();

            List<RoomData> rooms = _unitOfWork.Room.GetRoomsByHospitalIdAndSpecialityId(hospitalId, specialityId);

            List<RoomFromTo> roomFromTosTaken = appointmentsTaken?.Select(
                appointment => new RoomFromTo { RoomId = appointment.RoomId, From = appointment.From, To = appointment.To }).ToList();

            List<RoomFromTo> roomFromTosFree = new List<RoomFromTo>();

            rooms.ForEach(room => {
                allDates.ForEach(date =>
                {
                    List<RoomFromTo> roomFromTosTakenTemp = roomFromTosTaken.Where(roomFromToTaken => roomFromToTaken.RoomId == room.Id && roomFromToTaken.From.DayOfWeek == date.DayOfWeek).OrderBy(roomFromToTaken => roomFromToTaken.From).ToList();
                    DateTime fromSet = new DateTime();
                    DateTime toSet = new DateTime();
                    long roomIdTemp = 0;
                    OpeningHoursData hospitalDay = hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == date.DayOfWeek.ToString().ToUpper());
                    if (roomFromTosTakenTemp.Count != 0)
                    {
                        roomFromTosTakenTemp.ForEach(roomFromToTakenTemp =>
                        {
                            if (roomFromToTakenTemp.From.Hour > hospitalDay.HourFrom && roomFromToTakenTemp.From.Minute > hospitalDay.MinuteFrom && roomFromTosFree.Count == 0)
                            {
                                fromSet = new DateTime(date.Year, date.Month, date.Day, hospitalDay.HourFrom, hospitalDay.MinuteFrom, 0);
                                toSet = new DateTime(date.Year, date.Month, date.Day, roomFromToTakenTemp.From.Hour, roomFromToTakenTemp.From.Minute, 0);
                            }
                            else
                            {
                                toSet = new DateTime(date.Year, date.Month, date.Day, roomFromToTakenTemp.From.Hour, roomFromToTakenTemp.From.Minute, 0);
                            }
                            if (fromSet != DateTime.MinValue && toSet != DateTime.MinValue)
                            {
                                roomFromTosFree.Add(new RoomFromTo { RoomId = roomFromToTakenTemp.RoomId, From = fromSet, To = toSet });
                                fromSet = new DateTime();
                                toSet = new DateTime();
                            }
                            fromSet = new DateTime(date.Year, date.Month, date.Day, roomFromToTakenTemp.To.Hour, roomFromToTakenTemp.To.Minute, 0);
                            roomIdTemp = roomFromToTakenTemp.RoomId;
                        });
                        toSet = new DateTime(date.Year, date.Month, date.Day, hospitalDay.HourTo, hospitalDay.MinuteTo, 0);
                        if (fromSet != DateTime.MinValue && toSet != DateTime.MinValue && roomIdTemp > 0)
                        {
                            roomFromTosFree.Add(new RoomFromTo { RoomId = roomIdTemp, From = fromSet, To = toSet });
                            fromSet = new DateTime();
                            toSet = new DateTime();
                        }
                    } else
                    {
                        fromSet = new DateTime(date.Year, date.Month, date.Day, hospitalDay.HourFrom, hospitalDay.MinuteFrom, 0);
                        toSet = new DateTime(date.Year, date.Month, date.Day, hospitalDay.HourTo, hospitalDay.MinuteTo, 0);
                        roomFromTosFree.Add(new RoomFromTo { RoomId = room.Id, From = fromSet, To = toSet });
                    }
                });
                });

            return roomFromTosFree;
        }

        public AppointmentResponse SaveAppointment(AppointmentRequest appointment)
        {
            if (appointment == null)
            {
                throw new InvalidAppointment("Appointment is null.");
            }
            if (appointment.Id > 0)
            {
                throw new InvalidAppointment("Appointment Id should be 0 on creation.");
            }
            if (AppointmentTaken(appointment.RoomId, appointment.From, appointment.To))
            {
                throw new InvalidAppointment("An appointment is already taken in room " + appointment.RoomId + " is already taken.");
            }
            if (!RoomExists(appointment.RoomId))
            {
                throw new NotFoundRoom("Room with ID " + appointment.RoomId + " does not exist.");
            }
            if (!AppointmentHalfDayValid(appointment.DoctorUsername, appointment.RoomId, appointment.From, appointment.To))
            {
                throw new InvalidAppointment("2 appointment of different hospitals cannot be taken in the same half day.");
            }
            if (!oAuthService.UserExists(appointment.DoctorUsername))
            {
                throw new NotFoundUser("Doctor with username " + appointment.DoctorUsername + " does not exist.");
            }
            else
            {
                emailService.SendEmail(
                    oAuthService.GetUserEmail(appointment.DoctorUsername),
                   "Appointment Request for " + appointment.PatientUsername,
                    "At " + appointment.From + " to " + appointment.To + " in room " + _unitOfWork.Room.Get(appointment.RoomId).Name + " for " + appointment.Description);
            }
            if (!oAuthService.UserExists(appointment.PatientUsername))
            {
                throw new NotFoundUser("Patient with username " + appointment.PatientUsername + " does not exist.");
            }
            else
            {
                emailService.SendEmail(
                    oAuthService.GetUserEmail(appointment.PatientUsername),
                    "Appoitment Request with Doctor " + appointment.DoctorUsername,
                    "At " + appointment.From + " to " + appointment.To + " in room " + _unitOfWork.Room.Get(appointment.RoomId).Name + " for " + appointment.Description);
            }
            appointment.Status = "PENDING";
            var appointmentData = modelConverter.EnvelopeOf(appointment);
            _unitOfWork.Appointment.Add(appointmentData);
            _unitOfWork.Save();

            return modelConverter.ResponseOf(_unitOfWork.Appointment.Get(appointmentData.Id));
        }

        public AppointmentResponse UpdateAppointment(AppointmentRequest appointment)
        {
            if (appointment == null)
            {
                throw new InvalidAppointment("Appointment is null.");
            }
            if (appointment.Id == 0)
            {
                throw new InvalidAppointment("Appointment Id should not be 0 on creation.");
            }
            if (!AppointmentExists(appointment.Id))
            {
                throw new NotFoundAppointment("Appointment with ID " + appointment.Id + " doesn't exist.");
            }
            if (appointment.Status != "PENDING")
            {
                throw new InvalidAppointment("Appointment status can only be PENDING.");
            }
            if (_unitOfWork.Appointment.GetAppointmentByRoomIdAndTimeExclusive(appointment.RoomId, appointment.From, appointment.To).Id != appointment.Id)
            {
                throw new InvalidAppointment("An appointment is already taken in room " + appointment.RoomId + " is already taken.");
            }
            if (!RoomExists(appointment.RoomId))
            {
                throw new NotFoundRoom("Room with ID " + appointment.RoomId + " does not exist.");
            }
            if (!oAuthService.UserExists(appointment.DoctorUsername))
            {
                throw new NotFoundUser("Doctor with username " + appointment.DoctorUsername + " does not exist.");
            }
            else
            {
                emailService.SendEmail(
                    oAuthService.GetUserEmail(appointment.DoctorUsername),
                   "Appoitment Request for " + appointment.PatientUsername,
                    "At " + appointment.From + " to " + appointment.To + " in room " + _unitOfWork.Room.Get(appointment.RoomId).Name + " for " + appointment.Description);
            }
            if (!oAuthService.UserExists(appointment.PatientUsername))
            {
                throw new NotFoundUser("Patient with username " + appointment.PatientUsername + " does not exist.");
            }
            else
            {
                emailService.SendEmail(
                    oAuthService.GetUserEmail(appointment.PatientUsername),
                    "Appoitment Request with Doctor " + appointment.DoctorUsername,
                    "At " + appointment.From + " to " + appointment.To + " in room " + _unitOfWork.Room.Get(appointment.RoomId).Name + " for " + appointment.Description);
            }
            if (!AppointmentHalfDayValid(appointment.DoctorUsername, appointment.RoomId, appointment.From, appointment.To))
            {
                throw new InvalidAppointment("2 appointment of different hospitals cannot be taken in the same half day.");
            }
            var appointmentData = modelConverter.EnvelopeOf(appointment);
            _unitOfWork.Appointment.Update(appointmentData);
            _unitOfWork.Save();

            return modelConverter.ResponseOf(_unitOfWork.Appointment.Get(appointmentData.Id));
        }

        public AppointmentResponse ConfirmAppointment(long id)
        {
            if (id == 0)
            {
                throw new InvalidAppointment("Appointment Id should not be 0.");
            }
            var appointmentData = _unitOfWork.Appointment.Get(id);
            if (appointmentData.Status != "PENDING")
            {
                throw new InvalidAppointment("Appointment status must be PENDING.");
            }
            appointmentData.Status = "CONFIRMED";
            _unitOfWork.Appointment.Update(appointmentData);
            _unitOfWork.Save();

            emailService.SendEmail(
                oAuthService.GetUserEmail(appointmentData.PatientUsername),
                "Appoitment Confirmed with Doctor " + appointmentData.DoctorUsername,
                "At " + appointmentData.From + " to " + appointmentData.To + " in room " + _unitOfWork.Room.Get(appointmentData.RoomId).Name + " for " + appointmentData.Description);


            return modelConverter.ResponseOf(_unitOfWork.Appointment.Get(appointmentData.Id));
        }

        private bool AppointmentExists(long id)
        {
            return _unitOfWork.Appointment.Get(id) != null;
        }
        public bool AppointmentHalfDayValid(string doctorUsername, long roomId, DateTime from, DateTime to)
        { 
            HospitalData hospitalData = _unitOfWork.Hospital.GetHospitalByRoomId(roomId);
            OpeningHoursData openingHoursData = hospitalData.OpeningHours.Single(openingHour => openingHour.Day == from.DayOfWeek.ToString().ToUpper());
            DateTime fromTemp;
            DateTime toTemp;
            if (from.Hour < 13)
            {
                fromTemp = new DateTime(from.Year, from.Month, from.Day, openingHoursData.HourFrom, openingHoursData.MinuteFrom, 0);
                toTemp = new DateTime(from.Year, from.Month, from.Day, halfDayHour, 0, 0);
            } else
            {
                fromTemp = new DateTime(from.Year, from.Month, from.Day, halfDayHour, 0, 0);
                toTemp = new DateTime(from.Year, from.Month, from.Day, openingHoursData.HourTo, openingHoursData.MinuteTo, 0);
            }
            List<AppointmentData> appointmentPreceding = _unitOfWork.Appointment.GetAppointmentsByNotHospitalAndDoctorUsername(doctorUsername, hospitalData.Id, fromTemp, toTemp);
            return appointmentPreceding.Count == 0;
        }
        public bool AppointmentTaken(long roomId, DateTime from, DateTime to)
        {
            return _unitOfWork.Appointment.GetAppointmentByRoomIdAndTimeExclusive(roomId, from, to) != null;
        }
        private bool RoomExists(long id)
        {
            return _unitOfWork.Room.Get(id) != null;
        }
        private bool HospitalExists(long id)
        {
            return _unitOfWork.Hospital.Get(id) != null;
        }
        private bool SpecialityExists(long id)
        {
            return _unitOfWork.Speciality.Get(id) != null;
        }

    }
}
