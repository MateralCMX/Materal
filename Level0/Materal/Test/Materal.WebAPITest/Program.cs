using Materal.Extensions.DependencyInjection;
using Materal.WebAPITest.Repository;
using Materal.WebAPITest.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Materal.WebAPITest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.Host.UseMateralServiceProvider();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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
            app.Run();
        }
    }
}