﻿using Microsoft.Extensions.Configuration;

namespace Materal.ContextCache.Test
{
    public abstract class BaseTest
    {
        private readonly IServiceCollection _serviceCollection;
        protected IServiceProvider Services;
        protected readonly IConfigurationRoot Configuration;
        protected BaseTest()
        {
            _serviceCollection = new ServiceCollection();
            ConfigurationBuilder configurationBuilder = new();
            AddConfig(configurationBuilder);
            Configuration = configurationBuilder.Build();
            HttpMessageHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (request, ceti, chain, errors) => true
            };
            HttpClient httpClient = new(handler);
            _serviceCollection.TryAddSingleton(httpClient);
            _serviceCollection.AddOptions();
            AddServices(_serviceCollection);
            MateralServices.Services = BuilderServiceProvider(_serviceCollection);
            Services = MateralServices.Services;
        }
        protected virtual IServiceProvider BuilderServiceProvider(IServiceCollection services) => services.BuildServiceProvider();
        public virtual void AddServices(IServiceCollection services)
        {

        }
        public virtual void AddConfig(IConfigurationBuilder builder)
        {
        }
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T? GetServices<T>() => Services.GetService<T>();
        /// <summary>
        /// 获取必须的服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetRequiredService<T>() where T : notnull => Services.GetRequiredService<T>();
    }
}
