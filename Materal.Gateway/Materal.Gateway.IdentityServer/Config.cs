using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;
using Materal.Gateway.Common;

namespace Materal.Gateway.IdentityServer
{
    public class Config
    {
        public IEnumerable<IdentityResource> Ids => new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

        public IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource(ApplicationConfig.JWTConfig.Scope, ApplicationConfig.JWTConfig.Scope)
        };
        public IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope(ApplicationConfig.JWTConfig.Scope, ApplicationConfig.JWTConfig.Scope)
        };

        public IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                Enabled = true,
                ClientId = ApplicationConfig.JWTConfig.ClientID,
                ClientSecrets = { new Secret(ApplicationConfig.JWTConfig.ClientSecret.Sha256()) },
                RequireClientSecret = true,
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowOfflineAccess = true,
                AllowedScopes = { ApplicationConfig.JWTConfig.Scope },
                IdentityTokenLifetime = 28800,
                AccessTokenLifetime = 28800,
                AuthorizationCodeLifetime = 28800
            }
        };
    }
}