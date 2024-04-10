using MMLib.SwaggerForOcelot.Configuration;

namespace Materal.Gateway.Application.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerEndPointOptionsExtensions
    {
        private const string AggregatesKey = "aggregates";
        private const string GatewayKey = "gateway";
        /// <summary>
        /// 是否是网关本身
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static bool IsGatewayItSelf(this SwaggerEndPointConfig config) 
            => config.Version == AggregatesKey || config.Version == GatewayKey;
    }
}
