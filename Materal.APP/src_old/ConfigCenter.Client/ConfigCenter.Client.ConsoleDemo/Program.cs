using Materal.ConfigurationHelper;
using Microsoft.Extensions.Configuration;

namespace ConfigCenter.Client.ConsoleDemo
{
    public class Program
    {
        public static async Task Main()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddDefaultNameSpace("http://175.27.194.19:8720", "XMJProject", 60)
                .AddNameSpace("WebAPI")
                .AddNameSpace("Demo");
            IConfiguration _configuration = configurationBuilder.Build();
            while (true)
            {
                string? testValue = _configuration.GetValue("AppName");
                Console.WriteLine(testValue);
                await Task.Delay(1000);
            }
        }
    }
}
