using hospital_manager_data_access.Entities;
using System.Collections.Generic;

namespace hospital_manager_data_access.Repositories.Interfaces
{
    public interface IDoctorRepository : IRepository<DoctorData>
    {
        DoctorData GetDoctorSimple(string username);

        DoctorData GetDoctor(string username);

        List<DoctorData> GetDoctors();

        List<DoctorData> GetDoctorsByHospitalId(long hospitalId);

        List<DoctorData> GetDoctorsByConsultationHospitalIdAndSpecialityId(long hospitalId, long specialityId);

    }
}
