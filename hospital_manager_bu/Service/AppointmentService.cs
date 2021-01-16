using hospital_manager_bl.Util;
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

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter(_unitOfWork);
            oAuthService = new OAuthService();
        }

        public AppointmentResponse GetAppointment(long id)
        {
            return modelConverter.ResponseOf(_unitOfWork.Appointment.Get(id));
        }

        public List<AppointmentResponse> GetAppointments()
        {
            return _unitOfWork.Appointment.All()?.Select(appointment => modelConverter.ResponseOf(appointment)).ToList();
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
                throw new InvalidAppointment("Hospital with ID " + hospitalId + " doesn't exist.");
            }
            if (!SpecialityExists(specialityId))
            {
                throw new InvalidAppointment("Speciality with ID " + specialityId + " doesn't exist.");
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
            if (!RoomExists(appointment.RoomId))
            {
                throw new InvalidAppointment("Room with ID " + appointment.RoomId + " does not exist.");
            }
            if (!oAuthService.UserExists(appointment.DoctorUsername))
            {
                throw new InvalidAppointment("Doctor with username " + appointment.DoctorUsername + " does not exist.");
            }
            if (!oAuthService.UserExists(appointment.PatientUsername))
            {
                throw new InvalidAppointment("Patient with username " + appointment.PatientUsername + " does not exist.");
            }


            var appointmentData = modelConverter.EnvelopeOf(appointment);
            _unitOfWork.Appointment.Add(appointmentData);
            _unitOfWork.Save();

            return modelConverter.ResponseOf(_unitOfWork.Appointment.Get(appointmentData.Id));
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
