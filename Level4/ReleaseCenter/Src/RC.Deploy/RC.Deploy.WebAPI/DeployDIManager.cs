using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace RC.Deploy.WebAPI
{
    public partial class DeployDIManager
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerGenConfig"></param>
        /// <returns></returns>
        public override IServiceCollection AddRCDeployService(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)
        {
            swaggerGenConfig = config => config.AddSignalRSwaggerGen(option =>
            {
                option.ScanAssembly(Assembly.Load("RC.Deploy.Hubs"));
            });
            base.AddRCDeployService(services, swaggerGenConfig);
            return services;
        }
    }
}
