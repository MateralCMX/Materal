using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 配置服务上下文
    /// </summary>
    public interface IConfigServiceContext
    {
        /// <summary>
        /// 配置对象
        /// </summary>
        ConfigurationManager Configuration { get; }
        /// <summary>
        /// 服务容器
        /// </summary>
        IServiceCollection Services { get; }
        /// <summary>
        /// 服务提供者
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}
