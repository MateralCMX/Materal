using IdentityModel.Client;
using Materal.CacheHelper;
using Materal.ConvertHelper;
using Materal.DateTimeHelper;
using Materal.Gateway.Common;
using Materal.NetworkHelper;
using Ocelot.Configuration.File;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Materal.Gateway.Common.Models;

namespace Materal.Gateway.WebAPI.Services
{
    public abstract class BaseServices
    {
        protected readonly ICacheManager CacheManager;

        protected BaseServices(ICacheManager cacheManager)
        {
            CacheManager = cacheManager;
        }
        /// <summary>
        /// 获得配置
        /// </summary>
        /// <returns></returns>
        protected async Task<FileConfiguration> GetFileConfigurationAsync()
        {
            try
            {
                string url = $"{ApplicationConfig.BaseUrl}{ApplicationConfig.OcelotConfig.AdministrationUrl}/configuration";
                Dictionary<string, string> heads = await GetHttpHeadsAsync();
                string resultText = await HttpManager.SendGetAsync(url, null, heads, Encoding.UTF8);
                FileConfiguration fileConfiguration = resultText.JsonToObject<FileConfiguration>();
                return fileConfiguration;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new GatewayException("获取配置失败", exception);
            }
        }
        /// <summary>
        /// 设置配置
        /// </summary>
        /// <param name="fileConfiguration"></param>
        /// <returns></returns>
        protected async Task SetFileConfigurationAsync(FileConfiguration fileConfiguration)
        {
            try
            {
                string url = $"{ApplicationConfig.BaseUrl}{ApplicationConfig.OcelotConfig.AdministrationUrl}/configuration";
                Dictionary<string, string> heads = await GetHttpHeadsAsync();
                await HttpManager.SendPostAsync(url, fileConfiguration, null, heads, Encoding.UTF8);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new GatewayException("设置配置失败", exception);
            }
        }
        /// <summary>
        /// 获得头
        /// </summary>
        /// <returns></returns>
        protected async Task<Dictionary<string, string>> GetHttpHeadsAsync()
        {
            Dictionary<string, string> result = new Dictionary<string, string>
            {
                ["Authorization"] = $"Bearer {await GetTokenAsync()}",
                ["Content-Type"] = "application/json"
            };
            return result;
        }
        /// <summary>
        /// 获得Token
        /// </summary>
        /// <returns></returns>
        protected async Task<string> GetTokenAsync()
        {
            const string tokenKey = "MateralGatewayAdministrationToken";
            string token = CacheManager.Get<string>(tokenKey);
            if (!string.IsNullOrWhiteSpace(token)) return token;
            string url = $"{ApplicationConfig.BaseUrl}{ApplicationConfig.OcelotConfig.AdministrationUrl}";
            DiscoveryDocumentResponse disco = await HttpManager.HttpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = url,
                Policy =
                {
                    ValidateIssuerName = false,
                    ValidateEndpoints = false,
                    RequireHttps = false
                }
            });
            if (disco.IsError)
            {
                throw new GatewayException(disco.Error);
            }
            TokenResponse tokenResponse = await HttpManager.HttpClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "admin",
                    ClientSecret = ApplicationConfig.OcelotConfig.ClientSecret,
                    Scope = "admin",
                    GrantType = "client_credentials"
                });
            if (tokenResponse.IsError)
            {
                throw new GatewayException(tokenResponse.Error);
            }
            token = tokenResponse.AccessToken;
            CacheManager.SetByAbsolute(tokenKey, token, tokenResponse.ExpiresIn - 10, DateTimeTypeEnum.Second);
            return token;
        }
    }
}
