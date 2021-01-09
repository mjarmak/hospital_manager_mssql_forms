using hospital_manager_data_access.Entities;
using System.Collections.Generic;

namespace hospital_manager_data_access.Repositories.Interfaces
{
    public interface IHospitalRepository : IRepository<HospitalData>
    {
        HospitalData GetHospital(long id);

        List<HospitalData> GetHospitals();

        public List<HospitalData> GetHospitalsBySpecialityId(long specialityId);
    }
}
