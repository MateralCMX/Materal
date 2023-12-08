using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        /// 应用程序构建器
        /// </summary>
        IApplicationBuilder ApplicationBuilder { get; }
        /// <summary>
        /// 服务容器
        /// </summary>
        IServiceProvider ServiceProvider { get; }
    }
}
