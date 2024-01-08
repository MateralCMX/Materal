using Materal.Abstractions;
using Materal.Logger.ConfigModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RC.ConfigClient.Demo
{
    public class Program
    {
        public static async Task Main()
        {
            #region 初始化DI
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger(option =>
            {
                option.AddConsoleTarget("LifeConsole");
                option.AddAllTargetsRule();
            });
            MateralServices.Services = serviceCollection.BuildServiceProvider();
            #endregion
            const string url = "https://gateway.xmjriyu.com/RCESDEVAPI";
            const string projectName = "XMJ_Educational_WebAPI";
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
                string? testValue = _configuration.GetValue("AppName");
                Console.WriteLine(testValue);
                await Task.Delay(1000);
            }
        }
    }
}
