using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 配置服务上下文
    /// </summary>
    public interface IConfigServiceContext
    {
        /// <summary>
        /// 主机构建器
        /// </summary>
        IHostBuilder HostBuilder { get; }
        /// <summary>
        /// 配置对象
        /// </summary>
        IConfiguration Configuration { get; }
        /// <summary>
        /// 服务容器
        /// </summary>
        IServiceCollection Services { get; }
        /// <summary>
        /// MVC构建器
        /// </summary>
        IMvcBuilder? MvcBuilder { get; }
        /// <summary>
        /// 模组信息
        /// </summary>
        List<IMergeBlockModuleInfo> ModuleInfos { get; }
    }
}
