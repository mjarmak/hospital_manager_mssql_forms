﻿using hospital_manager_data_access.Entities;
using System.Collections.Generic;

namespace hospital_manager_data_access.Repositories.Interfaces
{
    public interface IHospitalRepository : IRepository<HospitalData>
    {
        void UpdateHospital(HospitalData hospital);

        HospitalData GetHospital(long id);

        List<HospitalData> GetHospitals();

        List<HospitalData> GetHospitalsBySpecialityId(long specialityId);

        HospitalData GetHospitalByRoomId(long roomId);
    }
}
