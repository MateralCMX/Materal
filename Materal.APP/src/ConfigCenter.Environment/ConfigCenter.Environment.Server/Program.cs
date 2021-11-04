using ConfigCenter.Environment.Common;
using Materal.APP.WebAPICore;
using Microsoft.Extensions.Hosting;
using NLog;
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
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            IHostBuilder hostBuilder = MateralAPPWebAPIProgram.CreateHostBuilder<Startup>(args);
            IHost host = hostBuilder.Build();
            Console.Title = ConfigCenterEnvironmentConfig.EnvironmentConfig.Name;
            await host.RunAsync();
        }
        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                ConsulManage.UnregisterConsul();
            }
            catch (ConfigCenterEnvironmentException exception)
            {
                logger.Info(exception.Message);
            }
            catch (Exception exception)
            {
                logger.Error(exception);
            }
            logger.Info("正在关闭Environment服务");
        }
    }
}