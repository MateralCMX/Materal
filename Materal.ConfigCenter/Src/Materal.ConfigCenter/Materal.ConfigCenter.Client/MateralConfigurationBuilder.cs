using Materal.StringHelper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Materal.ConfigCenter.Client
{
    public class MateralConfigurationBuilder : ConfigurationBuilder, IMateralConfigurationBuilder
    {
        private string _configUrl;
        private string _applicationName;
        private List<string> _namespaces = new List<string>();
        public MateralConfigurationBuilder(string configUrl, string applicationName)
        {
            if (!configUrl.IsUrl()) throw new MateralConfigCenterClientException("Url地址不正确");
            if (!string.IsNullOrEmpty(applicationName)) throw new MateralConfigCenterClientException("applicationName名称不能为空");
            _configUrl = configUrl;
            _applicationName = applicationName;
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
        public IConfigurationRoot BuildMateralConfig()
        {
            var configs = new Dictionary<string, string>
            {
                ["TestConfig1"] = "value1",
                ["TestConfig2"] = "value2"
            };
            Add(new MateralConfigurationSource(configs));
            return Build();
        }
    }
}
