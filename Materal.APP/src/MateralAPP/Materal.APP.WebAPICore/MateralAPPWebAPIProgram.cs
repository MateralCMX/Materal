using Materal.APP.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace Materal.APP.WebAPICore
{
    public static class MateralAPPWebAPIProgram
    {
        public static IHostBuilder CreateHostBuilder<T>(string[] args) where T : MateralAPPWebAPIStartup
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
            string contentRoot = AppDomain.CurrentDomain.BaseDirectory;
            hostBuilder = hostBuilder.UseContentRoot(contentRoot);
            hostBuilder = hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddMateralAPPConfig(hostingContext.HostingEnvironment.EnvironmentName);
            });
            hostBuilder = hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<T>();
            });
            hostBuilder = hostBuilder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                LogLevel minLogLevel = ApplicationConfig.NLogConfig.MinLogLevel;
                logging.SetMinimumLevel(minLogLevel);
            });
            hostBuilder = hostBuilder.UseServiceProviderFactory(new MateralAPPServiceContextProviderFactory());
            hostBuilder = hostBuilder.UseNLog();
            return hostBuilder;
        }
    }
}
