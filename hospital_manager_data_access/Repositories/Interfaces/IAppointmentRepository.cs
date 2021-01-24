using hospital_manager_data_access.Entities;
using System;
using System.Collections.Generic;

namespace hospital_manager_data_access.Repositories.Interfaces
{
    public interface IAppointmentRepository : IRepository<AppointmentData>
    {

        List<AppointmentData> GetAppointmentByDoctorUsername(string doctorUsername, DateTime from, DateTime to);

        List<AppointmentData> GetAppointmentByPatientUsername(string doctorUsername, DateTime from, DateTime to);

        AppointmentData GetAppointmentByRoomIdAndTimeExclusive(long roomId, DateTime dateFrom, DateTime dateTo);

        AppointmentData GetAppointmentByRoomIdAndTime(long roomId, DateTime dateFrom, DateTime dateTo);

        List<AppointmentData> GetAppointmentsByHospitalAndSpeciality(int hospitalId, int specialityId, DateTime dateFrom, DateTime dateTo);

        List<AppointmentData> GetAppointmentsByHospital(int hospitalId, DateTime dateFrom, DateTime dateTo);
    }
}
