using Materal.Abstractions;
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
        private readonly IServiceCollection _serviceCollection;
        /// <summary>
        /// 服务
        /// </summary>
        protected IServiceProvider ServiceProvider;
        /// <summary>
        /// 配置
        /// </summary>
        protected readonly IConfigurationRoot Configuration;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected MateralTestBase()
        {
            _serviceCollection = new ServiceCollection();
            PageRequestModel.PageStartNumber = 1;
            ConfigurationBuilder configurationBuilder = new();
            AddConfig(configurationBuilder);
            Configuration = configurationBuilder.Build();
            HttpMessageHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (request, ceti, chain, errors) => true
            };
            HttpClient httpClient = new(handler);
            _serviceCollection.TryAddSingleton(httpClient);
            _serviceCollection.AddMateralUtils();
            _serviceCollection.AddOptions();
            AddServices(_serviceCollection);
            MateralServices.Services = BuilderServiceProvider(_serviceCollection);
            ServiceProvider = MateralServices.Services;
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
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T? GetServices<T>() => ServiceProvider.GetService<T>();
        /// <summary>
        /// 获取必须的服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetRequiredService<T>() where T : notnull => ServiceProvider.GetRequiredService<T>();
    }
}
