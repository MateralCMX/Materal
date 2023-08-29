using Microsoft.Extensions.Configuration;

namespace RC.ConfigClient
{
    /// <summary>
    /// 配置源
    /// </summary>
    public class MateralConfigurationSource : IConfigurationSource
    {
        private readonly string _namespaceName;
        private readonly string _configUrl;
        private readonly string _projectName;
        private readonly int _reloadSecondInterval;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <param name="configUrl"></param>
        /// <param name="projectName"></param>
        /// <param name="reloadSecondInterval"></param>
        public MateralConfigurationSource(string namespaceName, string configUrl, string projectName, int reloadSecondInterval)
        {
            _namespaceName = namespaceName;
            _configUrl = configUrl;
            _projectName = projectName;
            _reloadSecondInterval = reloadSecondInterval;
        }
        /// <summary>
        /// 构建配置提供者
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder) => new MateralConfigurationProvider(_namespaceName, _configUrl, _projectName, _reloadSecondInterval);
    }
}
