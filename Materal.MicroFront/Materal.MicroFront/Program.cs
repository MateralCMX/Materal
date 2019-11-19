using Materal.MicroFront.Common;
using NLog;
using NLog.Config;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Materal.MicroFront
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
            string version = Assembly.Load("Materal.MicroFront").GetName().Version.ToString();
            Console.Title = $"Materal.MicroFront [版本号:{version}]";
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}Application/Temp";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            if (TryRegisterService())
            {
                try
                {
                    var conDepServer = ApplicationData.GetService<IMicroFrontServer>();
                    await conDepServer.RunServerAsync();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.MicroFrontServerWriteLine("服务发生致命错误", "错误", ConsoleColor.Red);
                    ConsoleHelper.MicroFrontServerErrorWriteLine(ex);
                }
            }
            else
            {
                ConsoleHelper.MicroFrontServerWriteLine("注册服务失败", "错误", ConsoleColor.Red);
            }
        }
        /// <summary>
        /// 注册依赖注入服务
        /// </summary>
        public static bool TryRegisterService()
        {
            try
            {
                ApplicationData.RegisterServices(MicroFrontServerDIExtension.AddMicroFrontServer);
                ApplicationData.BuildServices();
                LogManager.LoadConfiguration("NLog.config");
                LogManager.Configuration.Install(new InstallationContext());
                return true;
            }
            catch (Exception ex)
            {
                ConsoleHelper.MicroFrontServerErrorWriteLine(ex);
                return false;
            }
        }
    }
}
