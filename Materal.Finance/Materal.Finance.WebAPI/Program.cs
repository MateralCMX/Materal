using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Materal.Finance.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--ConfigTarget":
                        i++;
                        //AuthorityConfig.ConfigurationBuilder(args[i]);
                        //TaskManagerConfig.ConfigurationBuilder(args[i]);
                        break;
                }
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog();
        }
    }
}
