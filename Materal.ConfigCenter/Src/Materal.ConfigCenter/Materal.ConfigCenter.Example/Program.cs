using Materal.ConfigCenter.Client;
using Microsoft.Extensions.Configuration;

namespace Materal.ConfigCenter.Example
{
    public class Program
    {
        public static void Main()
        {
            IMateralConfigurationBuilder builder = new MateralConfigurationBuilder("http://192.168.0.101:8201", "MateralExample");
            IConfiguration configuration = builder.AddDefaultNamespace().BuildMateralConfig();
            var value = configuration.GetValue("TestConfig1");
        }
    }
}
