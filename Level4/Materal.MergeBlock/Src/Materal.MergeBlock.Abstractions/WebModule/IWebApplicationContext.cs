using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock.Abstractions.WebModule
{
    /// <summary>
    /// 应用程序上下文
    /// </summary>
    public interface IWebApplicationContext : IApplicationContext
    {
        /// <summary>
        /// Web应用程序
        /// </summary>
        WebApplication WebApplication { get; }
    }
}
