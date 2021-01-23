using hospital_manager_data_access.Entities;
using System;
using System.Collections.Generic;

namespace hospital_manager_data_access.Repositories.Interfaces
{
    public interface IAppointmentRepository : IRepository<AppointmentData>
    {
        AppointmentData GetAppointmentByRoomIdAndTime(long roomId, DateTime dateFrom, DateTime dateTo);

        List<AppointmentData> GetAppointmentsByHospitalAndSpeciality(int hospitalId, int specialityId, DateTime dateFrom, DateTime dateTo);

        List<AppointmentData> GetAppointmentsByHospital(int hospitalId, DateTime dateFrom, DateTime dateTo);
    }
}
