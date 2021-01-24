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
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<HospitalResponse> SaveHospital(HospitalRequest hospital)
        {
            return Ok(new
            {
                data = _hospitalService.SaveHospital(hospital)
            });
        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<HospitalResponse> UpdateHospital(HospitalRequest hospital)
        {
            return Ok(new
            {
                data = _hospitalService.UpdateHospital(hospital)
            });
        }

        [HttpPut("{id}/room")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<HospitalResponse> UpdateHospitalRooms(long id, List<RoomRequest> rooms)
        {
            return Ok(new
            {
                data = _hospitalService.UpdateHospitalRooms(id, rooms)
            });
        }

        [HttpDelete("room/{id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
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
            catch (InvalidAppointment e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
            catch (InvalidRoom e)
            {
                return BadRequest(new
                {
                    data = e.Message
                });
            }
        }

        [HttpGet("{id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<HospitalResponse> GetHospital(long id)
        {
            return Ok(new
            {
                data = _hospitalService.GetHospital(id)
            });
        }

        [HttpGet("all")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<List<HospitalResponse>> GetHospitals()
        {
            return Ok(new
            {
                data = _hospitalService.GetHospitals()
            });
        }
        [HttpGet("speciality/{specialityId}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
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