using System;
using System.Collections.Generic;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;
using System.Threading.Tasks;
using Materal.ConfigCenter.ConfigServer.DataTransmitModel.ConfigurationItem;
using Materal.ConfigCenter.ConfigServer.PresentationModel.ConfigurationItem;

namespace Materal.ConfigCenter.ProtalServer.HttpRepository
{
    public class ConfigServerRepositoryImpl : HttpRepositoryImpl, IConfigServerRepository
    {
        public async Task HealthAsync(string address)
        {
            await SendGetAsync($"{address}api/Health/Health");
        }
        public async Task DeleteProjectAsync(string address, string token, Guid id)
        {
            Dictionary<string, string> heads = GetDefaultHeads();
            heads["Authorization"] = $"Bearer {token}";
            var data = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };
            await SendGetAsync($"{address}api/ConfigurationItem/DeleteConfigurationItemByProjectID", data, heads);
        }
        public async Task DeleteNamespaceAsync(string address, string token, Guid id)
        {
            Dictionary<string, string> heads = GetDefaultHeads();
            heads["Authorization"] = $"Bearer {token}";
            var data = new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            };
            await SendGetAsync($"{address}api/ConfigurationItem/DeleteConfigurationItemByNamespaceID", data, heads);
        }

        public async Task<List<ConfigurationItemListDTO>> GetConfigurationItemAsync(QueryConfigurationItemFilterModel filterModel, string address)
        {
            List<ConfigurationItemListDTO> result = await SendPostAsync<List<ConfigurationItemListDTO>>($"{address}api/ConfigurationItem/GetConfigurationItemList", filterModel);
            return result;
        }

        public async Task InitConfigurationItemsAsync(string address, string token, List<AddConfigurationItemModel> model)
        {
            Dictionary<string, string> heads = GetDefaultHeads();
            heads["Authorization"] = $"Bearer {token}";
            await SendPostAsync($"{address}api/ConfigurationItem/InitConfigurationItems", model, heads);
        }

        public async Task InitConfigurationItemsByNamespaceAsync(string address, string token, InitConfigurationItemsByNamespaceModel model)
        {
            Dictionary<string, string> heads = GetDefaultHeads();
            heads["Authorization"] = $"Bearer {token}";
            await SendPostAsync($"{address}api/ConfigurationItem/InitConfigurationItemsByNamespace", model, heads);
        }
    }
}
