using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock
{
    /// <summary>
    /// 配置服务上下文
    /// </summary>
    public class ConfigServiceContext(IHostBuilder hostBuilder, IConfiguration configuration, IServiceCollection services, List<IMergeBlockModuleInfo> moduleInfos) : IConfigServiceContext
    {
        /// <summary>
        /// 主机构建器
        /// </summary>
        public IHostBuilder HostBuilder { get; } = hostBuilder;
        /// <summary>
        /// 配置对象
        /// </summary>
        public IConfiguration Configuration { get; } = configuration;
        /// <summary>
        /// 服务容器
        /// </summary>
        public IServiceCollection Services { get; } = services;
        /// <summary>
        /// MVC构建器
        /// </summary>
        public IMvcBuilder? MvcBuilder { get; internal set; }
        /// <summary>
        /// 模组信息
        /// </summary>
        public List<IMergeBlockModuleInfo> ModuleInfos { get; } = moduleInfos;
    }
}
