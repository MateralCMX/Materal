namespace RC.ConfigClient.Extensions
{
    /// <summary>
    /// 配置中心构建器扩展
    /// </summary>
    public static class ConfigurationBuilderExtension
    {
        private const string _defaultNameSpace = "Application";
        private static string? _configUrl;
        private static string? _projectName;
        private static int? _reloadSecondInterval;
        /// <summary>
        /// 设置配置中心
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configUrl"></param>
        /// <param name="projectName"></param>
        /// <param name="reloadSecondInterval"></param>
        /// <returns></returns>
        public static IConfigurationBuilder SetConfigCenter(this IConfigurationBuilder builder, string configUrl, string projectName, int reloadSecondInterval = 60)
        {
            _configUrl = configUrl;
            _projectName = projectName;
            _reloadSecondInterval = reloadSecondInterval;
            return builder;
        }
        /// <summary>
        /// 添加命名空间
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configUrl"></param>
        /// <param name="projectName"></param>
        /// <param name="namespace"></param>
        /// <param name="reloadSecondInterval"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddNameSpace(this IConfigurationBuilder builder, string configUrl, string projectName, string @namespace, int reloadSecondInterval = 60)
        {
            SetConfigCenter(builder, configUrl, projectName, reloadSecondInterval);
            builder.Add(new MateralConfigurationSource(@namespace, configUrl, projectName, reloadSecondInterval));
            return builder;
        }
        /// <summary>
        /// 添加命名空间
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="namespace"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigClientException"></exception>
        public static IConfigurationBuilder AddNameSpace(this IConfigurationBuilder builder, string @namespace)
        {
            if (string.IsNullOrWhiteSpace(_configUrl) || !_configUrl.IsUrl() || string.IsNullOrWhiteSpace(_projectName) || _reloadSecondInterval == null)
            {
                throw new MateralConfigClientException($"请先调用IConfigurationBuilder.{nameof(SetConfigCenter)}方法设置环境");
            }
            builder.Add(new MateralConfigurationSource(@namespace, _configUrl, _projectName, _reloadSecondInterval.Value));
            return builder;
        }
        /// <summary>
        /// 添加命名空间
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigClientException"></exception>
        public static IConfigurationBuilder AddNameSpaces(this IConfigurationBuilder builder, params string[] namespaces)
        {
            foreach (string @namespace in namespaces)
            {
                builder.AddNameSpace(@namespace);
            }
            return builder;
        }
        /// <summary>
        /// 添加默认命名空间
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configUrl"></param>
        /// <param name="projectName"></param>
        /// <param name="reloadSecondInterval"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddDefaultNameSpace(this IConfigurationBuilder builder, string configUrl, string projectName, int reloadSecondInterval = 60) => builder.AddNameSpace(configUrl, projectName, _defaultNameSpace, reloadSecondInterval);
        /// <summary>
        /// 添加默认命名空间
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddDefaultNameSpace(this IConfigurationBuilder builder) => builder.AddNameSpace(_defaultNameSpace);
    }
}
