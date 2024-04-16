using Materal.Logger.ConsoleHostDemo.Services;
using Materal.Logger.ConsoleLogger;
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
            applicationBuilder.AddMateralLogger(options =>
            {
                options.AddConsoleTarget("ConsoleLog");
                options.AddAllTargetsRule();
            }, true);
            applicationBuilder.Services.AddHostedService<HelloWorldService>();
            IHost host = applicationBuilder.Build();
            await host.RunAsync();
        }
    }
}
