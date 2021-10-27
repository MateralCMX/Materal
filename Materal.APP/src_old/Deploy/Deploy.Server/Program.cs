using Deploy.Common;
using Materal.APP.Core;
using Materal.APP.WebAPICore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Threading.Tasks;

namespace Deploy.Server
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
            Console.Title = $"{DeployConfig.ServerInfo.Name} 版本:[{ApplicationConfig.GetProgramVersion()}]";
            string[] inputArgs = MateralAPPHelper.HandlerArgs(args, DeployConfig.ServerInfo);
            DeployConsoleHelper.WriteLine($"本服务地址:{ApplicationConfig.Url}");
            DeployConsoleHelper.WriteLine($"本服务公开地址:{ApplicationConfig.PublicUrl}");
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
                webBuilder.ConfigureKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = null;
                });
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