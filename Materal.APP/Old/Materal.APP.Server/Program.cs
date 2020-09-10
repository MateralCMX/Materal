using System.Threading.Tasks;
using Materal.APP.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;

namespace Materal.APP.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder result = Host.CreateDefaultBuilder(args);
            result = result.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
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