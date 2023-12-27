using Materal.Logger.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.ConsoleTest.Tests
{
    public class ConfigFileTest : ITest
    {
        public async Task TestAsync(string[] args)
        {
            HostApplicationBuilder hostApplicationBuilder = Host.CreateApplicationBuilder(args);
            hostApplicationBuilder.Configuration.AddJsonFile("MateralLogger.json", true, true);
            hostApplicationBuilder.AddMateralLogger();
            IHost host = hostApplicationBuilder.Build();
            host.UseMateralLogger();
            ILogger logger = host.Services.GetRequiredService<ILogger<Program>>();
            SendLoggerManager.Send(logger);
            await host.RunAsync();
        }
    }
}
