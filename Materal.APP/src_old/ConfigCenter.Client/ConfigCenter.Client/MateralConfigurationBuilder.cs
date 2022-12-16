using Materal.StringHelper;
using Microsoft.Extensions.Configuration;

namespace ConfigCenter.Client
{
    public class MateralConfigurationBuilder : ConfigurationBuilder, IMateralConfigurationBuilder
    {
        private readonly string _configUrl;
        private readonly string _projectName;
        private readonly int _reloadSecondInterval;
        public MateralConfigurationBuilder(string configUrl, string projectName, int reloadSecondInterval = 30)
        {
            if (!configUrl.IsUrl()) throw new MateralConfigCenterClientException("Url地址不正确");
            _configUrl = configUrl;
            _projectName = projectName;
            _reloadSecondInterval = reloadSecondInterval;
        }
        public IMateralConfigurationBuilder AddNamespace(string @namespace)
        {
            Add(new MateralConfigurationSource(@namespace, _configUrl, _projectName, _reloadSecondInterval));
            return this;
        }
        public IMateralConfigurationBuilder AddDefaultNamespace() => AddNamespace("Application");
    }
}
