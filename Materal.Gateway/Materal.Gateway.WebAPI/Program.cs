using Materal.Gateway.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Threading.Tasks;

namespace Materal.Gateway.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ApplicationConfig.Init(args);
            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
            string contentRoot = AppDomain.CurrentDomain.BaseDirectory;
            hostBuilder = hostBuilder.UseContentRoot(contentRoot);
            hostBuilder = hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("Ocelot.json");
            });
            hostBuilder = hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
            hostBuilder = hostBuilder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(ApplicationConfig.NLogConfig.MinLogLevel);
            });
            hostBuilder = hostBuilder.UseNLog();
            return hostBuilder;
        }

    }
}