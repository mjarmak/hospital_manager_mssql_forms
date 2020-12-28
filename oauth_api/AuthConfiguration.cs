using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace authentication_api

{
    public static class AuthConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId {
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.FamilyName,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Gender,
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.BirthDate,
                        JwtClaimTypes.Role
                    }
                }
            };

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource> {
                new ApiResource("hm", new[] {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.FamilyName,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Gender,
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.BirthDate,
                        JwtClaimTypes.Role
                } ),
                new ApiResource("auth", new[] { JwtClaimTypes.Role, JwtClaimTypes.Email })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                new Client
                {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { "hm", "auth", IdentityServerConstants.StandardScopes.OpenId },
                    RequireConsent = false,
                    AlwaysIncludeUserClaimsInIdToken = true,
                }
            };
        }
    }
}
