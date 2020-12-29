using hospital_manager_data_access.Data;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_data_access.Repositories.Implementation
{
    public class RoomRepository : Repository<RoomData>, IRoomRepository
    {
        public RoomRepository(HospitalDbContext context) : base(context) { }

        public object GetRoom(long id)
        {
            return Db.RoomData.Where(room => room.Id == id)
                .Join(
                Db.SpecialityToRoomData,
                room => room.Id,
                specialityToRoom => specialityToRoom.RoomId,
            (room, specialityToRoom) => new
            {
                room.Id,
                room.Name,
                Specialities = specialityToRoom.Id
            })
                .Single();
        }
    }
}