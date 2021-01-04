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
        public async System.Threading.Tasks.Task<ActionResult<DoctorResponse>> SaveRoomAsync(UserAccountRequest userAccountRequest)
        {
            if (userAccountRequest.DoctorRequest == null)
            {
                return BadRequest(new
                {
                    data = "Doctor object is missing."
                });
            }

            var url = "https://localhost:44321/register/doctor";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetAccessToken());
            var json = JsonConvert.SerializeObject(userAccountRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, data);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return BadRequest(new
                {
                    data = response.ReasonPhrase
                }); ;
            }
            string result = response.Content.ReadAsStringAsync().Result;
            JObject jObject = JObject.Parse(result);
            string username = jObject.GetValue("username").ToString();
            userAccountRequest.DoctorRequest.Username = username;
            userAccountRequest.DoctorRequest.Name = userAccountRequest.Name + " " + userAccountRequest.Surname;
            return Ok(new
            {
                data = _doctorService.SaveDoctor(userAccountRequest.DoctorRequest)
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