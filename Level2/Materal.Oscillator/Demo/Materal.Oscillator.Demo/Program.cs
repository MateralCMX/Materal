using Materal.Logger.Abstractions.Extensions;
using Materal.Logger.ConsoleLogger;
using Materal.Logger.Extensions;
using Materal.Oscillator.Demo.Services;
using Materal.Oscillator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Materal.Oscillator.Demo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            HostApplicationBuilder applicationBuilder = Host.CreateApplicationBuilder(args);
            applicationBuilder.AddMateralLogger(optios =>
            {
                optios.AddConsoleTarget("ConsoleLog")
                .AddRule($"OscillatorRule", ["ConsoleLog"], LogLevel.Trace, LogLevel.Critical, null, new()
                {
                    ["Default"] = LogLevel.None,
                    ["Oscillator"] = LogLevel.Trace
                })
                .AddScope("Oscillator", LogLevel.None)
                .AddRule($"DefaultRule", ["ConsoleLog"]);
            }, true);
            applicationBuilder.Services.AddOscillator();
            applicationBuilder.Services.AddHostedService<TestService>();
            IHost host = applicationBuilder.Build();
            await host.Services.UseOscillatorAsync();
            await host.RunAsync();
        }
    }
}
