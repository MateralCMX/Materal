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
                config
                .AddSqliteTargetFromPath("SqliteLog", "${RootPath}\\Logs\\MateralLogger.db", "MateralLogger",
                    [
                        new() { Name = "ID", Type = "TEXT", Value = "${LogID}", PK = true },
                        new() { Name = "CreateTime", Type = "DATE", Value = "${DateTime}", Index = true, IsNull = false },
                        new() { Name = "Level", Type = "TEXT", Value = "${Level}" },
                        new() { Name = "ProgressID", Type = "TEXT", Value = "${ProgressID}" },
                        new() { Name = "ThreadID", Type = "TEXT", Value = "${ThreadID}" },
                        new() { Name = "Scope", Type = "TEXT", Value = "${Scope}" },
                        new() { Name = "MachineName", Type = "TEXT", Value = "${MachineName}" },
                        new() { Name = "CategoryName", Type = "TEXT", Value = "${CategoryName}" },
                        new() { Name = "Application", Type = "TEXT", Value = "${Application}" },
                        new() { Name = "UserID", Type = "TEXT", Value = "${UserID}" },
                        new() { Name = "Message", Type = "TEXT", Value = "${Message}" },
                        new() { Name = "Exception", Type = "TEXT", Value = "${Exception}" }
                    ])
                //.AddSqliteTargetFromPath("SqliteLog", "${RootPath}\\Logs\\MateralLogger.db", "${Level}Log")
                .AddAllTargetsRule(minLevel: LogLevel.Trace);
            });
            IHost host = hostApplicationBuilder.Build();
            host.UseMateralLogger();
            ILogger logger = host.Services.GetRequiredService<ILogger<Program>>();
            SendLoggerManager.Send(logger);
            await host.RunAsync();
        }
    }
}
