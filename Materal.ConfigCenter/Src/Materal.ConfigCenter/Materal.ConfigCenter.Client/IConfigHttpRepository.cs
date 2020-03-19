using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.ConfigCenter.ConfigServer.DataTransmitModel.ConfigurationItem;

namespace Materal.ConfigCenter.Client
{
    public interface IConfigRepository
    {
        Task<List<ConfigurationItemListDTO>> GetAllConfigurationItemAsync(string address, string projectName, string[] namespaceNames);
    }
}
