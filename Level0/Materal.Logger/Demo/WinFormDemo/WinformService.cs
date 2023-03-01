using Materal.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WinFormDemo
{
    public static class WinformService
    {
        public static IServiceProvider Services { get; private set; }
        static WinformService()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralLogger();
            Services = serviceCollection.BuildServiceProvider();
            IConfiguration configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .Build();
            LoggerManager.Init(null, configuration);
        }
        /// <summary>
        /// 获得服务或者默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="LoggerException"></exception>
        public static T? GetServiceOrDefatult<T>()
        {
            T? result = Services.GetService<T>();
            return result;
        }
    }
}
