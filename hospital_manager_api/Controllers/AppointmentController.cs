using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using hospital_manager_bl.Service;
using hospital_manager_data_access.Entities;
using Microsoft.AspNetCore.Authorization;
using hospital_manager_models.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Net.Http.Headers;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using hospital_manager_models.Util_Models;

namespace hospital_manager_api.Controllers
{
    [Produces("application/json")]
    [Route("appointment")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _appointmentService;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _appointmentService = new AppointmentService(unitOfWork);
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        [HttpGet("ping")]
        public string Ping()
        {
            return "OK";
        }

        [HttpPost]
        [Authorize]
        public ActionResult<AppointmentData> SaveAppointment(AppointmentRequest appointment)
        {

            string role = GetClaim("role");
            string username = GetClaim("preferred_username");
            if (!role.Contains("ADMIN"))
            {
                if (role.Contains("DOCTOR") && !username.Equals(appointment.DoctorUsername))
                {
                    return BadRequest(new
                    {
                        data = "A doctor can only create his own appointments."
                    });
                }
                else if (role.Contains("PATIENT") && !username.Equals(appointment.PatientUsername))
                {
                    return BadRequest(new
                    {
                        data = "A patient can only create his own appointments."
                    });
                }
            }
            try
            {
                var appointmentData = _appointmentService.SaveAppointment(appointment);
                return Ok(new
                {
                    data = appointmentData
                });
            }
            catch (InvalidAppointment e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
            catch (NotFoundRoom e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
            catch (NotFoundUser e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
        }


        [HttpPut]
        [Authorize]
        public ActionResult<AppointmentData> UpdateAppointment(AppointmentRequest appointment)
        {
            string role = GetClaim("role");
            string username = GetClaim("preferred_username");
            if (!role.Contains("ADMIN"))
            {
                if (role.Contains("DOCTOR") && !username.Equals(appointment.DoctorUsername))
                {
                    return BadRequest(new
                    {
                        data = "A doctor can only update his own appointments."
                    });
                }
                else if (role.Contains("PATIENT") && !username.Equals(appointment.PatientUsername))
                {
                    return BadRequest(new
                    {
                        data = "A patient can only update his own appointments."
                    });
                }
            }
            try
            {
                var appointmentData = _appointmentService.UpdateAppointment(appointment);
                return Ok(new
                {
                    data = appointmentData
                });
            }
            catch (NotFoundAppointment e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
            catch (InvalidAppointment e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
            catch (NotFoundRoom e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
            catch (NotFoundUser e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult DeleteAppointment(long id)
        {
            string role = GetClaim("role");
            string username = GetClaim("preferred_username");
            AppointmentResponse appointment;
            try
            {
                appointment = _appointmentService.GetAppointment(id);
            }
            catch (NotFoundAppointment e)
            {
                return NotFound(new
                {
                    data = e.Message
                });
            }
            if (!role.Contains("ADMIN"))
            {
                if (role.Contains("DOCTOR") && !username.Equals(appointment.DoctorUsername))
                {
                    return BadRequest(new
                    {
                        data = "A doctor can only delete his own appointments."
                    });
                }
                else if (role.Contains("PATIENT") && !username.Equals(appointment.PatientUsername))
                {
                    return BadRequest(new
                    {
                        data = "A patient can only delete his own appointments."
                    });
                }
            }
            try
            {
                _appointmentService.DeleteAppointment(id);
                return Ok(new
                {
                    data = "OK"
                });
            }
            catch (NotFoundAppointment e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<AppointmentData> GetAppointment(long id)
        {
            string role = GetClaim("role");
            string username = GetClaim("preferred_username");
            AppointmentResponse appointment;
            try
            {
                appointment = _appointmentService.GetAppointment(id);
            } catch (NotFoundAppointment e)
            {
                return NotFound(new
                {
                    data = e.Message
                });
            }
            if (!role.Contains("ADMIN"))
            {
                if (role.Contains("DOCTOR") && !username.Equals(appointment.DoctorUsername))
                {
                    return BadRequest(new
                    {
                        data = "A doctor can only get his own appointments."
                    });
                }
                else if (role.Contains("PATIENT") && !username.Equals(appointment.PatientUsername))
                {
                    return BadRequest(new
                    {
                        data = "A patient can only get his own appointments."
                    });
                }
            }
            return Ok(new
            {
                data = appointment
            });
        }

        [HttpGet("{id}/confirm")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN,DOCTOR")]
        public ActionResult<AppointmentData> ConfirmAppointment(long id)
        {
            string role = GetClaim("role");
            string username = GetClaim("preferred_username");
            AppointmentResponse appointment;
            try
            {
                appointment = _appointmentService.GetAppointment(id);
            }
            catch (NotFoundAppointment e)
            {
                return NotFound(new
                {
                    data = e.Message
                });
            }
            if (!role.Contains("ADMIN"))
            {
                if (role.Contains("DOCTOR") && !username.Equals(appointment.DoctorUsername))
                {
                    return BadRequest(new
                    {
                        data = "A doctor can only confirm his own appointments."
                    });
                }
                else if (role.Contains("PATIENT") && !username.Equals(appointment.PatientUsername))
                {
                    return BadRequest(new
                    {
                        data = "A patient can only confirm his own appointments."
                    });
                }
            }
            try
            {
                return Ok(new
                {
                    data = _appointmentService.ConfirmAppointment(id)
                });
            }
            catch (InvalidAppointment e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
        }

        [HttpGet("all")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<IEnumerable<AppointmentData>> GetAppointments()
        {
            return Ok(new
            {
                data = _appointmentService.GetAppointments()
            });
        }

        [HttpGet("patient/{patientUsername}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "PATIENT")]
        public ActionResult<List<AppointmentData>> GetAppointmentByPatientUsername(
            string patientUsername,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to
            )
        {
            string role = GetClaim("role");
            string username = GetClaim("preferred_username");
            if (!role.Contains("ADMIN"))
            {
                if (role.Contains("DOCTOR") && !username.Equals(patientUsername))
                {
                    return BadRequest(new
                    {
                        data = "A doctor can only get his own appointments."
                    });
                }
                else if (role.Contains("PATIENT") && !username.Equals(patientUsername))
                {
                    return BadRequest(new
                    {
                        data = "A patient can only get his own appointments."
                    });
                }
            }
            return Ok(new
            {
                data = _appointmentService.GetAppointmentByPatientUsername(patientUsername, from, to)
            });
        }

        [HttpGet("doctor/{doctorUsername}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "DOCTOR")]
        public ActionResult<List<AppointmentData>> GetAppointmentByDoctorUsername(
            string doctorUsername,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to
            )
        {
            string role = GetClaim("role");
            string username = GetClaim("preferred_username");
            if (!role.Contains("ADMIN"))
            {
                if (role.Contains("DOCTOR") && !username.Equals(doctorUsername))
                {
                    return BadRequest(new
                    {
                        data = "A doctor can only get his own appointments."
                    });
                }
                else if (role.Contains("PATIENT") && !username.Equals(doctorUsername))
                {
                    return BadRequest(new
                    {
                        data = "A patient can only get his own appointments."
                    });
                }
            }
            return Ok(new
            {
                data = _appointmentService.GetAppointmentByDoctorUsername(doctorUsername, from, to)
            });
        }

        [HttpGet("room/{roomId}/taken")]
        public ActionResult<bool> GetAppointmentTaken(
            long roomId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to
            )
        {
            return Ok(new
            {
                data = _appointmentService.AppointmentTaken(roomId, from, to)
            });
        }

        [HttpGet("room/{roomId}/possible")]
        public ActionResult<bool> GetAppointmentPossible(
            long roomId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            [FromQuery] string doctorUsername
            )
        {
            bool taken = !_appointmentService.AppointmentTaken(roomId, from, to);
            bool valid = _appointmentService.AppointmentHalfDayValid(doctorUsername, roomId, from, to);
            return Ok(new
            {
                data = taken && valid
            });
        }

        [HttpGet("hospital/{hospitalId}/speciality/{specialityId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<List<AppointmentData>> GetAppointmentsByHospitalAndSpeciality(
            int hospitalId,
            int specialityId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to
            )
        {
            return Ok(new
            {
                data = _appointmentService.GetAppointmentsByHospitalAndSpeciality(hospitalId, specialityId, from, to)
            });
        }
        [HttpGet("hospital/{hospitalId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<List<AppointmentData>> GetAppointmentsByHospital(
            int hospitalId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to
            )
        {
            return Ok(new
            {
                data = _appointmentService.GetAppointmentsByHospital(hospitalId, from, to)
            });
        }
        [HttpGet("hospital/{hospitalId}/speciality/{specialityId}/suggestions")]
        public ActionResult<List<AppointmentData>> GetAppointmentSuggestions(
            int hospitalId,
            int specialityId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to
            )
        {
            return Ok(new
            {
                data = _appointmentService.GetAppointmentSuggestions(hospitalId, specialityId, from, to)
            });
        }
        [HttpGet("hospital/{hospitalId}/speciality/{specialityId}/freerooms")]
        public ActionResult<List<RoomFromTo>> GetFreeRooms(
            int hospitalId,
            int specialityId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to
            )
        {
            return Ok(new
            {
                data = _appointmentService.GetFreeRooms(hospitalId, specialityId, from, to)
            });
        }
        [HttpGet("hospital/{hospitalId}/speciality/{specialityId}/freedays")]
        public ActionResult<List<DateTime>> GetFreeDays(
            int hospitalId,
            int specialityId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to
            )
        {
            return Ok(new
            {
                data = _appointmentService.GetFreeDays(hospitalId, specialityId, from, to)
            });
        }
        private string GetClaim(string name)
        {
            var accessTokenString = Request.Headers[HeaderNames.Authorization].ToString();

            if (accessTokenString == null || !accessTokenString.Contains("Bearer "))
            {
                return "NONE";
            }

            try
            {
                var accessToken = _tokenHandler.ReadToken(accessTokenString.Replace("Bearer ", "")) as JwtSecurityToken;
                return accessToken.Claims.Single(claim => claim.Type == name).Value;
            }
            catch (ArgumentException)
            {
                return "NONE";
            }
        }
    }
}