using Materal.MergeBlock.Abstractions.WebModule;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// Web配置服务上下文
    /// </summary>
    /// <param name="serviceProvider"></param>
    public class WebApplicationContext(IServiceProvider serviceProvider) : ApplicationContext(serviceProvider), IWebApplicationContext
    {
    }
}
