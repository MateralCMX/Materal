using Materal.ConfigurationHelper;
using Microsoft.Extensions.Configuration;

namespace ConfigCenter.Client.ConsoleDemo
{
    public class Program
    {
        public static async Task Main()
        {
            IMateralConfigurationBuilder configurationBuilder = new MateralConfigurationBuilder("http://175.27.194.19:8720", "XMJProject", -1)
                .AddDefaultNamespace()
                .AddNamespace("WebAPI")
                .AddNamespace("Demo");
            IConfigurationRoot _configuration = configurationBuilder.Build();
            while (true)
            {
                string? testValue = _configuration.GetValue("MateralLogger:Application");
                Console.WriteLine(testValue);
                await Task.Delay(1000);
            }
        }
    }
}
