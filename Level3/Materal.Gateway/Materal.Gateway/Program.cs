using Materal.ConvertHelper;
using Materal.Gateway.OcelotExtension.ConfigModel;

namespace Materal.Gateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string openFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GatewayConfig.json");
            OcelotConfigModel ocelotConfig = await OcelotConfigModel.CreateByFileAsync(openFilePath);
            string saveFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NewGatewayConfig.json");
            await ocelotConfig.SaveAsAsync(saveFilePath);
        //    var builder = WebApplication.CreateBuilder(args);

            //    // Add services to the container.
            //    builder.Services.AddAuthorization();


            //    var app = builder.Build();

            //    // Configure the HTTP request pipeline.

            //    app.UseAuthorization();

            //    var summaries = new[]
            //    {
            //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            //};

            //    app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            //    {
            //        var forecast = Enumerable.Range(1, 5).Select(index =>
            //            new WeatherForecast
            //            {
            //                Date = DateTime.Now.AddDays(index),
            //                TemperatureC = Random.Shared.Next(-20, 55),
            //                Summary = summaries[Random.Shared.Next(summaries.Length)]
            //            })
            //            .ToArray();
            //        return forecast;
            //    });

            //    app.Run();
        }
    }
}