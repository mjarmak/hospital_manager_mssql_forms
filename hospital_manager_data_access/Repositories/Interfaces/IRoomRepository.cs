using hospital_manager_data_access.Entities;
using System;
using System.Collections.Generic;

namespace hospital_manager_data_access.Repositories.Interfaces
{
    public interface IRoomRepository : IRepository<RoomData>
    {
        object GetRoom(long id);
    }
}
