using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock
{
    /// <summary>
    /// 配置服务上下文
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="services"></param>
    public class ConfigServiceContext(IConfiguration configuration, IServiceCollection services) : IConfigServiceContext
    {
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
        public IMvcBuilder? MvcBuilder { get; set; }
    }
}
