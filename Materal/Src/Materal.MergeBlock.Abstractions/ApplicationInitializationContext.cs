using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 应用初始化上下文
    /// </summary>
    public class ApplicationInitializationContext(IHost host)
    {
        /// <summary>
        /// 主机
        /// </summary>
        public IHost Host { get; } = host;
        /// <summary>
        /// 服务提供者
        /// </summary>
        public IServiceProvider ServiceProvider => Host.Services;
    }
}
