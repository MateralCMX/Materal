using Materal.Logger;
using Materal.Logger.ConfigModels;
using Materal.TTA.Common;
using Materal.TTA.Demo.Tests;
using Materal.Utils;
using Materal.Utils.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.TTA.Demo
{
    public class Program
    {
        public static async Task Main()
        {
            PageRequestModel.PageStartNumber = 1;
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralUtils();
            serviceCollection.AddMateralLogger(config =>
            {
                config.AddConsoleTarget("LifeConsole")
                .AddAllTargetsRule(minLevel:LogLevel.Trace);
            });
            serviceCollection.AddTransient<ITTADemoTest, Test00>();
            serviceCollection.AddTransient<ITTADemoTest, Test01>();
            serviceCollection.AddTransient<ITTADemoTest, Test02>();
#if NET6_0
            //serviceCollection.AddMySqlEFTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await MySqlEFHelper.MigrateAsync(serviceProvider);

            serviceCollection.AddSqliteEFTTA();
            static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqliteEFHelper.MigrateAsync(serviceProvider);

            //serviceCollection.AddSqlServerEFTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqlServerEFHelper.MigrateAsync(serviceProvider);

            //serviceCollection.AddSqliteADONETTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqliteADONETHelper.MigrateAsync(serviceProvider);

            //serviceCollection.AddSqlServerADONETTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqlServerADONETHelper.MigrateAsync(serviceProvider);
#else
            serviceCollection.AddSqliteEFTTA();
            static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqliteEFHelper.MigrateAsync(serviceProvider);

            //serviceCollection.AddSqlServerEFTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqlServerEFHelper.MigrateAsync(serviceProvider);

            //serviceCollection.AddSqliteADONETTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqliteADONETHelper.MigrateAsync(serviceProvider);

            //serviceCollection.AddSqlServerADONETTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqlServerADONETHelper.MigrateAsync(serviceProvider);
#endif

            IServiceProvider services = serviceCollection.BuildServiceProvider();
            await services.UseMateralLoggerAsync();
            ILoggerExtension.TSQLLogLevel = LogLevel.Trace;

            using (IServiceScope scope = services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                await MigrateAsync(serviceProvider);
                IEnumerable<ITTADemoTest> ttaDemoTests = serviceProvider.GetServices<ITTADemoTest>();
                foreach (ITTADemoTest ttaDemoTest in ttaDemoTests)
                {
                    await ttaDemoTest.RunTestAsync();
                }
            }
        }
    }
}