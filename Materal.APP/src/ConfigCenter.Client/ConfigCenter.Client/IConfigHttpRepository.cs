using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;

namespace ConfigCenter.Client
{
    public interface IConfigRepository
    {
        Task<List<ConfigurationItemListDTO>> GetAllConfigurationItemAsync(string address, string projectName, string[] namespaceNames);
    }
}
