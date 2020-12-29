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

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter();
        }

        public RoomData GetRoom(long id)
        {
            return _unitOfWork.Room.Get(id);
        }

        public IEnumerable<RoomData> GetRooms()
        {
            return _unitOfWork.Room.All();
        }

        public RoomData SaveRoom(RoomRequest room)
        {
            if (!HospitalExists(room.HospitalId))
            {
                throw new InvalidRoom("Hospital with ID " + room.HospitalId + " does not exist.");
            }
            var roomData = modelConverter.EnvelopeOf(room);
            _unitOfWork.Room.Add(roomData);
            _unitOfWork.Save();

            return _unitOfWork.Room.Get(roomData.Id);
        }

        private bool HospitalExists(long id)
        {
            return _unitOfWork.Hospital.Get(id) != null;
        }
    }
}
