using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_bl.Service
{
    public class SpecialityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;

        public SpecialityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter();
        }

        public List<SpecialityResponse> GetSpecialitiesForDoctor(string doctorUsername)
        {
            List<SpecialityToDoctorData> specialities = _unitOfWork.SpecialityToDoctor.Find(e => e.DoctorUsername == doctorUsername).ToList();
            List<SpecialityData> specialityData = new List<SpecialityData>();
            specialities.ForEach(speciality =>
                specialityData.Add(_unitOfWork.Speciality.Get(speciality.Id))
            );
            return specialityData?.Select(speciality => modelConverter.ResponseOf(speciality)).ToList();
        }
        public List<SpecialityResponse> GetSpecialitiesForRoom(long roomId)
        {
            List<SpecialityToRoomData> specialities = _unitOfWork.SpecialityToRoom.Find(e => e.RoomId == roomId).ToList();
            List<SpecialityData> specialityData = new List<SpecialityData>();
            specialities.ForEach(speciality =>
                specialityData.Add(_unitOfWork.Speciality.Get(speciality.Id))
            );
            return specialityData?.Select(speciality => modelConverter.ResponseOf(speciality)).ToList();
        }
        //public void SaveSpecialitiesForRoom(long roomId, List<SpecialityRequest> specialityRequests)
        //{
        //    specialityRequests.ForEach(speciality =>
        //        specialityData.Add(_unitOfWork.Speciality.Add(speciality.Id))
        //    );
        //}

        public IEnumerable<SpecialityResponse> GetSpecialities()
        {
            return _unitOfWork.Speciality.All().ToList()?.Select(speciality => new SpecialityResponse()
            {
                Id = speciality.Id,
                Name = speciality.Name
            }).ToList();
        }

        public SpecialityData SaveSpeciality(SpecialityRequest specialityRequest)
        {
            var specialityData = modelConverter.EnvelopeOf(specialityRequest);
            _unitOfWork.Speciality.Add(specialityData);
            _unitOfWork.Save();

            return _unitOfWork.Speciality.Get(specialityData.Id);
        }

    }
}
