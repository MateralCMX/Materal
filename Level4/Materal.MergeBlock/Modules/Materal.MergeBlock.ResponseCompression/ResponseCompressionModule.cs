using Materal.MergeBlock.Abstractions.WebModule;
using Microsoft.AspNetCore.Builder;

namespace Materal.MergeBlock.ResponseCompression
{
    /// <summary>
    /// 响应压缩模块
    /// </summary>
    public class ResponseCompressionModule() : MergeBlockWebModule("响应压缩模块", "ResponseCompression")
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.AddResponseCompression();
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitBeforeAsync(IWebApplicationContext context)
        {
            context.WebApplication.UseResponseCompression();
            await base.OnApplicationInitBeforeAsync(context);
        }
    }
}
