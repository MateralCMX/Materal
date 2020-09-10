using Materal.APP.Server.Services;
using Materal.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.APP.Server
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class ServerDIExtension
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddServerServices(this IServiceCollection services)
        {
            MateralConfig.PageStartNumber = 1;
        }

        public static void AddGrpcRoute(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGrpcService<StringHelperService>();
        }
    }
}
