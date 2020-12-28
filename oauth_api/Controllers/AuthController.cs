using IdentityModel;
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
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register(UserAccountModel userAccountModel)
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

            return Ok(new
            {
                data = result.ToString()
            });
        }


        [Route("register/doctor")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public async Task<ActionResult> RegisterDoctor(UserAccountModel userAccountModel)
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
            var result = await _userManager.CreateAsync(user, userAccountDataModel.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    data = new List<string> { result.Errors.ToList()[0].Description }
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

            return Ok(new
            {
                data = result.ToString()
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public ActionResult<IEnumerable<IdentityUser>> GetAllUsers(string role)
        {
            if (role == null)
            {
                return Ok(new
                {
                    data = _userManager.Users
                });
            }

            return Ok(new
            {
                data = _userManager.GetUsersInRoleAsync(role).Result
            });
        }

        /// <summary>
        /// Search for the user based on role
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("user/username")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "ADMIN")]
        public async Task<ActionResult> GetUsersByUserName(string username)
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
                data = user
            });
        }


        /// <summary>
        /// Remove role
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
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

        private static UserAccountDataModel EnvelopeOf(UserAccountModel userAccountModel)
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

        private static string GetUserName(UserAccountModel userAccountModel)
        {
            return userAccountModel.Name + userAccountModel.Surname + userAccountModel.BirthDate.Replace("/", "");
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
