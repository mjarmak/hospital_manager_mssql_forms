using hospital_manager_data_access.Data;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_data_access.Repositories.Implementation
{
    public class DoctorRepository : Repository<DoctorData>, IDoctorRepository
    {
        public DoctorRepository(HospitalDbContext context) : base(context) { }

        public DoctorData GetDoctor(string username)
        {
            return Db.DoctorData.Include(doctor => doctor.Specialities).Include(doctor => doctor.Consultations).Single(doctor => doctor.Username == username);
        }
        public List<DoctorData> GetDoctors()
        {
            return Db.DoctorData.Include(doctor => doctor.Specialities).Include(doctor => doctor.Consultations).ToList();
        }
        public List<DoctorData> GetDoctorsByHospitalId(long hospitalId)
        {
            return Db.DoctorData.Where(doctor => doctor.Consultations.Any(consultation => consultation.HospitalId == hospitalId)).Include(doctor => doctor.Specialities).Include(doctor => doctor.Consultations).ToList();
        }
        public List<DoctorData> GetDoctorsByConsultationHospitalIdAndSpecialityId(long hospitalId, long specialityId)
        {
            return Db.DoctorData.Where(doctor => doctor.Consultations.Any(consultation => consultation.HospitalId == hospitalId && consultation.SpecialityId == specialityId)).Include(doctor => doctor.Specialities).Include(doctor => doctor.Consultations).ToList();
        }
    }
}