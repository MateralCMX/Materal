﻿using Materal.ConfigCenter.ConfigServer.Common;
using Materal.ConfigCenter.ConfigServer.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ConfigServer.HttpRepository
{
    public class ConfigServerRepositoryImpl : HttpRepositoryImpl, IConfigServerRepository
    {
        public async Task RegisterAsync()
        {
            var heads = new Dictionary<string, string>
            {
                ["Content-Type"] = "application/json",
                ["Referer"] = $"http://{ApplicationConfig.ServerConfig.Host}:{ApplicationConfig.ServerConfig.Port}"
            };
            var data = new Dictionary<string, string>
            {
                ["name"] = ApplicationConfig.ConfigServerConfig.Name
            };
            await SendGetAsync($"{ApplicationConfig.ConfigServerConfig.ProtalUrl}/api/ConfigServer/Register", data, heads);
        }
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        public async Task HealthAsync()
        {
            await SendGetAsync($"{ApplicationConfig.ConfigServerConfig.ProtalUrl}/api/Health/Health");
        }
    }
}
