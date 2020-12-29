using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using hospital_manager_bl.Service;
using hospital_manager_data_access.Entities;
using Microsoft.AspNetCore.Authorization;
using hospital_manager_models.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Net.Http.Headers;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;

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
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<AppointmentData> SaveAppointment(AppointmentRequest appointment)
        {
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
        }

        [HttpGet("{id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<AppointmentData> GetAppointment(long id)
        {
            return Ok(new
            {
                data = _appointmentService.GetAppointment(id)
            });
        }

        [HttpGet("all")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<IEnumerable<AppointmentData>> GetAppointments()
        {
            return Ok(new
            {
                data = _appointmentService.GetAppointments()
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