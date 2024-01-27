using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.Abstractions.ConsoleModule
{
    /// <summary>
    /// 控制台配置服务上下文
    /// </summary>
    public interface IConsoleConfigServiceContext : IConfigServiceContext
    {
        /// <summary>
        /// 主机构建器
        /// </summary>
        HostApplicationBuilder HostBuilder { get; }
    }
}
