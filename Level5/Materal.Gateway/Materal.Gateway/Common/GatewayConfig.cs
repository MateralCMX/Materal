namespace Materal.Gateway.Common
{
    /// <summary>
    /// 网关配置
    /// </summary>
    public static class GatewayConfig
    {
        /// <summary>
        /// 忽略无法找到下游路由错误
        /// </summary>
        public static bool IgnoreUnableToFindDownstreamRouteError { get; set; } = false;
    }
}
