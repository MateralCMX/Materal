using ConfigCenter.Environment.Common;
using Materal.APP.Core;
using Materal.APP.WebAPICore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.Server
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            Console.Title = $"{ConfigCenterEnvironmentConfig.ServerInfo.Name} 版本:[{ApplicationConfig.GetProgramVersion()}]";
            string[] inputArgs = MateralAPPHelper.HandlerArgs(args, ConfigCenterEnvironmentConfig.ServerInfo);
            ConfigCenterEnvironmentConsoleHelper.WriteLine($"本服务地址:{ApplicationConfig.Url}");
            ConfigCenterEnvironmentConsoleHelper.WriteLine($"本服务公开地址:{ApplicationConfig.PublicUrl}");
            await CreateHostBuilder(inputArgs).Build().RunAsync();
        }
        /// <summary>
        /// 创建主机
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder result = Host.CreateDefaultBuilder(args);
            result = result.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
            result = result.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(LogLevel.Trace);
            });
            result = result.UseNLog();
            result = result.UseServiceProviderFactory(new MateralAPPServiceContextProviderFactory());
            return result;
        }
    }
}