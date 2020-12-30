using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using System.Collections.Generic;

namespace hospital_manager_bl.Service
{
    public class RoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;
        private readonly SpecialityService specialityService;
        private readonly SpecialityLinkService specialityLinkService;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter();
            specialityService = new SpecialityService(_unitOfWork);
            specialityLinkService = new SpecialityLinkService(_unitOfWork);
        }

        public RoomResponse GetRoom(long id)
        {
            //var test = _unitOfWork.Room.GetRoom(id);

            var roomResponse = modelConverter.ResponseOf(_unitOfWork.Room.Get(id));
            roomResponse.Specialities = specialityService.GetSpecialitiesForRoom(id);
            return roomResponse;
        }

        public IEnumerable<RoomData> GetRooms()
        {
            return _unitOfWork.Room.All();
        }

        public RoomResponse SaveRoom(RoomRequest roomRequest)
        {
            if (!HospitalExists(roomRequest.HospitalId))
            {
                throw new InvalidRoom("Hospital with ID " + roomRequest.HospitalId + " does not exist.");
            }

            var roomData = modelConverter.EnvelopeOf(roomRequest);
            _unitOfWork.Room.Add(roomData);
            _unitOfWork.Save();

            var roomResponse = modelConverter.ResponseOf(_unitOfWork.Room.Get(roomData.Id));
            specialityLinkService.SaveSpecialitiesForRoom(roomData.Id, roomRequest.SpecialityIds);
            //roomResponse.Specialities = specialityLinkService.GetSpecialitiesForRoom(roomData.Id);
            return roomResponse;
        }

        private bool HospitalExists(long id)
        {
            return _unitOfWork.Hospital.Get(id) != null;
        }
    }
}
