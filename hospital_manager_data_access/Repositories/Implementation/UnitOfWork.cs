using hospital_manager_data_access.Data;
using hospital_manager_data_access.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace hospital_manager_data_access.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private HospitalDbContext _context;
        private bool _disposed;

        public IAppointmentRepository Appointment { get; private set; }
        public IConsultationRepository Consultation { get; private set; }
        public IDoctorRepository Doctor { get; private set; }
        public IHospitalRepository Hospital { get; private set; }
        public IRoomRepository Room { get; private set; }
        public ISpecialityRepository Speciality { get; private set; }
        public ISpecialityToDoctorRepository SpecialityToDoctor { get; private set; }
        public ISpecialityToRoomRepository SpecialityToRoom { get; private set; }

        public UnitOfWork(HospitalDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            Appointment = new AppointmentRepository(_context);
            Consultation = new ConsultationRepository(_context);
            Doctor = new DoctorRepository(_context);
            Hospital = new HospitalRepository(_context);
            Room = new RoomRepository(_context);
            Speciality = new SpecialityRepository(_context);
            SpecialityToDoctor = new SpecialityToDoctorRepository(_context);
            SpecialityToRoom = new SpecialityToRoomRepository(_context);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing && _context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}