using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Web;


namespace Materal.ApplicationUpdate.WebAPI
{
    /// <summary>
    /// 应用程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 应用程序入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            LogManager.Configuration.Install(new InstallationContext());
            CreateWebHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// 创建WebHost
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();
    }
}
