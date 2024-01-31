using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// Web配置服务上下文
    /// </summary>
    public class WebApplicationContext(IServiceProvider serviceProvider, WebApplication webApplication) : ApplicationContext(serviceProvider), IWebApplicationContext
    {
        /// <summary>
        /// Web应用程序
        /// </summary>
        public WebApplication WebApplication { get; } = webApplication;
    }
}
