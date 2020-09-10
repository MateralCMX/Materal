using System;
using Authority.Common;
using Materal.APP.Core;
using Materal.APP.WebAPICore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authority.Server
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
            Console.Title = $"{AuthorityConfig.ServerInfo.Name} 版本:[{ApplicationConfig.GetProgramVersion()}]";
            string[] inputArgs = HandlerArgs(args);
            await CreateHostBuilder(inputArgs).Build().RunAsync();
        }
        /// <summary>
        /// 处理传入参数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static string[] HandlerArgs(string[] args)
        {
            string[] inputArgs = args;
            string urlsArg = inputArgs.FirstOrDefault(m => m.StartsWith("--urls="));
            if (string.IsNullOrWhiteSpace(urlsArg))
            {
                List<string> temp = inputArgs.ToList();
                temp.Add($"--urls={AuthorityConfig.ServerInfo.Url}");
                inputArgs = temp.ToArray();
                ApplicationConfig.Url = AuthorityConfig.ServerInfo.Url;
            }
            else
            {
                string[] tempArgs = urlsArg.Split('=');
                if (tempArgs.Length == 2)
                {
                    ApplicationConfig.Url = tempArgs[1];
                }
            }
            AuthorityConsoleHelper.WriteLine($"本服务地址:{ApplicationConfig.Url}");
            return inputArgs;
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