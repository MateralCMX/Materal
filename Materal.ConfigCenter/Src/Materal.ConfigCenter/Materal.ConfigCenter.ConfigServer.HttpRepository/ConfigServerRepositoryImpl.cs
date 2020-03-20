using Materal.ConfigCenter.ConfigServer.Common;
using Materal.ConfigCenter.ConfigServer.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ConfigServer.HttpRepository
{
    public class ConfigServerRepositoryImpl : HttpRepositoryImpl, IConfigServerRepository
    {
        public async Task RegisterAsync()
        {
            Dictionary<string, string> heads = GetDefaultHeads();
            heads["Referer"] = $"http://{ApplicationConfig.ServerConfig.Host}:{ApplicationConfig.ServerConfig.Port}/";
            var data = new Dictionary<string, string>
            {
                ["name"] = ApplicationConfig.ProtalServerConfig.Name
            };
            await SendGetAsync($"{ApplicationConfig.ProtalServerConfig.ProtalUrl}api/ConfigServer/Register", data, heads);
        }
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        public async Task HealthAsync()
        {
            await SendGetAsync($"{ApplicationConfig.ProtalServerConfig.ProtalUrl}api/Health/Health");
        }
    }
}
