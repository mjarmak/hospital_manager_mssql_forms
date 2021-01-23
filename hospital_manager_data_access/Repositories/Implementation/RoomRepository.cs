using hospital_manager_data_access.Data;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_data_access.Repositories.Implementation
{
    public class RoomRepository : Repository<RoomData>, IRoomRepository
    {
        public RoomRepository(HospitalDbContext context) : base(context) { }

        public RoomData GetRoom(long id)
        {
            return Db.RoomData.Include(room => room.Specialities).Single(room => room.Id == id);
        }
        public RoomData GetRoomByHospitalIdAndName(long hospitalId, string name)
        {
            return Db.RoomData.Include(room => room.Specialities).Single(room => room.HospitalId == hospitalId && room.Name == name);
        }
        public List<RoomData> GetRooms()
        {
            return Db.RoomData.Include(room => room.Specialities).ToList();
        }
        public List<RoomData> GetRoomsByHospitalId(long hospitalId)
        {
            return Db.RoomData.Include(room => room.Specialities).Where(room => room.HospitalId == hospitalId).ToList();
        }
        public List<RoomData> GetRoomsByHospitalIdAndSpecialityId(long hospitalId, long specialityId)
        {
            return Db.RoomData.Include(room => room.Specialities).Where(room => room.HospitalId == hospitalId && room.Specialities.Any(speciality => speciality.SpecialityId == specialityId)).ToList();
        }
    }
}