using hospital_manager_data_access.Data;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace hospital_manager_data_access.Repositories.Implementation
{
    public class AppointmentRepository : Repository<AppointmentData>, IAppointmentRepository
    {
        public AppointmentRepository(HospitalDbContext context) : base(context) { }

        public List<AppointmentData> GetAppointmentsByHospitalAndSpeciality(int hospitalId, int specialityId, DateTime dateFrom, DateTime dateTo)
        {
            return Db.AppointmentData
                .Where(appointment =>
                Db.RoomData.Any(room =>
                room.Id == appointment.RoomId
                && room.Specialities.Any(speciality => speciality.SpecialityId == specialityId)
                && room.HospitalId == hospitalId)
                && appointment.From >= dateFrom
                && appointment.To <= dateTo
                ).OrderBy(appointment => appointment.From).ToList();
        }
        public List<AppointmentData> GetAppointmentsByHospital(int hospitalId, DateTime dateFrom, DateTime dateTo)
        {
            return Db.AppointmentData
                .Where(appointment =>
                Db.RoomData.Any(room =>
                room.Id == appointment.RoomId
                && room.HospitalId == hospitalId)
                && appointment.From >= dateFrom
                && appointment.To <= dateTo
                ).OrderBy(appointment => appointment.From).ToList();
        }
    }
}