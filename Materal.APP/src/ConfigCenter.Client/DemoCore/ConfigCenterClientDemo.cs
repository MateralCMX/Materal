using ConfigCenter.Client;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DemoCore
{
    public static class ConfigCenterClientDemo
    {
        public static async Task<string> GetConfigAsync()
        {
            const string configUrl = "http://192.168.0.101:8900/DEVEnvAPI";
            //const string configUrl = "http://192.168.0.101:8703";
            //const string configUrl = "qweqweqwe";
            IMateralConfigurationBuilder configurationBuilder = new MateralConfigurationBuilder(configUrl, "TestProject")
                .AddDefaultNamespace();
            IConfiguration configuration = await configurationBuilder.BuildMateralConfigAsync();
            string result = configuration["Test"];
            return result;
        }
    }
}
