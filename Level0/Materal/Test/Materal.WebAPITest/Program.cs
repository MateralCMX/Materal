using Materal.Extensions.DependencyInjection;
using Materal.Utils;
using Materal.WebAPITest.Services;

namespace Materal.WebAPITest
{
    public class Program
    {
        public static IServiceCollection? Services;
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<ITestService, TestServiceImpl>();
            builder.Services.AddInterceptor<MyInterceptorAttribute>();
            Services = builder.Services;
            builder.Host.UseServiceProviderFactory(new MateralServiceProviderFactory());// π”√AOP
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
    public class MyInterceptorAttribute : InterceptorAttribute
    {
        public override void Befor(InterceptorContext context)
        {
            ConsoleQueue.WriteLine("Befor", ConsoleColor.DarkGreen);
        }
        public override void After(InterceptorContext context)
        {
            ConsoleQueue.WriteLine("After", ConsoleColor.DarkGreen);
        }
    }
}