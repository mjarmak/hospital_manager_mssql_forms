using authentication_api.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace authentication_api
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true; // for debugging

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", config =>
                {
                    config.Authority = "https://localhost:44321/";
                    config.Audience = "auth";
                    config.Audience = "hm";
                    //config.RequireHttpsMetadata = false;
                });

            services.AddDbContext<DefaultContext>(config =>
            {
                config.UseInMemoryDatabase("Memory");
            });
            // AddIdentity registers the services
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 6;
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //swagger
            //app.UseSwagger(c =>
            //{
            //    c.SerializeAsV2 = true;
            //});

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseIdentityServer();

            //Need to use an end point in order to access to swagger page
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/V1/swagger.json", "My API V1");
            //    c.RoutePrefix = string.Empty;
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
