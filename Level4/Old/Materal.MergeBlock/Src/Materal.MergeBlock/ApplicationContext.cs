using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public class ApplicationContext(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider) : IApplicationContext
    {
        /// <summary>
        /// 应用程序构建器
        /// </summary>
        public IApplicationBuilder ApplicationBuilder { get; } = applicationBuilder;
        /// <summary>
        /// 服务容器
        /// </summary>
        public IServiceProvider ServiceProvider { get; } = serviceProvider;
    }
}
