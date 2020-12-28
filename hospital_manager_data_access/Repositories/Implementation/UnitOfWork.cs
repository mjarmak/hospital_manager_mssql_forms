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

        #region Public variables
        public IAppointmentRepository Appointment { get; private set; }
        public IConsultationRepository Consultation { get; private set; }
        public IDoctorRepository Doctor { get; private set; }
        public IHospitalRepository Hospital { get; private set; }
        public IRoomRepository Room { get; private set; }
        public ISpecialityRepository Speciality { get; private set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public UnitOfWork(HospitalDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            Appointment = new AppointmentRepository(_context);
            Consultation = new ConsultationRepository(_context);
            Doctor = new DoctorRepository(_context);
            Hospital = new HospitalRepository(_context);
            Room = new RoomRepository(_context);
            Speciality = new SpecialityRepository(_context);
        }

        /// <summary>
        /// Completes this instance.
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
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