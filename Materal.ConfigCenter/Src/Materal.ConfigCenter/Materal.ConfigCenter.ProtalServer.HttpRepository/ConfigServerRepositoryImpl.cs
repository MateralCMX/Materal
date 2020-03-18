using System;
using System.Collections.Generic;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ProtalServer.HttpRepository
{
    public class ConfigServerRepositoryImpl : HttpRepositoryImpl, IConfigServerRepository
    {
        public async Task HealthAsync(string address)
        {
            await SendGetAsync($"{address}api/Health/Health");
        }
        public async Task DeleteProjectAsync(string address, Guid id)
        {
            var data = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };
            await SendGetAsync($"{address}api/ConfigurationItem/DeleteConfigurationItemByProjectID", data);
        }
        public async Task DeleteNamespaceAsync(string address, Guid id)
        {
            var data = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };
            await SendGetAsync($"{address}api/ConfigurationItem/DeleteConfigurationItemByNamespaceID", data);
        }
    }
}
