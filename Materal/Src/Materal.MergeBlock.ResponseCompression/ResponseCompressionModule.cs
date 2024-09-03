using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock.ResponseCompression
{
    /// <summary>
    /// 响应压缩模块
    /// </summary>
    [DependsOn(typeof(Web.WebModule))]
    public class ResponseCompressionModule() : MergeBlockModule("响应压缩模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void OnConfigureServices(ServiceConfigurationContext context) => context.Services.AddResponseCompression();
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            AdvancedContext advancedContext = context.ServiceProvider.GetRequiredService<AdvancedContext>();
            if (advancedContext.App is not WebApplication webApplication) return;
            webApplication.UseResponseCompression();
        }
    }
}
