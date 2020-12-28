using hospital_manager_data_access.Data;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;

namespace hospital_manager_data_access.Repositories.Implementation
{
    public class SpecialityRepository : Repository<SpecialityData>, ISpecialityRepository
    {
        public SpecialityRepository(HospitalDbContext context) : base(context) { }
    }
}