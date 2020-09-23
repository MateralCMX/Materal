using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deploy.Common;
using Materal.APP.Core;
using Materal.APP.WebAPICore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Deploy.Server
{
    /// <summary>
    /// ������
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            Console.Title = $"{DeployConfig.ServerInfo.Name} �汾:[{ApplicationConfig.GetProgramVersion()}]";
            string[] inputArgs = HandlerArgs(args);
            await CreateHostBuilder(inputArgs).Build().RunAsync();
        }
        /// <summary>
        /// ���������
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
                temp.Add($"--urls={DeployConfig.ServerInfo.Url}");
                inputArgs = temp.ToArray();
                ApplicationConfig.Url = DeployConfig.ServerInfo.Url;
            }
            else
            {
                string[] tempArgs = urlsArg.Split('=');
                if (tempArgs.Length == 2)
                {
                    ApplicationConfig.Url = tempArgs[1];
                }
            }
            DeployConsoleHelper.WriteLine($"�������ַ:{ApplicationConfig.Url}");
            return inputArgs;
        }
        /// <summary>
        /// ��������
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