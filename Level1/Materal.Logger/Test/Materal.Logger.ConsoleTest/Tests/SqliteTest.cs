using Materal.Logger.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleTest.Tests
{
    public class SqliteTest : ITest
    {
        public async Task TestAsync(string[] args)
        {
            HostApplicationBuilder hostApplicationBuilder = Host.CreateApplicationBuilder(args);
            hostApplicationBuilder.AddMateralLogger(config =>
            {
                config.AddSqliteTargetFromPath("SqliteLog", "${RootPath}\\Logs\\MateralLogger.db").AddAllTargetsRule(minLevel: LogLevel.Trace);
            });
            IHost host = hostApplicationBuilder.Build();
            host.UseMateralLogger();
            ILogger logger = host.Services.GetRequiredService<ILogger<Program>>();
            SendLoggerManager.Send(logger);
            await host.RunAsync();
        }
    }
}
