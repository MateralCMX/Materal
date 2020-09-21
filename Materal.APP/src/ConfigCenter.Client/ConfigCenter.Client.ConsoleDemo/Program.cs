using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ConfigCenter.Client.ConsoleDemo
{
    public class Program
    {
        static async Task Main()
        {
            IMateralConfigurationBuilder configurationBuilder = new MateralConfigurationBuilder("http://192.168.2.101:8720", "TestProject")
                .AddDefaultNamespace();
            IConfiguration _configuration = await configurationBuilder.BuildMateralConfigAsync();
            string testValue = _configuration["Test"];
            Console.WriteLine(testValue);
        }
    }
}
