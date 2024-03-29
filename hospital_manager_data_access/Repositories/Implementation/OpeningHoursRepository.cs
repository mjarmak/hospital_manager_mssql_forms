﻿using hospital_manager_data_access.Data;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;

namespace hospital_manager_data_access.Repositories.Implementation
{
    public class OpeningHoursRepository : Repository<OpeningHoursData>, IOpeningHoursRepository
    {
        public OpeningHoursRepository(HospitalDbContext context) : base(context) { }
    }
}