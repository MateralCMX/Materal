using Materal.ConfigCenter.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.Example
{
    public class Program
    {
        public static async Task Main()
        {
            IMateralConfigurationBuilder builder = new MateralConfigurationBuilder("http://192.168.0.101:8201", "MateralExample");
            IConfiguration configuration = await builder.AddDefaultNamespace().BuildMateralConfigAsync();
            string value = configuration.GetValue("TestConfig1");
            Console.WriteLine($"TestConfig1:{value}");
        }
    }
}
