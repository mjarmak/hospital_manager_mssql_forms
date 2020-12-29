using hospital_manager_data_access.Repositories.Implementation;
using hospital_manager_data_access.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace hospital_manager_api.Configuration
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), (typeof(Repository<>)));
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IConsultationRepository, ConsultationRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<ISpecialityRepository, SpecialityRepository>();

            return services;
        }
    }
}
