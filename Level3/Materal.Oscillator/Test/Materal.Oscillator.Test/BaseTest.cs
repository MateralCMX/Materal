using Materal.Abstractions;
using Materal.Oscillator.SqliteEFRepository;
using Materal.Oscillator.SqlServerEFRepository;
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

            IMigrateHelper<OscillatorSqliteDBContext> migrateHelper = GetServiceTest<IMigrateHelper<OscillatorSqliteDBContext>>();
            //IMigrateHelper<OscillatorSqlServerDBContext> migrateHelper = GetServiceTest<IMigrateHelper<OscillatorSqlServerDBContext>>();

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

            //SqlServerConfigModel dbConfig = new()
            //{
            //    Address = "82.156.11.176",
            //    Port = "1433",
            //    Name = "OscillatorTestDB",
            //    UserID = "sa",
            //    Password = "gdb@admin678",
            //    TrustServerCertificate = true
            //};
            //services.AddOscillatorSqlServerRepository(dbConfig);
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