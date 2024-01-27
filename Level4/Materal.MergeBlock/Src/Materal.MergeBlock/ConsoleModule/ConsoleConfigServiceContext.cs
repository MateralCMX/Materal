using Materal.MergeBlock.Abstractions.ConsoleModule;
using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.ConsoleModule
{
    /// <summary>
    /// 控制台配置服务上下文
    /// </summary>
    /// <param name="hostBuilder"></param>
    public class ConsoleConfigServiceContext(HostApplicationBuilder hostBuilder) : ConfigServiceContext(hostBuilder.Services, hostBuilder.Configuration), IConsoleConfigServiceContext
    {
        /// <summary>
        /// 主机构建器
        /// </summary>
        public HostApplicationBuilder HostBuilder { get; } = hostBuilder;
    }
}
