﻿using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hospital_manager_models.Models;
using static hospital_manager_models.Models.UserAccountRequest;
using authentication_api.Service;

namespace authentication_api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class AuthController : Controller
    {
        public SignInManager<IdentityUser> InManager { get; }
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        public RoleManager<IdentityRole> RoleManager { get; }
        public IIdentityServerInteractionService InteractionService { get; }
        private readonly EmailService emailService;

        public AuthController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IIdentityServerInteractionService interactionService)
        {
            InManager = signInManager;
            _userManager = userManager;
            RoleManager = roleManager;
            InteractionService = interactionService;
            _tokenHandler = new JwtSecurityTokenHandler();
            emailService = new EmailService();
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register(UserAccountRequest userAccountModel)
        {
            UserAccountDataModel userAccountDataModel;
            try
            {
                userAccountDataModel = EnvelopeOf(userAccountModel);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new
                {
                    data = new List<string> { "Invalid paramaters, " + e.Message }
                });
            }

            var username = GetUserName(userAccountModel);
            var user = new IdentityUser(username);
            user.Email = userAccountModel.Email;
            user.PhoneNumber = userAccountModel.Phone;
            var result = await _userManager.CreateAsync(user, userAccountDataModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = new List<string> { result.Errors.ToList()[0].Description }
                });
            }
            await _userManager.AddToRoleAsync(user, "PATIENT");
            if (userAccountDataModel.Email != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Email, userAccountDataModel.Email));
            }
            if (userAccountDataModel.Name != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Name, userAccountDataModel.Name));
            }
            if (userAccountDataModel.Surname != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.FamilyName, userAccountDataModel.Surname));
            }
            if (userAccountDataModel.Gender != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Gender, userAccountDataModel.Gender));
            }
            if (userAccountDataModel.Phone != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.PhoneNumber, userAccountDataModel.Phone));
            }
            if (userAccountDataModel.BirthDate != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.BirthDate, userAccountDataModel.BirthDate));
            }
            emailService.SendEmail(
                    userAccountDataModel.Email,
                    "Account Creation Comfirmation",
                    "Username: " + username + "\nPassword: " + userAccountDataModel.Password);

            return Ok(new
            {
                data = result.ToString(),
                username
            });
        }


        [Route("register/doctor")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public async Task<ActionResult> RegisterDoctor(UserAccountRequest userAccountModel)
        {
            UserAccountDataModel userAccountDataModel;
            try
            {
                userAccountDataModel = EnvelopeOf(userAccountModel);
            }
            catch (ArgumentException e)
            {
                return BadRequest(new
                {
                    data = new List<string> { "Invalid paramaters, " + e.Message }
                });
            }

            var username = GetUserName(userAccountModel);
            var user = new IdentityUser(username);
            user.Email = userAccountModel.Email;
            user.PhoneNumber = userAccountModel.Phone;
            var result = await _userManager.CreateAsync(user, userAccountDataModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = result.Errors.ToList()[0].Description
                });
            }
            await _userManager.AddToRoleAsync(user, "DOCTOR");
            if (userAccountDataModel.Email != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Email, userAccountDataModel.Email));
            }
            if (userAccountDataModel.Name != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Name, userAccountDataModel.Name));
            }
            if (userAccountDataModel.Surname != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.FamilyName, userAccountDataModel.Surname));
            }
            if (userAccountDataModel.Gender != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Gender, userAccountDataModel.Gender));
            }
            if (userAccountDataModel.Phone != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.PhoneNumber, userAccountDataModel.Phone));
            }
            if (userAccountDataModel.BirthDate != null)
            {
                await _userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.BirthDate, userAccountDataModel.BirthDate));
            }

            emailService.SendEmail(
                    userAccountDataModel.Email,
                    "Account Creation Comfirmation",
                    "Username: " + username + "\nPassword: " + userAccountDataModel.Password);

            return Ok(new
            {
                data = result.ToString(),
                username
            });
        }


        [Route("roles")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<IEnumerable<IdentityUser>> GetAllRoles()
        {
            return Ok(new
            {
                data = RoleManager.Roles.ToList().Select(role => role.Name).ToList(),
            });
        }

        [Route("user")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN,DOCTOR")]
        public ActionResult<List<UserDetailsResponse>> GetAllUsers(string role)
        {
            List<UserDetailsResponse> users;

            if (role == null)
            {
                users = _userManager.Users.Select(user => new UserDetailsResponse()
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Phone = user.PhoneNumber
                }).ToList();
            }
            else
            {
                users = _userManager.GetUsersInRoleAsync(role).Result.Select(user => new UserDetailsResponse()
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Phone = user.PhoneNumber
                }).ToList(); ;
            }
                return Ok(new
            {
                data = users
                });
        }

        [Route("user/username")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public async Task<ActionResult<UserDetailsResponse>> GetUserByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest(new
                {
                    data = new List<string> { "User " + username + " doesn't exist" }
                });
            }
            UserDetailsResponse userDetailsResponse = new UserDetailsResponse();
            userDetailsResponse.Username = user.UserName;
            userDetailsResponse.Email = user.Email;
            userDetailsResponse.Phone = user.PhoneNumber;
            return Ok(new
            {
                data = userDetailsResponse
            });
        }

        [Route("user/email")]
        [HttpGet]
        public async Task<ActionResult<UserDetailsResponse>> GetUserEmailByUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest(new
                {
                    data = new List<string> { "User " + username + " doesn't exist" }
                });
            }
            return Ok(new
            {
                email = user.Email
            });
        }

        [Route("user/exists")]
        [HttpGet]
        public async Task<ActionResult> GetUserExists(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest(new
                {
                    data = new List<string> { "User " + username + " doesn't exist" }
                });
            }
            return Ok(new
            {
                data = "OK"
            });
        }

        [Route("user/make-admin")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public async Task<ActionResult> MakeUserAdmin(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return BadRequest(new
                {
                    data = new List<string> { "User " + username + " doesn't exist" }
                });
            }
            await _userManager.RemoveFromRoleAsync(user, "BLOCKED");
            await _userManager.RemoveFromRoleAsync(user, "REVIEWING");
            await _userManager.AddToRoleAsync(user, "USER");
            var result = await _userManager.AddToRoleAsync(user, "ADMIN");
            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = new List<string> { result.Errors.ToList()[0].Description }
                });
            }
            return Ok(new
            {
                data = result.ToString()
            });
        }

        private static UserAccountDataModel EnvelopeOf(UserAccountRequest userAccountModel)
        {
            return new UserAccountDataModel
            {
                Email = userAccountModel.Email,
                Name = userAccountModel.Name,
                Surname = userAccountModel.Surname,
                BirthDate = userAccountModel.BirthDate,
                Phone = userAccountModel.Phone,
                Gender = userAccountModel.Gender == null ? null : ((UserGenderEnum)Enum.Parse(typeof(UserGenderEnum), userAccountModel.Gender)).ToString(),
                Password = userAccountModel.Password
            };
        }

        private static string GetUserName(UserAccountRequest userAccountModel)
        {
            string result = userAccountModel.Name + userAccountModel.Surname + userAccountModel.BirthDate.Replace("/", "");
            result = result.Replace(":", "");
            result = result.Replace(" ", "");
            return result;
        }

        private string GetClaim(string name)
        {
            var accessTokenString = Request.Headers[HeaderNames.Authorization].ToString();

            if (accessTokenString == null || !accessTokenString.Contains("Bearer "))
            {
                return null;
            }

            try
            {
                var accessToken = _tokenHandler.ReadToken(accessTokenString.Replace("Bearer ", "")) as JwtSecurityToken;
                return accessToken.Claims.Single(claim => claim.Type == name).Value;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
