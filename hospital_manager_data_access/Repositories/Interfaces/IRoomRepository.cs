using hospital_manager_data_access.Entities;
using System;
using System.Collections.Generic;

namespace hospital_manager_data_access.Repositories.Interfaces
{
    public interface IRoomRepository : IRepository<RoomData>
    {
        RoomData GetRoom(long id);

        RoomData GetRoomByHospitalIdAndName(long hospitalId, string name);

        List<RoomData> GetRooms();

        List<RoomData> GetRoomsByHospitalId(long hospitalId);

        List<RoomData> GetRoomsByHospitalIdAndSpecialityId(long hospitalId, long specialityId);
    }
}
