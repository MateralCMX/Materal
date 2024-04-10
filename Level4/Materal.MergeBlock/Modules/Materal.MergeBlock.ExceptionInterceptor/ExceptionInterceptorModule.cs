using Materal.MergeBlock.Abstractions.WebModule;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// 异常拦截器模块
    /// </summary>
    public class ExceptionInterceptorModule() : MergeBlockWebModule("异常拦截模块", "ExceptionInterceptor")
    {
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
    }
}
