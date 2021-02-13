using hospital_manager_bl.Util;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Models;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_bl.Service
{
    public class RoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ModelConverter modelConverter;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            modelConverter = new ModelConverter(_unitOfWork);
        }

        public RoomResponse GetRoom(long id)
        {
            RoomData roomData = _unitOfWork.Room.GetRoom(id);
            if (roomData == null)
            {
                throw new NotFoundRoom("Room with ID " + id + " does not exist.");
            }
            return modelConverter.ResponseOf(roomData);
        }

        public List<RoomResponse> GetRooms()
        {
            List<RoomData> roomData = _unitOfWork.Room.GetRooms();
            return roomData?.Select(room => modelConverter.ResponseOf(room)).ToList();
        }
        public List<RoomResponse> GetRoomsByHospitalId(long hospitalId)
        {
            List<RoomData> roomData = _unitOfWork.Room.GetRoomsByHospitalId(hospitalId);
            return roomData?.Select(room => modelConverter.ResponseOf(room)).ToList();
        }

        public List<RoomResponse> GetRoomsByHospitalIdAndSpecialityId(long hospitalId, long specialityId)
        {
            List<RoomData> roomData = _unitOfWork.Room.GetRoomsByHospitalIdAndSpecialityId(hospitalId, specialityId);
            return roomData?.Select(room => modelConverter.ResponseOf(room)).ToList();
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

            var roomResponse = modelConverter.ResponseOf(_unitOfWork.Room.GetRoom(roomData.Id));
            return roomResponse;
        }

        public List<RoomResponse> SaveRooms(List<RoomRequest> roomRequests)
        {
            List<RoomResponse> roomResponses = new List<RoomResponse>();
            roomRequests.ForEach(roomRequest =>
            {
                roomResponses.Add(SaveRoom(roomRequest));
            });
            return roomResponses;
        }

        private bool HospitalExists(long id)
        {
            return _unitOfWork.Hospital.Get(id) != null;
        }
    }
}
