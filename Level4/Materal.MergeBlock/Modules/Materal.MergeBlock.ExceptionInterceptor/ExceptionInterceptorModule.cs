using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// 异常拦截器模块
    /// </summary>
    public class ExceptionInterceptorModule : MergeBlockWebModule, IMergeBlockWebModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ExceptionInterceptorModule() : base("异常拦截模块", "ExceptionInterceptor")
        {

        }
#if NET8_0
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
#endif
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IWebConfigServiceContext context)
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
        public override Task OnApplicationInitBeforeAsync(IWebApplicationContext context)
        {
            context.WebApplication.UseExceptionHandler("/Error");
            return base.OnApplicationInitBeforeAsync(context);
        }
    }
}
