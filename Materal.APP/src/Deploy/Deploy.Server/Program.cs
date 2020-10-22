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
            string[] inputArgs = MateralAPPHelper.HandlerArgs(args, DeployConfig.ServerInfo);
            DeployConsoleHelper.WriteLine($"�������ַ:{ApplicationConfig.Url}");
            DeployConsoleHelper.WriteLine($"�����񹫿���ַ:{ApplicationConfig.PublicUrl}");
            await CreateHostBuilder(inputArgs).Build().RunAsync();
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