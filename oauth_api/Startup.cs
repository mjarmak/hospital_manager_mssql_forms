using authentication_api.Context;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using System.Linq;
using System.Reflection;

namespace authentication_api
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
            IdentityModelEventSource.ShowPII = true; // for debugging

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", config =>
                {
                    config.Authority = "https://localhost:44321/";
                    config.Audience = "auth";
                    config.Audience = "hm";
                });

            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            // Do first:
            // dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
            // dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
            // dotnet ef migrations add InitialIdentityServerDefaultDbMigration -c DefaultDbContext -o Data/Migrations/IdentityServer/DefaultDbContext
            //services.AddDbContext<DefaultContext>(
            //    options => options.UseSqlServer(connectionString,
            //    b => b.MigrationsAssembly(typeof(DefaultContext).Assembly.FullName))
            //);

            services.AddDbContext<DefaultContext>(config =>
            {
                config.UseInMemoryDatabase("Memory");
            });
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 1;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<DefaultContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddInMemoryApiResources(AuthConfiguration.GetApis())
                .AddInMemoryIdentityResources(AuthConfiguration.GetIdentityResources())
                .AddInMemoryClients(AuthConfiguration.GetClients())
                .AddDeveloperSigningCredential();

            //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //services.AddIdentityServer()
            //    .AddAspNetIdentity<IdentityUser>()
            //    .AddDeveloperSigningCredential()
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //            builder.UseSqlServer(connectionString,
            //                sql => sql.MigrationsAssembly(migrationsAssembly));
            //    })
            //    .AddOperationalStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //            builder.UseSqlServer(connectionString,
            //                sql => sql.MigrationsAssembly(migrationsAssembly));
            //        options.EnableTokenCleanup = true;
            //        options.TokenCleanupInterval = 30;
            //    });

            services.AddCors(config =>
                config.AddPolicy("AllowAll",
                    p => p.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader())
                );
            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //private void InitializeDatabase(IApplicationBuilder app)
        //{
        //    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        //    {
        //        serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        //        var contextConfig = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        //        contextConfig.Database.Migrate();
        //        if (!contextConfig.Clients.Any())
        //        {
        //            foreach (var client in AuthConfiguration.GetClients())
        //            {
        //                contextConfig.Clients.Add(client.ToEntity());
        //            }
        //            contextConfig.SaveChanges();
        //        }

        //        if (!contextConfig.IdentityResources.Any())
        //        {
        //            foreach (var resource in AuthConfiguration.GetIdentityResources())
        //            {
        //                contextConfig.IdentityResources.Add(resource.ToEntity());
        //            }
        //            contextConfig.SaveChanges();
        //        }

        //        if (!contextConfig.ApiResources.Any())
        //        {
        //            foreach (var resource in AuthConfiguration.GetApis())
        //            {
        //                contextConfig.ApiResources.Add(resource.ToEntity());
        //            }
        //            contextConfig.SaveChanges();
        //        }
        //    }
        //}
    }
}
