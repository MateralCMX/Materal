using Microsoft.Extensions.Configuration;

namespace RC.ConfigClient
{

    public class MateralConfigurationSource : IConfigurationSource
    {
        private readonly string _namespaceName;
        private readonly string _configUrl;
        private readonly string _projectName;
        private readonly int _reloadSecondInterval;
        private readonly bool _isAbsoluteUrl;
        public MateralConfigurationSource(string namespaceName, string configUrl, string projectName, int reloadSecondInterval, bool isAbsoluteUrl = false)
        {
            _namespaceName = namespaceName;
            _configUrl = configUrl;
            _projectName = projectName;
            _reloadSecondInterval = reloadSecondInterval;
            _isAbsoluteUrl = isAbsoluteUrl;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder) => new MateralConfigurationProvider(_namespaceName, _configUrl, _projectName, _reloadSecondInterval, _isAbsoluteUrl);
    }
}
