using Materal.APP.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace Authority.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder result = Host.CreateDefaultBuilder(args);
            result = result.ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
            //result = result.ConfigureLogging(logging =>
            //{
            //    logging.ClearProviders();
            //    logging.SetMinimumLevel(LogLevel.Trace);
            //});
            result = result.UseNLog();
            result = result.UseServiceProviderFactory(new AppServiceContextProviderFactory());
            return result;
        }
    }
}
