using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using hospital_manager_bl.Service;
using Microsoft.AspNetCore.Authorization;
using hospital_manager_models.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Net.Http.Headers;
using hospital_manager_data_access.Repositories.Interfaces;
using hospital_manager_exceptions.Exceptions;

namespace hospital_manager_api.Controllers
{
    [Produces("application/json")]
    [Route("hospital")]
    [ApiController]
    public class HospitalController : Controller
    {
        private readonly HospitalService _hospitalService;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public HospitalController(IUnitOfWork unitOfWork)
        {
            _hospitalService = new HospitalService(unitOfWork);
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        [HttpGet("ping")]
        public string Ping()
        {
            return "OK";
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<HospitalResponse> SaveHospital(HospitalRequest hospital)
        {
            try
            {
                return Ok(new
                {
                    data = _hospitalService.SaveHospital(hospital)
                });
            }
            catch (InvalidHospital e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<HospitalResponse> UpdateHospital(HospitalRequest hospital)
        {
            try
            {
                return Ok(new
                {
                    data = _hospitalService.UpdateHospital(hospital)
                });
            }
            catch (InvalidHospital e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
        }

        [HttpPut("{id}/room")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<HospitalResponse> UpdateHospitalRooms(long id, List<RoomRequest> rooms)
        {
            try
            {
                return Ok(new
                {
                    data = _hospitalService.UpdateHospitalRooms(id, rooms)
                });
            }
            catch (InvalidHospital e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
            return Ok(new
            {
                data = _hospitalService.UpdateHospitalRooms(id, rooms)
            });
        }

        [HttpDelete("room/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<HospitalResponse> DeleteHospitalRoom(long id)
        {
            try
            {
                _hospitalService.DeleteHospitalRoom(id);
                return Ok(new
                {
                    data = "OK"
                });
            }
            catch (InvalidHospital e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<HospitalResponse> GetHospital(long id)
        {
            return Ok(new
            {
                data = _hospitalService.GetHospital(id)
            });
        }

        [HttpGet("all")]
        public ActionResult<List<HospitalResponse>> GetHospitals()
        {
            return Ok(new
            {
                data = _hospitalService.GetHospitals()
            });
        }
        [HttpGet("speciality/{specialityId}")]
        public ActionResult<List<HospitalResponse>> GetHospitalsBySpecialityId(long specialityId)
        {
            return Ok(new
            {
                data = _hospitalService.GetHospitalsBySpecialityId(specialityId)
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