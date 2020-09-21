using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using Materal.StringHelper;
using Microsoft.Extensions.Configuration;

namespace ConfigCenter.Client
{
    public class MateralConfigurationBuilder : ConfigurationBuilder, IMateralConfigurationBuilder
    {
        private readonly string _configUrl;
        private readonly string _projectName;
        private readonly List<string> _namespaces = new List<string>();
        public MateralConfigurationBuilder(string configUrl, string projectName)
        {
            if (!configUrl.IsUrl()) throw new MateralConfigCenterClientException("Url地址不正确");
            if (string.IsNullOrEmpty(projectName)) throw new MateralConfigCenterClientException("applicationName名称不能为空");
            _configUrl = configUrl;
            _projectName = projectName;
        }
        public IMateralConfigurationBuilder AddNamespace(string @namespace)
        {
            _namespaces.Add(@namespace);
            return this;
        }
        public IMateralConfigurationBuilder AddDefaultNamespace()
        {
            return AddNamespace("Application");
        }
        public async Task<IConfigurationRoot> BuildMateralConfigAsync()
        {
            IConfigRepository configRepository = new ConfigHttpRepositoryImpl();
            List<ConfigurationItemListDTO> configurationItems = await configRepository.GetAllConfigurationItemAsync(_configUrl, _projectName, _namespaces.ToArray());
            foreach (IGrouping<string, ConfigurationItemListDTO> grouping in configurationItems.GroupBy(m=>m.NamespaceName))
            {
                var configs = new Dictionary<string, string>();
                foreach (ConfigurationItemListDTO item in grouping.ToList())
                {
                    configs[item.Key] = item.Value;
                }
                Add(new MateralConfigurationSource(configs, grouping.Key));
            }
            return Build();
        }
    }
}
