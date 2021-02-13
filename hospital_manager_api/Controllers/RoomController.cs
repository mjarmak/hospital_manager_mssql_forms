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
    [Route("room")]
    [ApiController]
    public class RoomController : Controller
    {
        private readonly RoomService _roomService;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public RoomController(IUnitOfWork unitOfWork)
        {
            _roomService = new RoomService(unitOfWork);
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        [HttpGet("ping")]
        public string Ping()
        {
            return "OK";
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<RoomResponse> SaveRoom(RoomRequest room)
        {
            try
            {
                var roomResponse = _roomService.SaveRoom(room);
                return Ok(new
                {
                    data = roomResponse
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
        [HttpPost("all")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<List<RoomResponse>> SaveRooms(List<RoomRequest> rooms)
        {
            try
            {
                var roomResponse = _roomService.SaveRooms(rooms);
                return Ok(new
                {
                    data = roomResponse
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
        public ActionResult<RoomResponse> GetRoom(long id)
        {
            try
            {
                return Ok(new
                {
                    data = _roomService.GetRoom(id)
                });
            }
            catch (NotFoundRoom e)
            {
                return NotFound(new
                {
                    data = e.Message
                });
            }
        }
        [HttpGet("hospital/{hospitalId}")]
        public ActionResult<List<RoomResponse>> GetRoomsByHospitalId(long hospitalId)
        {
            return Ok(new
            {
                data = _roomService.GetRoomsByHospitalId(hospitalId)
            });
        }
        [HttpGet("hospital/{hospitalId}/speciality/{specialityId}")]
        public ActionResult<List<RoomResponse>> GetRoomsByHospitalIdAndSpecialityId(long hospitalId, long specialityId)
        {
            return Ok(new
            {
                data = _roomService.GetRoomsByHospitalIdAndSpecialityId(hospitalId, specialityId)
            });
        }
       [HttpGet("all")]
        public ActionResult<List<RoomResponse>> GetRooms()
        {
            return Ok(new
            {
                data = _roomService.GetRooms()
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