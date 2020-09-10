using Materal.ConDep.Common;
using Materal.ConDep.Services;
using Materal.DotNetty.Server.Core;
using Materal.DotNetty.Server.CoreImpl;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Materal.ConDep.Server
{
    public class Program
    {
        public static async Task Main()
        {
            Version version = Assembly.Load("Materal.ConDep.Server").GetName().Version;
            Console.Title = $"Materal.ConDep.Server [版本号:{version}]";
            if (TryRegisterService())
            {
                try
                {
                    var serverManage = ApplicationService.GetService<IServerManage>();
                    await serverManage.RegisterServerAsync();
                    var dotNettyServer = ApplicationService.GetService<IDotNettyServer>();
                    dotNettyServer.OnConfigHandler += DotNettyServer_OnConfigHandler;
                    dotNettyServer.OnException += ConsoleHelper.ServerWriteError;
                    dotNettyServer.OnGetCommand += Console.ReadLine;
                    dotNettyServer.OnMessage += message => ConsoleHelper.ServerWriteLine(message);
                    dotNettyServer.OnSubMessage += (message, subTitle) => ConsoleHelper.ServerWriteLine(message, subTitle);
                    await dotNettyServer.RunAsync(ApplicationConfig.ServerConfig);
                    ConsoleHelper.ServerWriteLine($"已监听http://{ApplicationConfig.ServerConfig.Host}:{ApplicationConfig.ServerConfig.Port}/api");
                    ConsoleHelper.ServerWriteLine($"已监听http://{ApplicationConfig.ServerConfig.Host}:{ApplicationConfig.ServerConfig.Port}");
                    var inputKey = string.Empty;
                    while (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
                    {
                        inputKey = Console.ReadLine();
                        if (string.Equals(inputKey, "Register", StringComparison.Ordinal))
                        {
                            await serverManage.RegisterServerAsync();
                        }
                        else if (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
                        {
                            ConsoleHelper.ServerWriteError(new Exception("未识别命令请重新输入"));
                        }
                    }
                    await dotNettyServer.StopAsync();
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

        private static void DotNettyServer_OnConfigHandler(IServerChannelHandler channelHandler)
        {
            channelHandler.AddLastHandler(ApplicationService.GetService<WebAPIHandler>());
            channelHandler.AddLastHandler(ApplicationService.GetService<ConDepFileHandler>());
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
