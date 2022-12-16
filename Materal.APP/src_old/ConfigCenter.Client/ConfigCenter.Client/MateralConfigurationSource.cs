using Microsoft.Extensions.Configuration;

namespace ConfigCenter.Client
{

    public class MateralConfigurationSource : IConfigurationSource
    {
        private readonly string _namespaceName;
        private readonly string _configUrl;
        private readonly string _projectName;
        private readonly int _reloadSecondInterval;
        public MateralConfigurationSource(string namespaceName, string configUrl, string projectName, int reloadSecondInterval)
        {
            _namespaceName = namespaceName;
            _configUrl = configUrl;
            _projectName = projectName;
            _reloadSecondInterval = reloadSecondInterval;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder) => new MateralConfigurationProvider(_namespaceName, _configUrl, _projectName, _reloadSecondInterval);
    }
}
