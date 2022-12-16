using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;

namespace ConfigCenter.Client
{
    public interface IConfigRepository
    {
        Task<List<ConfigurationItemListDTO>> GetConfigurationItemsAsync(string address, string projectName, params string[] namespaceNames);
    }
}
