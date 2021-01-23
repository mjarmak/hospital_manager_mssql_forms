using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_models.Models;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_bl.Util
{
    public class ModelConverter
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModelConverter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AppointmentData EnvelopeOf(AppointmentRequest appointment)
        {
            return new AppointmentData
            {
                Id = appointment.Id,
                PatientUsername = appointment.PatientUsername,
                DoctorUsername = appointment.DoctorUsername,
                RoomId = appointment.RoomId,
                Description = appointment.Description,
                From = appointment.From,
                To = appointment.To
            };
        }
        public DoctorData EnvelopeOf(DoctorRequest doctorRequest)
        {
            return new DoctorData
            {
                Username = doctorRequest.Username,
                Name = doctorRequest.Name,
                Consultations = EnvelopeOf(doctorRequest.Consultations),
                Specialities = EnvelopeOfSpecialityToDoctor(doctorRequest.SpecialityIds)
            };
        }
        public DoctorResponse ResponseOf(DoctorData doctorData)
        {
            var doctorResponse = new DoctorResponse
            {
                Username = doctorData.Username,
                Name = doctorData.Name,
                Consultations = ResponseOf(doctorData.Consultations)
            };
            doctorResponse.Specialities = ResponseOf(
                _unitOfWork.Speciality.GetSpecialities(doctorData.Specialities?.Select(speciality => speciality.SpecialityId).ToList())
                );
            return doctorResponse;
        }
        public AppointmentResponse ResponseOf(AppointmentData appointment)
        {
            var appointmentResponse = new AppointmentResponse
            {
                Id = appointment.Id,
                PatientUsername = appointment.PatientUsername,
                DoctorUsername = appointment.DoctorUsername,
                Doctor = ResponseOf(_unitOfWork.Doctor.GetDoctorSimple(appointment.DoctorUsername)),
                RoomId = appointment.RoomId,
                Room = ResponseOf(_unitOfWork.Room.Get(appointment.RoomId)),
                Description = appointment.Description,
                From = appointment.From,
                To = appointment.To
            };
            return appointmentResponse;
        }
        public HospitalResponse ResponseOf(HospitalData hospitalData)
        {
            var hospitalResponse = new HospitalResponse
            {
                Id = hospitalData.Id,
                Name = hospitalData.Name,
                Address = ResponseOf(hospitalData.Address),
                OpeningHours = ResponseOf(hospitalData.OpeningHours),
                Rooms = ResponseOf(_unitOfWork.Room.GetRoomsByHospitalId(hospitalData.Id))

            };
            return hospitalResponse;
        }
        public AddressResponse ResponseOf(AddressData addressData)
        {
            return new AddressResponse
            {
                Id = addressData.Id,
                Street = addressData.Street,
                City = addressData.City,
                Country = addressData.Country,
                PostalCode = addressData.PostalCode,
                BoxNumber = addressData.BoxNumber

            };
        }
        public List<OpeningHoursResponse> ResponseOf(List<OpeningHoursData> openingHoursData)
        {
            return openingHoursData?.Select(openingHours => new OpeningHoursResponse()
            {
                Id = openingHours.Id,
                Day = openingHours.Day,
                Closed = openingHours.Closed,
                HourFrom = openingHours.HourFrom,
                MinuteFrom = openingHours.MinuteFrom,
                HourTo = openingHours.HourTo,
                MinuteTo = openingHours.MinuteTo
            }).ToList();
        }
        public ConsultationData EnvelopeOf(ConsultationRequest consultationRequest)
        {
            return new ConsultationData
            {
                Id = consultationRequest.Id,
                HospitalId = consultationRequest.HospitalId,
                SpecialityId = consultationRequest.SpecialityId,
                Duration = consultationRequest.Duration
            };
        }
        public List<ConsultationData> EnvelopeOf(List<ConsultationRequest> consultationRequest)
        {
            return consultationRequest?.Select(consultation => EnvelopeOf(consultation)).ToList();
        }
        public ConsultationResponse ResponseOf(ConsultationData consultationData)
        {
            HospitalResponse hospital = ResponseOf(_unitOfWork.Hospital.GetHospital(consultationData.HospitalId));
            SpecialityResponse speciality = ResponseOf(_unitOfWork.Speciality.Get(consultationData.SpecialityId));
            return new ConsultationResponse
            {
                Id = consultationData.Id,
                Hospital = hospital,
                Speciality = speciality,
                Duration = consultationData.Duration
            };
        }
        public List<ConsultationResponse> ResponseOf(List<ConsultationData> consultationData)
        {
            return consultationData?.Select(consultation => ResponseOf(consultation)).ToList();
        }
        public RoomData EnvelopeOf(RoomRequest roomRequest)
        {
            return new RoomData
            {
                Id = roomRequest.Id,
                HospitalId = roomRequest.HospitalId,
                Name = roomRequest.Name,
                Specialities = EnvelopeOfSpecialityToRoom(roomRequest.SpecialityIds)
            };
        }
        public RoomResponse ResponseOf(RoomData roomData)
        {
            var roomResponse = new RoomResponse
            {
                Id = roomData.Id,
                Name = roomData.Name,
                HospitalId = roomData.HospitalId
            };
            roomResponse.Specialities = ResponseOf(
                _unitOfWork.Speciality.GetSpecialities(roomData.Specialities?.Select(speciality => speciality.SpecialityId).ToList())
                );
            return roomResponse;
        }
        public List<RoomResponse> ResponseOf(List<RoomData> roomDatas)
        {
            return roomDatas?.Select(roomData => ResponseOf(roomData)).ToList();
        }

        public SpecialityResponse ResponseOf(SpecialityData speciality)
        {
            return new SpecialityResponse
            {
                Id = speciality.Id,
                Name = speciality.Name
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
                }).ToList()
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
        public List<SpecialityResponse> ResponseOf(List<SpecialityData> specialities)
        {
            return specialities?.Select(speciality => new SpecialityResponse()
            {
                Id = speciality.Id,
                Name = speciality.Name
            }).ToList();
        }
        public List<SpecialityToDoctorData> EnvelopeOfSpecialityToDoctor(List<long> specialityIds)
        {
            return specialityIds?.Select(specialityId => new SpecialityToDoctorData()
            {
                SpecialityId = specialityId
            }).ToList();
        }
        public List<SpecialityToRoomData> EnvelopeOfSpecialityToRoom(List<long> specialityIds)
        {
            return specialityIds?.Select(specialityId => new SpecialityToRoomData()
            {
                SpecialityId = specialityId
            }).ToList();
        }
    }
}
