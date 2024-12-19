using Materal.Extensions.DependencyInjection;
using Materal.Extensions.DependencyInjection.AspNetCore;
using Materal.Utils.Consul;
using Materal.Utils.Consul.ConfigModels;
using Materal.WebAPITest.Repository;
using Materal.WebAPITest.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.WebAPITest
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddMateralServiceProvider();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMateralConsulUtils();
            builder.Services.TryAddScoped<ITestService, TestServiceImpl>();
            builder.Services.TryAddScoped<ITestRepository, TestRepositoryImpl>();
            WebApplication app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthorization();
            app.MapControllers();
            IConsulService consulService = app.Services.GetRequiredService<IConsulService>();
            await consulService.RegisterConsulAsync(new ConsulConfig
            {
                ConsulUrl = new()
                {
                    Host = "127.0.0.1",
                    Port = 8500
                },
                Enable = true,
                Health = new()
                {
                    Interval = 3,
                    Url = new()
                    {
                        Host = "host.docker.internal",
                        Port = 5000,
                        Path = "/api/Health"
                    }
                },
                ServiceName = "MyServer",
                ServiceUrl = new()
                {
                    Host = "localhost",
                    Port = 5000
                },
                Tags = ["Materal", "Materal.Server"]
            });
            await app.RunAsync();
        }
    }
}