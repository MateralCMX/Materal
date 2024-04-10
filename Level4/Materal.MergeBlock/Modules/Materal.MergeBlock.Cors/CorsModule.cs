using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock.Cors
{
    /// <summary>
    /// 跨域模块
    /// </summary>
    public class CorsModule() : MergeBlockWebModule("跨域模块", "CorsModule")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IWebApplicationContext context)
        {
            context.WebApplication.UseCors();
            await base.OnApplicationInitBeforeAsync(context);
        }
    }
}
