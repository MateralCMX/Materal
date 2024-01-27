using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// Web配置服务上下文
    /// </summary>
    public class WebConfigServiceContext(WebApplicationBuilder hostBuilder) : ConfigServiceContext(hostBuilder.Services, hostBuilder.Configuration), IWebConfigServiceContext
    {
        /// <summary>
        /// 主机构建器
        /// </summary>
        public WebApplicationBuilder HostBuilder { get; } = hostBuilder;
    }
}
