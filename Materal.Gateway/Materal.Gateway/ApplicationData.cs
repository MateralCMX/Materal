using Microsoft.Extensions.DependencyInjection;
using System;

namespace Materal.Gateway
{
    /// <summary>
    /// 应用程序数据
    /// </summary>
    public static class ApplicationData
    {
        /// <summary>
        /// Ocelot配置Url
        /// </summary>
        public const string OcelotConfigUrl = "/administration";
        /// <summary>
        /// Host
        /// </summary>
        public static string Host = "http://loclhost";
        /// <summary>
        /// 容器
        /// </summary>
        public static IServiceCollection Services;
        /// <summary>
        /// 依赖注入服务
        /// </summary>
        public static IServiceProvider ServiceProvider { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="appArgs"></param>
        public static void Init(string[] appArgs)
        {
            foreach (string arg in appArgs)
            {
                string[] tempArgs = arg.Split('=');
                if (tempArgs.Length != 2) continue;
                string argName = tempArgs[0];
                string argValue = tempArgs[1];
                switch (argName)
                {
                    case "--urls":
                        Host = argValue;
                        break;
                }
            }
        }
        /// <summary>
        /// 构建服务
        /// </summary>
        public static void BuildServices()
        {
            ServiceProvider = Services.BuildServiceProvider();
        }
        /// <summary>
        /// 获得服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
