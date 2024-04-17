using Materal.Extensions.DependencyInjection;
using Materal.Logger.ConsoleLogger;
using Materal.Logger.Extensions;
using Materal.TTA.Common;
using Materal.TTA.Demo.Tests;
using Materal.Utils.Extensions;
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
                .AddAllTargetsRule(minLevel: LogLevel.Trace);
            });
            serviceCollection.AddTransient<ITTADemoTest, Test00>();
            serviceCollection.AddTransient<ITTADemoTest, Test01>();
            serviceCollection.AddTransient<ITTADemoTest, Test02>();
            //serviceCollection.AddMySqlEFTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await MySqlEFHelper.MigrateAsync(serviceProvider);

            serviceCollection.AddSqliteEFTTA();
            static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqliteEFHelper.MigrateAsync(serviceProvider);

            //serviceCollection.AddSqlServerEFTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqlServerEFHelper.MigrateAsync(serviceProvider);

            IServiceProvider services = serviceCollection.BuildMateralServiceProvider();
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