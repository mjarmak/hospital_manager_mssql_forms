using System;
using System.Collections.Generic;
using hospital_manager_bl.Service;
using hospital_manager_data_access.Entities;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_models.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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

        private static AppointmentService _appointmentService;
        private static DoctorService _doctorService;
        private static HospitalService _hospitalService;
        private static SpecialityService _specialityService;
        
        

        public Program(IUnitOfWork unitOfWork)
        {
            _hospitalService = new HospitalService(unitOfWork);
            _specialityService = new SpecialityService(unitOfWork);
            _appointmentService = new AppointmentService(unitOfWork);
            _doctorService = new DoctorService(unitOfWork);
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
                    new OpeningHoursRequest{ Day = "MONDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                    new OpeningHoursRequest{ Day = "TUESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                    new OpeningHoursRequest{ Day = "WEDNESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                    new OpeningHoursRequest{ Day = "THURSDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                    new OpeningHoursRequest{ Day = "FRIDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                    new OpeningHoursRequest{ Day = "SATURDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true},
                    new OpeningHoursRequest{ Day = "SUNDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true}
                    },
                    Rooms = new List<RoomRequest>
                    {
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "111"},
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "112"},
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "113"},
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "114"}
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
                        new OpeningHoursRequest{ Day = "MONDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Day = "TUESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Day = "WEDNESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Day = "THURSDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Day = "FRIDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Day = "SATURDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true},
                        new OpeningHoursRequest{ Day = "SUNDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true}
                    },
                    Rooms = new List<RoomRequest>
                    {
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "999"},
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "888"},
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "777"},
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "666"}
                    }

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
                        new OpeningHoursRequest{ Day = "MONDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Day = "TUESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Day = "WEDNESDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Day = "THURSDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Day = "FRIDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = false},
                        new OpeningHoursRequest{ Day = "SATURDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true},
                        new OpeningHoursRequest{ Day = "SUNDAY", HourFrom = 7, HourTo = 19, MinuteTo = 00, MinuteFrom = 30, Closed = true}
                    },
                    Rooms = new List<RoomRequest>
                    {
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "AAAA"},
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "BBBB"},
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "CCCC"},
                        new RoomRequest { SpecialityIds = new List<long>{1,2,3,4}, Name = "DDDD"}
                    }
                } );
        }

        private static void AddSpeciality(IHost host)
        {
            _specialityService.SaveSpeciality(
                new SpecialityRequest {Name = "Generalist" }
            );
            _specialityService.SaveSpeciality(
                new SpecialityRequest { Name = "Pediatrics" }
            );
            _specialityService.SaveSpeciality(
                new SpecialityRequest { Name = "Surgery" }
            );
            _specialityService.SaveSpeciality(
                new SpecialityRequest { Name = "Orthopedics" }
            );
            _specialityService.SaveSpeciality(
                new SpecialityRequest { Name = "Gynecology" }
            );
        }

        private static void AddAppointment(IHost host)
        {
            //Doctor DRamoray
            _appointmentService.SaveAppointment(
                new  AppointmentRequest
                {
                    PatientUsername = "user1",
                    DoctorUsername = "DRamoray",
                    Description = "appointment for Pediatrics.",
                    RoomId = 1,
                    From = new DateTime(2021, 1, 18, 7, 0,0),
                    To = new DateTime(2021, 1, 18, 7, 15, 0),
                });
            _appointmentService.SaveAppointment(
                new AppointmentRequest
                {
                    PatientUsername = "user2",
                    DoctorUsername = "DRamoray",
                    Description = "appointment for Generalist.",
                    RoomId = 1,
                    From = new DateTime(2021, 1, 18, 15, 45, 00),
                    To = new DateTime(2021, 1, 18, 16, 00, 00),
                });
            _appointmentService.SaveAppointment(
                new AppointmentRequest
                {
                    PatientUsername = "user2",
                    DoctorUsername = "DRamoray",
                    Description = "appointment for Generalist.",
                    RoomId = 1,
                    From = new DateTime(2021, 1, 18, 16, 45, 00),
                    To = new DateTime(2021, 1, 18, 17, 00, 00),
                });
            _appointmentService.SaveAppointment(
                new AppointmentRequest
                {
                    PatientUsername = "user3",
                    DoctorUsername = "DRamoray",
                    Description = "appointment for Orthopedics.",
                    RoomId = 2,
                    From = new DateTime(2021, 1, 19, 16, 45, 00),
                    To = new DateTime(2021, 1, 19, 17, 00, 00),
                    
                });

            //Doctor DWho
            _appointmentService.SaveAppointment(
                new AppointmentRequest
                {
                    PatientUsername = "user1",
                    DoctorUsername = "DWho",
                    Description = "appointment for Surgery.",
                    RoomId = 3,
                    From = new DateTime(2021, 1, 16, 17, 0, 0),
                    To = new DateTime(2021, 1, 18, 17, 15, 0),
                });
        }

        private static void AddDoctor(IHost host)
        {
            _doctorService.SaveDoctor(
                new DoctorRequest
                {
                    Name = "Drake Ramoray",
                    Username = "DRamoray",
                    Consultations = new List<ConsultationRequest>
                    {
                        new ConsultationRequest{HospitalId = 1,Duration = 2,Id = 2,SpecialityId = 3}
                    },
                    SpecialityIds = new List<long> {1,2,3}
                });
            _doctorService.SaveDoctor(
                new DoctorRequest
                {
                    Name = "Doctor Who",
                    Username = "DWho",
                    Consultations = new List<ConsultationRequest>
                    {
                        new ConsultationRequest{HospitalId = 1,Duration = 1,Id = 2,SpecialityId = 3}
                    },
                    SpecialityIds = new List<long> { 3, 4, 5 }
                });
        }
    }
}
