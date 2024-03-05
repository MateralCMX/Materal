namespace Materal.MergeBlock
{
    /// <summary>
    /// 配置服务上下文
    /// </summary>
    public abstract class ConfigServiceContext(IServiceCollection services, ConfigurationManager configuration) : IConfigServiceContext
    {
        /// <summary>
        /// 配置管理器
        /// </summary>
        public ConfigurationManager Configuration { get; } = configuration;
        /// <summary>
        /// 服务集合
        /// </summary>
        public IServiceCollection Services { get; } = services;
        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider { get; } = services.BuildMateralServiceProvider();
    }
}
