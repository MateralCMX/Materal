using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
#if DEBUG
#else
using System.IO;
#endif
namespace Materal.Gateway
{
    /// <summary>
    /// ����
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static void Main(string[] args)
        {
            ApplicationData.Init(args);
            IHost _host = CreateHostBuilder(args).Build();
            _host.Run();
        }
        /// <summary>
        /// ����HostBuilder
        /// </summary>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder = hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = null;
                });
                webBuilder.ConfigureServices(services =>
                {
                    ApplicationData.Services = services;
                });
                webBuilder.UseStartup<Startup>();
            });
            hostBuilder = hostBuilder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            });
            string contentRoot = AppDomain.CurrentDomain.BaseDirectory;
            hostBuilder = hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(contentRoot)
                    .AddJsonFile("ocelot.json");
            });
            hostBuilder = hostBuilder.UseServiceProviderFactory(new ServiceContextProviderFactory());
#if DEBUG
#else
            if (Directory.Exists(contentRoot))
            {
                hostBuilder.UseContentRoot(contentRoot);
            }
#endif
            return hostBuilder;
        }
    }
}
