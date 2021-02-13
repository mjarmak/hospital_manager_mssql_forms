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
            HospitalData hospitalData = _unitOfWork.Hospital.GetHospital(id);
            if (hospitalData == null)
            {
                throw new NotFoundHospital("Hospital with ID " + id + " does not exist.");
            }
            return modelConverter.ResponseOf(hospitalData);
        }

        public List<HospitalResponse> GetHospitals()
        {
            return _unitOfWork.Hospital.GetHospitals()?.Select(hospital => modelConverter.ResponseOf(hospital)).ToList();
        }
        public List<HospitalResponse> GetHospitalsBySpecialityId(long specialityId)
        {
            return _unitOfWork.Hospital.GetHospitalsBySpecialityId(specialityId)?.Select(hospital => modelConverter.ResponseOf(hospital)).ToList();
        }
        public HospitalResponse GetHospitalsByRoomId(long roomId)
        {
            return modelConverter.ResponseOf(_unitOfWork.Hospital.GetHospitalByRoomId(roomId));
        }


        public HospitalResponse SaveHospital(HospitalRequest hospital)
        {
            if (hospital == null)
            {
                throw new InvalidHospital("Hospital is null.");
            }
            if (hospital.Id > 0)
            {
                throw new InvalidHospital("Hospital Id should be 0 on creation.");
            }
            if (hospital.Address.Id > 0)
            {
                throw new InvalidHospital("Hospital address Id should be 0 on creation.");
            }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "MONDAY") == null) { throw new InvalidHospital("Monday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "TUESDAY") == null) { throw new InvalidHospital("Tuesday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "WEDNESDAY") == null) { throw new InvalidHospital("Wednesday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "THURSDAY") == null) { throw new InvalidHospital("Thursday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "FRIDAY") == null) { throw new InvalidHospital("Friday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "SATURDAY") == null) { throw new InvalidHospital("Saturday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "SUNDAY") == null) { throw new InvalidHospital("Sunday opening hours are missing."); }

            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "MONDAY").Id != 0) { throw new InvalidHospital("Monday opening hours ID should be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "TUESDAY").Id != 0) { throw new InvalidHospital("Tuesday opening hours ID should be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "WEDNESDAY").Id != 0) { throw new InvalidHospital("Wednesday opening hours ID should be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "THURSDAY").Id != 0) { throw new InvalidHospital("Thursday opening hours ID should be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "FRIDAY").Id != 0) { throw new InvalidHospital("Friday opening hours ID should be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "SATURDAY").Id != 0) { throw new InvalidHospital("Saturday opening hours ID should be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "SUNDAY").Id != 0) { throw new InvalidHospital("Sunday opening hours ID should be 0."); }


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
        public HospitalResponse UpdateHospital(HospitalRequest hospital)
        {
            if (hospital == null)
            {
                throw new InvalidHospital("Hospital is null.");
            }
            if (hospital.Id == 0)
            {
                throw new InvalidHospital("Hospital Id should not be 0 on creation.");
            }
            if (hospital.Address.Id == 0)
            {
                throw new InvalidHospital("Hospital address Id should not be 0 on creation.");
            }
            if (!HospitalExists(hospital.Id))
            {
                throw new NotFoundHospital("Hospital with ID " + hospital.Id + " doesn't exist.");
            }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "MONDAY") == null) { throw new InvalidHospital("Monday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "TUESDAY") == null) { throw new InvalidHospital("Tuesday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "WEDNESDAY") == null) { throw new InvalidHospital("Wednesday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "THURSDAY") == null) { throw new InvalidHospital("Thursday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "FRIDAY") == null) { throw new InvalidHospital("Friday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "SATURDAY") == null) { throw new InvalidHospital("Saturday opening hours are missing."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "SUNDAY") == null) { throw new InvalidHospital("Sunday opening hours are missing."); }

            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "MONDAY").Id == 0) { throw new InvalidHospital("Monday opening hours ID should not be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "TUESDAY").Id == 0) { throw new InvalidHospital("Tuesday opening hours ID should not be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "WEDNESDAY").Id == 0) { throw new InvalidHospital("Wednesday opening hours ID should not be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "THURSDAY").Id == 0) { throw new InvalidHospital("Thursday opening hours ID should not be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "FRIDAY").Id == 0) { throw new InvalidHospital("Friday opening hours ID should not be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "SATURDAY").Id == 0) { throw new InvalidHospital("Saturday opening hours ID should not be 0."); }
            if (hospital.OpeningHours.SingleOrDefault(openingHour => openingHour.Day == "SUNDAY").Id == 0) { throw new InvalidHospital("Sunday opening hours ID should not be 0."); }


            HospitalData hospitalData = modelConverter.EnvelopeOf(hospital);
            _unitOfWork.Hospital.UpdateHospital(hospitalData);
            _unitOfWork.Save();
            return modelConverter.ResponseOf(_unitOfWork.Hospital.GetHospital(hospitalData.Id));
        }
        public HospitalResponse UpdateHospitalRooms(long hospitalId, List<RoomRequest> rooms)
        {
            if (hospitalId == 0)
            {
                throw new InvalidHospital("Hospital ID is invalid.");
            }
            if (!HospitalExists(hospitalId))
            {
                throw new NotFoundHospital("Hospital with ID " + hospitalId + " doesn't exist.");
            }
            foreach (RoomRequest roomRequest in rooms)
            {
                RoomData roomData = modelConverter.EnvelopeOf(roomRequest);
                if (!RoomExists(hospitalId, roomData.Name))
                {
                    roomData.HospitalId = hospitalId;
                    _unitOfWork.Room.Add(roomData);
                }
            }
            _unitOfWork.Save();
            return modelConverter.ResponseOf(_unitOfWork.Hospital.GetHospital(hospitalId));
        }
        public void DeleteHospitalRoom(long roomId)
        {
            if (roomId == 0)
            {
                throw new InvalidRoom("Room ID is invalid.");
            }
            if (!RoomExists(roomId))
            {
                throw new InvalidRoom("Room with ID " + roomId + ".");
            }
            RoomData roomData = _unitOfWork.Room.Get(roomId);
            _unitOfWork.Room.Remove(roomData);
            _unitOfWork.Save();
        }
        private bool RoomExists(long roomId)
        {
            return _unitOfWork.Room.GetRoomSimple(roomId) != null;
        }
        private bool RoomExists(long hospitalId, string name)
        {
            return _unitOfWork.Room.GetRoomByHospitalIdAndName(hospitalId, name) != null;
        }
        private bool HospitalExists(long id)
        {
            return _unitOfWork.Hospital.GetDetached(id) != null;
        }
    }
}
