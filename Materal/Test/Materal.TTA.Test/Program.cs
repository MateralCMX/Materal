using Materal.Extensions.DependencyInjection;
using Materal.TTA.SqliteEFRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Materal.TTA.Test
{
    public class Program
    {
        private const string dbConnectionString = "Data Source=E:\\Project\\古典部\\古典部\\TrainningAssistant\\TrainningAssistant.Main\\TrainningAssistant.Main.WebAPI\\bin\\TrainningAssistantMain.db";
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
