using Ocelot.Configuration;

namespace Materal.Gateway.OcelotExtension.Middleware
{
    /// <summary>
    /// 下游路由扩展
    /// </summary>
    public static class DownstreamRouteExtension
    {
        /// <summary>
        /// 获取下游路由地址
        /// </summary>
        /// <param name="downstreamRoute"></param>
        /// <returns></returns>
        public static string GetDownstreamScheme(this DownstreamRoute downstreamRoute) => downstreamRoute.DownstreamScheme switch
        {
            "grpc" => "http",
            "grpcs" => "https",
            _ => downstreamRoute.DownstreamScheme,
        };
    }
}
