using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using authentication_api.Context;
using hospital_manager_bl.Service;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_models.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;

namespace hospital_manager_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            AddHospital(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static HospitalService _hospitalService;
        public Program(IUnitOfWork unitOfWork)
        {
            _hospitalService = new HospitalService(unitOfWork);
        }
        private static void AddHospital(IHost host)
        {
            _hospitalService.SaveHospital(
                new HospitalRequest {
                    Name = "Saint pierre",
                    Address = new AddressRequest{
                        Street = "rue au bois", 
                        BoxNumber = "25", 
                        PostalCode = "1465", 
                        City = "Brussels", 
                        Country = "Belgium"
                    }, 
                    OpeningHours = new List<OpeningHoursRequest>{
                    new OpeningHoursRequest{ Id = 0, Day = "MONDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                    new OpeningHoursRequest{ Id = 1, Day = "TUESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                    new OpeningHoursRequest{ Id = 2, Day = "WEDNESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                    new OpeningHoursRequest{ Id = 3, Day = "THURSDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                    new OpeningHoursRequest{ Id = 4, Day = "FRIDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                    new OpeningHoursRequest{ Id = 5, Day = "SATURDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true},
                    new OpeningHoursRequest{ Id = 6, Day = "SUNDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true}
                    }
                });

            _hospitalService.SaveHospital(
                new HospitalRequest
                {
                    Name = "SaintLuc",
                    Address = new AddressRequest
                    {
                        Street = "rue au marché",
                        BoxNumber = "86",
                        PostalCode = "1578",
                        City = "Brussels",
                        Country = "Belgium"
                    },
                    OpeningHours = new List<OpeningHoursRequest>{
                        new OpeningHoursRequest{ Id = 0, Day = "MONDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Id = 1, Day = "TUESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Id = 2, Day = "WEDNESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Id = 3, Day = "THURSDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Id = 4, Day = "FRIDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Id = 5, Day = "SATURDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true},
                        new OpeningHoursRequest{ Id = 6, Day = "SUNDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true}
                    },

                });

            _hospitalService.SaveHospital(
                new HospitalRequest
                {
                    Name = "UZ Jette",
                    Address = new AddressRequest
                    {
                        Street = "rue Jette",
                        BoxNumber = "25",
                        PostalCode = "1465",
                        City = "Jette",
                        Country = "Belgium"
                    },
                    OpeningHours = new List<OpeningHoursRequest>{
                        new OpeningHoursRequest{ Id = 0, Day = "MONDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Id = 1, Day = "TUESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Id = 2, Day = "WEDNESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Id = 3, Day = "THURSDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Id = 4, Day = "FRIDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Id = 5, Day = "SATURDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true},
                        new OpeningHoursRequest{ Id = 6, Day = "SUNDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true}
                    }
                });
        }

                




    }
}
