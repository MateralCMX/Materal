using Materal.Logger.Extensions;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Materal.Oscillator.Test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            HostApplicationBuilder applicationBuilder = Host.CreateApplicationBuilder(args);
            applicationBuilder.Services.AddMateralLogger(true);
            applicationBuilder.Services.AddOscillator();
            applicationBuilder.Services.AddSingleton<IOscillatorListener, TestOscillatorListener>();
            applicationBuilder.Services.AddHostedService<OscillatorHostedService>();
            IHost host = applicationBuilder.Build();
            await host.Services.UseOscillatorAsync();
            await host.RunAsync();
        }
    }
}
