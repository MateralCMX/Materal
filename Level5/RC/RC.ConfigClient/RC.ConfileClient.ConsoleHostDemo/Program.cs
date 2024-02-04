using Materal.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RC.ConfigClient.Extensions;

namespace RC.ConfileClient.ConsoleHostDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            foreach (string arg in args)
            {
                ConsoleQueue.WriteLine(arg);
            }
            HostApplicationBuilder applicationBuilder = Host.CreateApplicationBuilder(args);
            applicationBuilder.Configuration.AddDefaultNameSpace("http://127.0.0.1:8700/RCESDEVAPI", "TestProject", 10);
            applicationBuilder.Services.AddOptions();
            applicationBuilder.Services.Configure<DemoConfig>(applicationBuilder.Configuration);
            applicationBuilder.Services.AddHostedService<PrintConfigService>();
            IHost host = applicationBuilder.Build();
            await host.RunAsync();
        }
    }
}
