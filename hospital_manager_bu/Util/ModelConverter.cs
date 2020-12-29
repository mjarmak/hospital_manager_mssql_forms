using hospital_manager_data_access.Entities;
using hospital_manager_models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hospital_manager_bl.Util
{
    public class ModelConverter
    {
        public AppointmentData EnvelopeOf(AppointmentRequest appointment)
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
        public RoomData EnvelopeOf(RoomRequest room)
        {
            return new RoomData
            {
                Id = room.Id,
                Name = room.Name
            };
        }
        public HospitalData EnvelopeOf(HospitalRequest hospital)
        {
            return new HospitalData
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = EnvelopeOf(hospital.Address),
                OpeningHours = hospital.OpeningHours?.Select(openingHours => new OpeningHoursData()
                {
                    Id = openingHours.Id,
                    Day = openingHours.Day == null ? null : openingHours.Day,
                    HourFrom = openingHours.HourFrom,
                    HourTo = openingHours.HourTo,
                    MinuteFrom = openingHours.MinuteFrom,
                    MinuteTo = openingHours.MinuteTo,
                    Closed = openingHours.Closed
                }).ToList(),
            };
        }
        public SpecialityData EnvelopeOf(SpecialityRequest speciality)
        {
            return new SpecialityData
            {
                Id = speciality.Id,
                Name = speciality.Name
            };
        }
        public AddressData EnvelopeOf(AddressRequest address)
        {
            return new AddressData
            {
                Id = address.Id,
                PostalCode = address.PostalCode,
                City = address.City,
                Country = address.Country,
                Street = address.Street,
                BoxNumber = address.BoxNumber
            };
        }
    }
}
