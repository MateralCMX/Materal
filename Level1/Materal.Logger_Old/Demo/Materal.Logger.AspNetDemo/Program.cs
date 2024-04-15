using Materal.Logger.Extensions;

namespace Materal.Logger.AspNetDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);
            applicationBuilder.AddMateralLogger();
            //applicationBuilder.Logging.AddMateralLogger();
            //applicationBuilder.Services.AddMateralLogger(bulider => bulider.AddMateralLogger());
            //applicationBuilder.Services.AddMateralLogger();
            applicationBuilder.Services.AddControllers();
            applicationBuilder.Services.AddEndpointsApiExplorer();
            applicationBuilder.Services.AddSwaggerGen();
            WebApplication app = applicationBuilder.Build();
            await app.UseMateralLoggerAsync();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthorization();
            app.MapControllers();
            await app.RunAsync();
        }
    }
}
