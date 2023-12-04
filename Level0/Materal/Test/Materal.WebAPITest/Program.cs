using Materal.Extensions.DependencyInjection;
using Materal.Utils;
using Materal.Utils.Consul;
using Materal.Utils.Consul.ConfigModels;
using Materal.Utils.Http;
using Materal.WebAPITest.Services;

namespace Materal.WebAPITest
{
    public class Program
    {
        private static IServiceProvider? _serviceProvider;
        public static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddConsulUtils();
            builder.Services.AddScoped<ITestService, TestServiceImpl>();
            builder.Services.AddInterceptor<MyInterceptorAttribute>();
            builder.Host.UseServiceProviderFactory(new MateralServiceProviderFactory());// π”√AOP
            WebApplication app = builder.Build();
            _serviceProvider = app.Services;
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            IConsulService consulService = app.Services.GetRequiredService<IConsulService>();
            ConsulConfigModel consulConfigModel = new()
            {
                ConsulUrl = new()
                {
                    Host = "127.0.0.1",
                    Port = 8500,
                    IsSSL = false
                },
                HealthConfig = new()
                {
                    HealthInterval = 10,
                    HealthUrl = new()
                    {
                        Host = "host.docker.internal",
                        Port = 5000,
                        IsSSL = false,
                        Path = "api/Health"
                    }
                },
                ServiceName = "MateralWebAPITest",
                Tags = ["Materal", "Materal.Utils"],
                Enable = true,
                ServiceUrl = new()
                {
                    Host = "localhost",
                    Port = 5000,
                    IsSSL = false
                }
            };
            await consulService.RegisterConsulAsync(consulConfigModel);
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            if (_serviceProvider is null) return;
            IConsulService consulService = _serviceProvider.GetRequiredService<IConsulService>();
            Task task = consulService.UnregisterConsulAsync();
            task.Wait();
        }
    }
}