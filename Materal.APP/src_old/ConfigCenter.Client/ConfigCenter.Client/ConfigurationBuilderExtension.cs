using Materal.StringHelper;
using Microsoft.Extensions.Configuration;

namespace ConfigCenter.Client
{
    public static class ConfigurationBuilderExtension
    {
        private const string _defaultNameSpace = "Application";
        private static string? _configUrl;
        private static string? _projectName;
        private static int? _reloadSecondInterval;
        public static IConfigurationBuilder SetConfigCenter(this IConfigurationBuilder builder, string configUrl, string projectName, int reloadSecondInterval = 60)
        {
            _configUrl = configUrl;
            _projectName = projectName;
            _reloadSecondInterval = reloadSecondInterval;
            return builder;
        }
        public static IConfigurationBuilder AddNameSpace(this IConfigurationBuilder builder, string configUrl, string projectName, string @namespace, int reloadSecondInterval = 60)
        {
            _configUrl = configUrl;
            _projectName = projectName;
            _reloadSecondInterval = reloadSecondInterval;
            builder.Add(new MateralConfigurationSource(@namespace, configUrl, projectName, reloadSecondInterval));
            return builder;
        }
        public static IConfigurationBuilder AddNameSpace(this IConfigurationBuilder builder, string @namespace)
        {
            if (string.IsNullOrWhiteSpace(_configUrl) || !_configUrl.IsUrl() || string.IsNullOrWhiteSpace(_projectName) || _reloadSecondInterval == null)
            {
                throw new MateralConfigCenterClientException($"请先调用IConfigurationBuilder.{nameof(SetConfigCenter)}方法设置环境");
            }
            builder.Add(new MateralConfigurationSource(@namespace, _configUrl, _projectName, _reloadSecondInterval.Value));
            return builder;
        }
        public static IConfigurationBuilder AddDefaultNameSpace(this IConfigurationBuilder builder, string configUrl, string projectName, int reloadSecondInterval = 60) => builder.AddNameSpace(configUrl, projectName, _defaultNameSpace, reloadSecondInterval);
        public static IConfigurationBuilder AddDefaultNameSpace(this IConfigurationBuilder builder) => builder.AddNameSpace(_defaultNameSpace);
    }
}
