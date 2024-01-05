using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// 异常拦截器模块
    /// </summary>
    public class ExceptionInterceptorModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnConfigServiceBeforeAsync(IConfigServiceContext context)
        {
            context.Services.AddExceptionHandler<MergeBlockExceptionHandler>();
            return base.OnConfigServiceBeforeAsync(context);
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ExceptionConfig>(context.Configuration.GetSection(ExceptionConfig.ConfigKey));
            context.MvcBuilder?.AddMvcOptions(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnApplicationInitBeforeAsync(IApplicationContext context)
        {
            context.ApplicationBuilder.UseExceptionHandler("/Error");
            return base.OnApplicationInitBeforeAsync(context);
        }
    }
}
