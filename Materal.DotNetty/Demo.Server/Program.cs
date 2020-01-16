using Demo.Common;
using Materal.DotNetty.Server.Core;
using Materal.DotNetty.Server.CoreImpl;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Demo.Server
{
    public class Program
    {
        public static async Task Main()
        {
            string version = Assembly.GetCallingAssembly().GetName().Version.ToString();
            Console.Title = $"Materal.DotNetty.Server [版本号:{version}]";
            if (TryRegisterService())
            {
                try
                {
                    var dotNettyServer = ApplicationService.GetService<IDotNettyServer>();
                    dotNettyServer.OnException += ConsoleHelper.ServerWriteError;
                    dotNettyServer.OnGetCommand += Console.ReadLine;
                    dotNettyServer.OnMessage += message => ConsoleHelper.ServerWriteLine(message);
                    dotNettyServer.OnSubMessage += (message, subTitle) => ConsoleHelper.ServerWriteLine(message, subTitle);
                    await dotNettyServer.RunServerAsync();
                }
                catch (Exception exception)
                {
                    ConsoleHelper.ServerWriteLine("服务器发生致命错误", "错误", ConsoleColor.Red);
                    ConsoleHelper.ServerWriteError(exception);
                }
            }
            else
            {
                ConsoleHelper.ServerWriteLine("注册服务失败", "失败", ConsoleColor.Red);
            }

        }
        #region 私有方法
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <returns></returns>
        private static bool TryRegisterService()
        {
            try
            {
                ApplicationService.RegisterServices(ServerDIExtension.AddServer, MateralDotNettyServerCoreDIExtension.AddMateralDotNettyServerCore);
                ApplicationService.BuildServices();
                return true;
            }
            catch (Exception exception)
            {
                ConsoleHelper.ServerWriteError(exception);
                return false;
            }
        }
        #endregion
    }
}
