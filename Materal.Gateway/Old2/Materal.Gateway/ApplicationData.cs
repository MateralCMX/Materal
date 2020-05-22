using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Materal.Gateway
{
    public static class ApplicationData
    {
        /// <summary>
        /// 关闭标识
        /// </summary>
        public static bool IsShutDown { get; private set; }
        private static IServiceCollection Services;
        /// <summary>
        /// 依赖注入服务
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }
        private static string[] _appArgs;
        private static IHost _host;
        public static void Init(string[] appArgs)
        {
            _appArgs = appArgs;
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public static async Task StartAsync()
        {
            _host = CreateHostBuilder().Build();
            await _host.RunAsync();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public static async Task RestartAsync()
        {
            IsShutDown = true;
            await StopAsync();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public static async Task StopAsync()
        {
            if (_host != null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }
        }
        /// <summary>
        /// 构建服务
        /// </summary>
        public static void BuildServices()
        {
            ServiceProvider = Services.BuildServiceProvider();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
        /// <summary>
        /// 创建HostBuilder
        /// </summary>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder()
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(_appArgs);
            hostBuilder = hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    Services = services;
                });
                webBuilder.UseStartup<Startup>();
            });
            hostBuilder = hostBuilder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            });
            hostBuilder = hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("ocelot.json");
                });
            //hostBuilder = hostBuilder.UseNLog();
            hostBuilder = hostBuilder.UseServiceProviderFactory(new ServiceContextProviderFactory());
            return hostBuilder;
        }
    }
}
