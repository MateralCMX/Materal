using System.Threading.Tasks;
using Materal.ConfigCenter.Client;
using Microsoft.Extensions.Configuration;

namespace Materal.Example
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IMateralConfigurationBuilder configurationBuilder = new MateralConfigurationBuilder("http://192.168.0.101:8201/", "TestProject")
                .AddDefaultNamespace()
                .AddNamespace("TestNamespace");
            IConfiguration configuration = await configurationBuilder.BuildMateralConfigAsync();
            string testConfig11 = configuration.GetValue("TestConfig", "Application");
            string testConfig12 = configuration.GetValue("TestConfig", "TestNamespace");
            string testConfig2 = configuration.GetValue("TestConfig2");

            IMateralConfigurationBuilder configurationBuilder2 = new MateralConfigurationBuilder("http://192.168.0.101:8202/", "TestProject")
                .AddDefaultNamespace()
                .AddNamespace("TestNamespace");
            IConfiguration configuration2 = await configurationBuilder2.BuildMateralConfigAsync();
            string test2Config11 = configuration2.GetValue("TestConfig", "Application");
            string test2Config12 = configuration2.GetValue("TestConfig", "TestNamespace");
            string test2Config2 = configuration2.GetValue("TestConfig2");
        }
    }
}
