using Materal.MergeBlock.Logger;
using Materal.MergeBlock.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.MergeBlock.AccessLog
{
    /// <summary>
    /// 访问日志模块
    /// </summary>
    [DependsOn(typeof(WebModule), typeof(LoggerModule))]
    public class AccessLogModule() : MergeBlockModule("访问日志模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddSingleton<IAccessLogService, AccessLogServiceImpl>();
            context.Services.TryAddSingleton<AccessLogMiddleware>();
        }
        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="context"></param>
        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            AdvancedContext advancedContext = context.ServiceProvider.GetRequiredService<AdvancedContext>();
            if (advancedContext.App is IApplicationBuilder app)
            {
                app.UseMiddleware<AccessLogMiddleware>();
            }
        }
    }
}
