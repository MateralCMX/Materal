using Microsoft.Extensions.Configuration;

namespace RC.ConfigClient.Demo
{
    public class Program
    {
        public static async Task Main()
        {
            const string url = "http://121.40.18.199:8701/RCES_DEVAPI";
            const string projectName = "XMJProject";
            string[] namespaces = new[]
            {
                "WebAPI",
                "Demo"
            };
            //IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
            //    .SetConfigCenter(url, projectName, 10)
            //    .AddDefaultNameSpace()
            //    .AddNameSpace(namespaces[0])
            //    .AddNameSpace(namespaces[1]);//会优先获取后加载命名空间的值
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddDefaultNameSpace(url, projectName, 10)
                .AddNameSpace(namespaces[0])
                .AddNameSpace(namespaces[1]);//会优先获取后加载命名空间的值
            IConfiguration _configuration = configurationBuilder.Build();
            while (true)
            {
                string? testValue = _configuration.GetValue("Test");
                Console.WriteLine(testValue);
                await Task.Delay(1000);
            }
        }
    }
}
