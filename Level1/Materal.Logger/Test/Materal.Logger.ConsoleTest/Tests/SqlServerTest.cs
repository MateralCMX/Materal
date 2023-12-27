using Materal.Logger.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleTest.Tests
{
    public class SqlServerTest : ITest
    {
        public async Task TestAsync(string[] args)
        {
            HostApplicationBuilder hostApplicationBuilder = Host.CreateApplicationBuilder(args);
            hostApplicationBuilder.AddMateralLogger(config =>
            {
                config.AddSqlServerTarget("SqlServerLog", "Data Source=82.156.11.176;Database=LogTestDB; User ID=sa; Password=gdb@admin678;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;").AddAllTargetsRule(minLevel: LogLevel.Trace);
            });
            IHost host = hostApplicationBuilder.Build();
            host.UseMateralLogger();
            ILogger logger = host.Services.GetRequiredService<ILogger<Program>>();
            SendLoggerManager.Send(logger);
            await host.RunAsync();
        }
    }
}
