using Materal.DotNetty.Client.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.DotNetty.Client.CoreImpl
{
    public static class MateralDotNettyClientCoreDIExtension
    {
        /// <summary>
        /// 添加服务依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddMateralDotNettyClientCore(this IServiceCollection services)
        {
            services.AddSingleton<IDotNettyClient, DotNettyClientImpl>();
            //services.AddTransient<MateralClientChannelHandler>();
        }
    }
}
