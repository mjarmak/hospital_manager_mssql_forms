using hospital_manager_data_access.Data;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;

namespace hospital_manager_data_access.Repositories.Implementation
{
    public class SpecialityToRoomRepository : Repository<SpecialityToRoomData>, ISpecialityToRoomRepository
    {
        public SpecialityToRoomRepository(HospitalDbContext context) : base(context) { }
    }
}