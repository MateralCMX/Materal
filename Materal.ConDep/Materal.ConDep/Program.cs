using System;
using System.Reflection;
using System.Threading.Tasks;
using Materal.ConDep.Common;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;

namespace Materal.ConDep
{
    /// <summary>
    /// 应用程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 主入口
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args)
        {
            string version = Assembly.Load("Materal.ConDep").GetName().Version.ToString();
            Console.Title = $"Materal.ConDep [版本号:{version}]";
            if (TryRegisterService())
            {
                try
                {
                    var conDepServer = ApplicationData.GetService<IConDepServer>();
                    await conDepServer.RunServerAsync();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.ConDepServerWriteLine("服务发生致命错误", "错误", ConsoleColor.Red);
                    ConsoleHelper.ConDepServerErrorWriteLine(ex);
                }
            }
            else
            {
                ConsoleHelper.ConDepServerWriteLine("注册服务失败", "错误", ConsoleColor.Red);
            }
        }
        /// <summary>
        /// 注册依赖注入服务
        /// </summary>
        public static bool TryRegisterService()
        {
            try
            {
                ApplicationData.RegisterServices(ConDepServerDIExtension.AddConDepServer);
                ApplicationData.BuildServices();
                LogManager.LoadConfiguration("NLog.config");
                LogManager.Configuration.Install(new InstallationContext());
                return true;
            }
            catch (Exception ex)
            {
                ConsoleHelper.ConDepServerErrorWriteLine(ex);
                return false;
            }
        }
    }
}
