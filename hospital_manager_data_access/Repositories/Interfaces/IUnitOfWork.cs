using System;

namespace hospital_manager_data_access.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository Appointment { get; }
        IConsultationRepository Consultation { get; }
        IDoctorRepository Doctor { get; }
        IHospitalRepository Hospital { get; }
        IRoomRepository Room { get; }
        ISpecialityRepository Speciality { get; }
        IOpeningHoursRepository OpeningHours { get; }
        int Save();
    }
}