
namespace Materal.Logger.AspNetDemo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder applicationBuilder = WebApplication.CreateBuilder(args);
            applicationBuilder.AddMateralLogger();
            //applicationBuilder.Logging.AddDyLogger();
            //applicationBuilder.Services.AddLogging(bulider => bulider.AddDyLogger());
            //applicationBuilder.Services.AddDyLogger();
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
