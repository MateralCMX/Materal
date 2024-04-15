using Materal.Logger.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Materal.Logger.ConsoleHostDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            HostApplicationBuilder applicationBuilder = Host.CreateApplicationBuilder(args);
            applicationBuilder.AddMateralLogger();
            //applicationBuilder.Logging.AddMateralLogger();
            //applicationBuilder.Services.AddLogging(builder => builder.AddMateralLogger());
            //applicationBuilder.Services.AddMateralLogger();
            applicationBuilder.Services.AddHostedService<WriteLogService>();
            IHost host = applicationBuilder.Build();
            await host.UseMateralLoggerAsync();
            await host.RunAsync();
        }
    }
}
