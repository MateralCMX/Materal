using Materal.MergeBlock.Abstractions.Extensions;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// 异常拦截器模块
    /// </summary>
    public class ExceptionInterceptorModule() : MergeBlockModule("异常拦截模块")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            IMvcBuilder? mvcBuilder = context.Services.GetSingletonInstance<IMvcBuilder>();
            if (mvcBuilder is null) return;
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });
        }
        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context"></param>
        public override void OnPostConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHostedServiceDecorator<ExceptionInterceptorHostedServiceDecorator>();
        }
    }
}
