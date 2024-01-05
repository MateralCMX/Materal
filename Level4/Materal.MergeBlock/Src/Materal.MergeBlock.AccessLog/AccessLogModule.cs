using Materal.MergeBlock.AccessLog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// 访问日志模块
    /// </summary>
    public class AccessLogModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.TryAddSingleton<AccessLogMiddleware>();
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            context.ApplicationBuilder.UseMiddleware<AccessLogMiddleware>();
            return base.OnApplicationInitBeforeAsync(context);
        }
    }
}
