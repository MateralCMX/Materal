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
            ConsoleHelper.AppName = "Server";
            Version version = Assembly.Load("Demo.Server").GetName().Version;
            if (version != null)
            {
                Console.Title = $"Demo.Server [版本号:{version}]";
            }
            if (TryRegisterService())
            {
                try
                {
                    var dotNettyServer = ApplicationService.GetService<IDotNettyServer>();
                    dotNettyServer.OnConfigHandler += DotNettyServer_OnConfigHandler;
                    dotNettyServer.OnException += ConsoleHelper.WriteError;
                    dotNettyServer.OnGetCommand += Console.ReadLine;
                    dotNettyServer.OnMessage += message => ConsoleHelper.WriteLine(message);
                    dotNettyServer.OnSubMessage += (message, subTitle) => ConsoleHelper.WriteLine(message, subTitle);
                    await dotNettyServer.RunAsync(ApplicationConfig.ServerConfig);
                    ConsoleHelper.WriteLine($"已监听http://{ApplicationConfig.ServerConfig.Host}:{ApplicationConfig.ServerConfig.Port}");
                    ConsoleHelper.WriteLine($"ws://{ApplicationConfig.ServerConfig.Host}:{ApplicationConfig.ServerConfig.Port}/websocket");
                    ConsoleHelper.WriteLine("输入Stop停止服务");
                    var inputKey = string.Empty;
                    while (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
                    {
                        inputKey = Console.ReadLine();
                        if (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
                        {
                            ConsoleHelper.WriteError(new Exception("未识别命令请重新输入"));
                        }
                    }
                    await dotNettyServer.StopAsync();
                }
                catch (Exception exception)
                {
                    ConsoleHelper.WriteLine("服务器发生致命错误", "错误", ConsoleColor.Red);
                    ConsoleHelper.WriteError(exception);
                }
            }
            else
            {
                ConsoleHelper.WriteLine("注册服务失败", "失败", ConsoleColor.Red);
            }

        }

        private static void DotNettyServer_OnConfigHandler(IServerChannelHandler channelHandler)
        {
            channelHandler.AddLastHandler(ApplicationService.GetService<WebSocketHandler>());
            channelHandler.AddLastHandler(ApplicationService.GetService<WebAPIHandler>());
            channelHandler.AddLastHandler(ApplicationService.GetService<FileHandler>());
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
                ConsoleHelper.WriteError(exception);
                return false;
            }
        }
        #endregion
    }
}
