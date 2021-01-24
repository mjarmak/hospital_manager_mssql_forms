using hospital_manager_ui.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace hospital_manager_ui.Util
{
    public class AuthUtil
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public AuthUtil()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public void DecodeToken(string token)
        {
            if (token == null)
            {
                return;
            }

            try
            {
                var accessToken = _tokenHandler.ReadToken(token) as JwtSecurityToken;

                AuthConfiguration.AccessToken = token;
                AuthConfiguration.Email = accessToken.Claims.Single(claim => claim.Type == "email").Value;
                AuthConfiguration.Name = accessToken.Claims.Single(claim => claim.Type == "name").Value;
                AuthConfiguration.LastName = accessToken.Claims.Single(claim => claim.Type == "family_name").Value;
                AuthConfiguration.Gender = accessToken.Claims.Single(claim => claim.Type == "gender").Value;
                AuthConfiguration.Phone = accessToken.Claims.Single(claim => claim.Type == "phone_number").Value;
                AuthConfiguration.Birthdate = accessToken.Claims.Single(claim => claim.Type == "birthdate").Value;
                AuthConfiguration.Role = accessToken.Claims.Single(claim => claim.Type == "role").Value;
            }
            catch (ArgumentException)
            {
                return;
            }
        }
    }
}
