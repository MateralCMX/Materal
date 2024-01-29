using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock.Abstractions.WebModule
{
    /// <summary>
    /// Web配置服务上下文
    /// </summary>
    public interface IWebConfigServiceContext : IConfigServiceContext
    {
        /// <summary>
        /// 主机构建器
        /// </summary>
        WebApplicationBuilder HostBuilder { get; }
        /// <summary>
        /// MVC构建器
        /// </summary>
        IMvcBuilder MvcBuilder { get; }
    }
}
