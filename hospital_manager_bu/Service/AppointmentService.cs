using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;

namespace hospital_manager_bl.Service
{
    class AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public Appointment SaveAppointment(Appointment appointment)
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
            if (!HospitalExists(appointment.HospitalId))
            {
                throw new InvalidAppointment("Hospital with ID " + appointment.HospitalId + " does not exist.");
            }


            _unitOfWork.Appointment.Add(EnvelopeOf(appointment));

            _unitOfWork.Save();

            return appointment;
        }

        private bool RoomExists(long id)
        {
            return _unitOfWork.Room.Find(e => e.Id == id) != null;
        }
        private bool HospitalExists(long id)
        {
            return _unitOfWork.Hospital.Find(e => e.Id == id) != null;
        }

        public AppointmentData EnvelopeOf(Appointment appointment)
        {
            return new AppointmentData
            {
                Id = appointment.Id,
                PatientUsername = appointment.PatientUsername,
                DoctorUsername = appointment.DoctorUsername,
                RoomId = appointment.RoomId,
                HospitalId = appointment.HospitalId,
                Duration = appointment.Duration,
                Description = appointment.Description,
                From = appointment.From,
                To = appointment.To
            };
        }
    }
}
