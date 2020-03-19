using Materal.ConfigCenter.ConfigServer.DataTransmitModel.ConfigurationItem;
using Materal.ConfigCenter.ConfigServer.PresentationModel.ConfigurationItem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.Client
{
    public class ConfigHttpRepositoryImpl : HttpRepositoryImpl, IConfigRepository
    {

        public async Task<List<ConfigurationItemListDTO>> GetAllConfigurationItemAsync(string address, string projectName, string[] namespaceNames)
        {
            var data = new QueryConfigurationItemFilterModel
            {
                ProjectName = projectName,
                NamespaceNames = namespaceNames
            };
            List<ConfigurationItemListDTO> result = await SendPostAsync<List<ConfigurationItemListDTO>>($"{address}api/ConfigurationItem/GetConfigurationItemList", data);
            return result;
        }
    }
}
