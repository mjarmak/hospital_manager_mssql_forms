using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_bl.Service
{
    public class AppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter(_unitOfWork);
        }

        public AppointmentResponse GetAppointment(long id)
        {
            return modelConverter.ResponseOf(_unitOfWork.Appointment.Get(id));
        }

        public List<AppointmentData> GetAppointments()
        {
            return _unitOfWork.Appointment.All().ToList();
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
            if (!HospitalExists(appointment.HospitalId))
            {
                throw new InvalidAppointment("Hospital with ID " + appointment.HospitalId + " does not exist.");
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

    }
}
