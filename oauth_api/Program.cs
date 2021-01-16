using IdentityModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace authentication_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var scope = host.Services.CreateScope();

            var roleManager = scope.ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            roleManager.CreateAsync(new IdentityRole { Name = "ADMIN" }).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole { Name = "DOCTOR" }).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole { Name = "PATIENT" }).GetAwaiter().GetResult();

            AddUser(host, "admin", "ADMIN", "admin", "admin", "OTHER", "+32466550935",
                    "10/07/1995");

            AddUser(host, "mohamadjarmak@gmail.com", "PATIENT", "Mohamad", "Jarmak", "MALE", "+32466550935",
                "07/06/1995");

            AddUser(host, "bigi_admin@businessmanager.com", "PATIENT", "Francesco", "Bigi", "MALE", "+32466550935",
                "10/07/1995");

            AddUser(host, "newbreaker@gmail.com", "PATIENT", "Francesco2", "Bigi2", "MALE", "+32466550935",
                "10/07/1995");

            AddUser(host, "mohamadjarmak@gmail.com", "DOCTOR", "Doctor", "Who", "MALE", "+32466550935",
                "10/07/1995");

            AddUser(host, "mohamadjarmak@gmail.com", "DOCTOR", "Drake", "Ramoray", "MALE", "+32466550935",
                "10/07/1995");

            host.Run();
        }

        public static void AddUser(IHost host, string email, string role, string name, string familyName, string gender, string phoneNumber, string birthdate)
        {
            var scope = host.Services.CreateScope();

            var userManager = scope.ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();

            var username = name + familyName + birthdate.Replace("/", "");
            var user = new IdentityUser(username);
            user.Email = email;
            userManager.CreateAsync(user, "password").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, role);
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Email, email));
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Name, name));
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.FamilyName, familyName));
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Gender, gender));
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.PhoneNumber, phoneNumber));
            userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.BirthDate, birthdate));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
