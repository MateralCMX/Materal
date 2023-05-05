using Materal.Abstractions;
using Materal.Oscillator.SqliteRepository;
using Materal.TTA.Common.Model;
using Materal.TTA.EFRepository;
using Materal.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.Test
{
    public abstract class BaseTest
    {
        protected readonly IServiceProvider Services;
        protected BaseTest()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            MateralConfig.PageStartNumber = 1;
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            AddConfig(configurationBuilder);
            serviceCollection.AddMateralUtils();
            serviceCollection.AddOscillator();
            AddSqliteRepository(serviceCollection);
            AddServices(serviceCollection);
            MateralServices.Services = serviceCollection.BuildServiceProvider();
            Services = MateralServices.Services;
            IMigrateHelper<OscillatorDBContext> migrateHelper = GetServiceTest<IMigrateHelper<OscillatorDBContext>>();
            migrateHelper.Migrate();
        }
        /// <summary>
        /// 添加Sqlite仓储
        /// </summary>
        /// <param name="services"></param>
        protected virtual void AddSqliteRepository(IServiceCollection services)
        {
            SqliteConfigModel dbConfig = new()
            {
                Source = "Oscillator.db"
            };
            services.AddOscillatorSqliteRepository(dbConfig);
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual T GetServiceTest<T>()
        {
            T? service = Services.GetService<T>();
            if (service == null) Assert.Fail($"获取{typeof(T)}失败");
            return service;
        }
        public virtual void AddServices(IServiceCollection services)
        {

        }
        public virtual void AddConfig(IConfigurationBuilder builder)
        {
        }
    }
}