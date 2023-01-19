using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using IdentityServer4.Models;

namespace Authority.IdentityServer
{
    public class IdentityConfig
    {
        /// <summary>
        /// 获得认证服务器资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }
        /// <summary>
        /// 获得API
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetAPIs()
        {
            return new List<ApiResource>
            {
                new ApiResource("WebAPI","WebAPI")
            };
        }
        /// <summary>
        /// 获得客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            var result = new List<Client>
            {
                new Client
                {
                    ClientId = ClientType.Web.ToString(),
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedScopes = {"WebAPI"},
                    AccessTokenLifetime = 10 * 60 * 60
                }
            };
            return result;
        }
    }
}
