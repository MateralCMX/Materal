using Materal.Extensions.DependencyInjection;
using Materal.Utils.Extensions;
using Materal.Utils.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.Test.Base
{
    /// <summary>
    /// 测试基类
    /// </summary>
    public abstract class MateralTestBase
    {
        /// <summary>
        /// 服务容器
        /// </summary>
        protected static IServiceCollection Services => MateralServices.Services;
        /// <summary>
        /// 服务提供者
        /// </summary>
        protected static IServiceProvider ServiceProvider => MateralServices.ServiceProvider;
        /// <summary>
        /// 配置
        /// </summary>
        protected readonly IConfiguration Configuration;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected MateralTestBase()
        {
            MateralServices.Services = new ServiceCollection();
            PageRequestModel.PageStartNumber = 1;
            ConfigurationBuilder configurationBuilder = new();
            AddConfig(configurationBuilder);
            Configuration = configurationBuilder.Build();
            HttpMessageHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (request, ceti, chain, errors) => true
            };
            HttpClient httpClient = new(handler);
            MateralServices.Services.TryAddSingleton(httpClient);
            MateralServices.Services.AddMateralUtils();
            MateralServices.Services.AddOptions();
            AddServices(MateralServices.Services);
            MateralServices.ServiceProvider = BuilderServiceProvider(MateralServices.Services);
        }
        /// <summary>
        /// 构建服务提供者
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected virtual IServiceProvider BuilderServiceProvider(IServiceCollection services) => services.BuildMateralServiceProvider();
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        public virtual void AddServices(IServiceCollection services)
        {

        }
        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="builder"></param>
        public virtual void AddConfig(IConfigurationBuilder builder)
        {
        }
    }
}
