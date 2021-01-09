using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_bl.Service
{
    public class HospitalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;
        private readonly RoomService roomService;

        public HospitalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter(_unitOfWork);
            roomService = new RoomService(_unitOfWork);
        }

        public HospitalResponse GetHospital(long id)
        {
            return modelConverter.ResponseOf(_unitOfWork.Hospital.GetHospital(id));
        }

        public List<HospitalResponse> GetHospitals()
        {
            return _unitOfWork.Hospital.GetHospitals()?.Select(hospital => modelConverter.ResponseOf(hospital)).ToList();
        }
        public List<HospitalResponse> GetHospitalsBySpecialityId(long specialityId)
        {
            return _unitOfWork.Hospital.GetHospitalsBySpecialityId(specialityId)?.Select(hospital => modelConverter.ResponseOf(hospital)).ToList();
        }

        public HospitalResponse SaveHospital(HospitalRequest hospital)
        {
            var hospitalData = modelConverter.EnvelopeOf(hospital);
            _unitOfWork.Hospital.Add(hospitalData);
            _unitOfWork.Save();

            if (hospital.Rooms != null)
            {
                foreach (RoomRequest room in hospital.Rooms)
                    room.HospitalId = hospitalData.Id;
                roomService.SaveRooms(hospital.Rooms);
            }

            return modelConverter.ResponseOf(_unitOfWork.Hospital.GetHospital(hospitalData.Id));
        }

        public void SaveHospital(HospitalRequest hospitalRequest, AddressRequest addressRequest, object p)
        {
            throw new NotImplementedException();
        }
    }
}
