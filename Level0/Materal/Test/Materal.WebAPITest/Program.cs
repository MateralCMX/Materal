using Materal.Utils.Consul;
using Materal.Utils.Consul.ConfigModels;
using Materal.WebAPITest.Services;

namespace Materal.WebAPITest
{
    public class Program
    {
        private static IServiceProvider? _serviceProvider;
        public static Guid ConsulConfigID1;
        public static Guid ConsulConfigID2;
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMateralConsulUtils();
            builder.Services.AddScoped<ITestService, TestServiceImpl>();
            builder.Services.AddScoped<ITestRepository, TestRepositoryImpl>();
            //builder.Services.AddInterceptor<MyInterceptorAttribute>();
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
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                ConsulConfig consulConfigModel1 = new()
                {
                    Enable = true,
                    ServiceName = "MateralWebAPITest",
                    Tags = ["Materal", "Materal.Utils"],
                    ConsulUrl = new()
                    {
                        Host = "127.0.0.1",
                        Port = 8500,
                        IsSSL = false
                    },
                    ServiceUrl = new()
                    {
                        Host = "localhost",
                        Port = 5000,
                        IsSSL = false
                    },
                    Health = new()
                    {
                        Interval = 10,
                        Url = new()
                        {
                            Host = "host.docker.internal",
                            //Host = "127.0.0.1",
                            Port = 5000,
                            IsSSL = false,
                            Path = "api/Health"
                        }
                    }
                };
                ConsulConfig consulConfigModel2 = new()
                {
                    Enable = true,
                    ServiceName = "MateralWebAPITest2",
                    Tags = ["Materal", "Materal.Utils"],
                    ConsulUrl = new()
                    {
                        Host = "127.0.0.1",
                        Port = 8500,
                        IsSSL = false
                    },
                    ServiceUrl = new()
                    {
                        Host = "localhost",
                        Port = 5000,
                        IsSSL = false
                    },
                    Health = new()
                    {
                        Interval = 10,
                        Url = new()
                        {
                            Host = "host.docker.internal",
                            //Host = "127.0.0.1",
                            Port = 5000,
                            IsSSL = false,
                            Path = "api/Health"
                        }
                    }
                };
                ConsulConfigID1 = await consulService.RegisterConsulConfigAsync(consulConfigModel1);
                ConsulConfigID2 = await consulService.RegisterConsulConfigAsync(consulConfigModel2);
                await consulService.RegisterAllConsulAsync();
            });
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
            if (_serviceProvider is null) return;
            IConsulService consulService = _serviceProvider.GetRequiredService<IConsulService>();
            Task task = consulService.UnregisterAllConsulAsync();
            task.Wait();
        }
    }
}