using System;
using System.IO;
using System.Reflection;
using hospital_manager_api.Configuration;
using hospital_manager_data_access.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace hospital_manager_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", config =>
                {
                    config.Authority = "https://localhost:44321/";
                    config.Audience = "hm";
                });

            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //// Do first:
            //// dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
            //// dotnet ef migrations add InitialIdentityServerConfigurationDbMigration - c ConfigurationDbContext - o Data/Migrations/IdentityServer/ConfigurationDb
            //// dotnet ef migrations add InitialIdentityServerDefaultDbMigration -c DefaultDbContext -o Data/Migrations/IdentityServer/DefaultDbContext
            //services.AddDbContext<HospitalDbContext>(
            //    options => options.UseSqlServer(connectionString,
            //    b => b.MigrationsAssembly(typeof(HospitalDbContext).Assembly.FullName))
            //);

            services.AddDbContext<HospitalDbContext>(
                config =>
                {
                    config.UseInMemoryDatabase("Memory");
                }
            );

            services.AddControllers();
            //services.AddMvc();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Hospital Manager",
                    Description = "Project SGBD - Creation of an Hospital Manager application. ",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Francesco Bigi",
                        Email = "psr07700@students.ephec.com",
                        Url = new Uri("https://be.linkedin.com/in/bigif"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddApplicationServices();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            //Need to use an end point in order to access to swagger page
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
