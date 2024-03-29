﻿using hospital_manager_data_access.Data;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_data_access.Repositories.Implementation
{
    public class HospitalRepository : Repository<HospitalData>, IHospitalRepository
    {
        public HospitalRepository(HospitalDbContext context) : base(context) { }

        public void UpdateHospital(HospitalData hospital)
        {
            Db.HospitalData.Update(hospital);
        }

        public HospitalData GetHospital(long id)
        {
            return Db.HospitalData.Include(hospital => hospital.Address).Include(hospital => hospital.OpeningHours).SingleOrDefault(hospital => hospital.Id == id);
        }
        public List<HospitalData> GetHospitals()
        {
            return Db.HospitalData.Include(hospital => hospital.Address).Include(hospital => hospital.OpeningHours).ToList();
        }
        public List<HospitalData> GetHospitalsBySpecialityId(long specialityId)
        {
            return Db.HospitalData.Where(hospital => Db.RoomData.Any(room => room.HospitalId == hospital.Id && room.Specialities.Any(speciality => speciality.SpecialityId == specialityId)))
            .Include(hospital => hospital.Address).Include(hospital => hospital.OpeningHours).ToList();
        }
        public HospitalData GetHospitalByRoomId(long roomId)
        {
            return Db.HospitalData.Include(hospital => hospital.Address).Include(hospital => hospital.OpeningHours)
                .SingleOrDefault(hospital => Db.RoomData.Any(room => room.HospitalId == hospital.Id && room.Id == roomId));
        }

    }
}