using Materal.Gateway.OcelotConsulExtension;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;

namespace Materal.Gateway
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加Materal网关
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IOcelotBuilder AddMateralGateway(this IServiceCollection services)
        {
            IOcelotBuilder result = services.AddOcelot();
            result.AddGatewayConsul();
            return result;
        }
    }
}
