namespace RC.ConfigClient
{
    /// <summary>
    /// 配置源
    /// </summary>
    public class MateralConfigurationSource(string namespaceName, string configUrl, string projectName, int reloadSecondInterval) : IConfigurationSource
    {
        /// <summary>
        /// 构建配置提供者
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder) => new MateralConfigurationProvider(namespaceName, configUrl, projectName, reloadSecondInterval);
    }
}
