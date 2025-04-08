using Materal.Extensions.DependencyInjection;
using Materal.TTA.SqliteEFRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Materal.TTA.Test
{
    public class Program
    {
        //private const string dbConnectionString = "Data Source=E:\\Project\\Materal\\DataBase\\Test.db";
        private const string dbConnectionString = "Data Source=/usr/project/databaseTest.db";
        public static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.AddMateralServiceProvider();
            builder.Services.AddTTASqliteEFRepository<TestDBContext>(dbConnectionString);
            builder.Services.AddHostedService<TestService>();
            IHost host = builder.Build();
            await host.RunAsync();
        }
    }
}
