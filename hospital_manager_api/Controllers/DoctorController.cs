using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using hospital_manager_bl.Service;
using hospital_manager_data_access.Entities;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Net.Http.Headers;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using hospital_manager_models.Models;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace hospital_manager_api.Controllers
{
    [Produces("application/json")]
    [Route("doctor")]
    [ApiController]
    public class DoctorController : Controller
    {
        private readonly DoctorService _doctorService;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public DoctorController(IUnitOfWork unitOfWork)
        {
            _doctorService = new DoctorService(unitOfWork);
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        [HttpGet("ping")]
        public string Ping()
        {
            return "OK";
        }

        [HttpPost("register")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<DoctorResponse> RegisterDoctor(UserAccountRequest userAccountRequest)
        {
            if (userAccountRequest.DoctorRequest == null)
            {
                return BadRequest(new
                {
                    data = "Doctor object is missing."
                });
            }

            try
            {
                var result = _doctorService.RegisterDoctor(userAccountRequest, GetAccessToken());
                return Ok(new
                {
                    data = result
                });
            }
            catch (InvalidUserRequest e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
        }

        [HttpGet("hospital/{hospitalId}")]
        public ActionResult<DoctorResponse> GetDoctorsByHospitalId(long hospitalId)
        {
            return Ok(new
            {
                data = _doctorService.GetDoctorsByHospitalId(hospitalId)
            });
        }

        [HttpGet("{username}")]
        public ActionResult<DoctorResponse> GetDoctor(string username)
        {
            return Ok(new
            {
                data = _doctorService.GetDoctor(username)
            });
        }

        [HttpGet("all")]
        public ActionResult<List<DoctorResponse>> GetDoctors()
        {
            return Ok(new
            {
                data = _doctorService.GetDoctors()
            });
        }
        private string GetAccessToken()
        {
            var accessTokenString = Request.Headers[HeaderNames.Authorization].ToString();

            if (accessTokenString == null || !accessTokenString.Contains("Bearer "))
            {
                return "NONE";
            }
            try
            {
                return accessTokenString.Replace("Bearer ", "");
            }
            catch (ArgumentException)
            {
                return "NONE";
            }
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