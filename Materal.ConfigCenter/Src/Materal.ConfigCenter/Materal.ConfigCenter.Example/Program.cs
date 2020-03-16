using Materal.DotNetty.Client.CoreImpl;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Materal.DotNetty.Client.Core;
using Materal.DotNetty.Common;

namespace Materal.ConfigCenter.Example
{
    public class Program
    {
        public static async Task Main()
        {
            //IMateralConfigurationBuilder builder = new MateralConfigurationBuilder("http://192.168.0.101:8201", "MateralExample");
            //IConfiguration configuration = builder.AddDefaultNamespace().BuildMateralConfig();
            //var value = configuration.GetValue("TestConfig1");
            string version = Assembly.Load("Materal.ConfigCenter.Example").GetName().Version.ToString();
            Console.Title = $"MateralConfigExample [版本号:{version}]";
            if (TryRegisterService())
            {
                try
                {
                    var dotNettyClient = ApplicationService.GetService<IDotNettyClient>();
                    dotNettyClient.OnConfigHandler += DotNettyServer_OnConfigHandler;
                    dotNettyClient.OnException += ConsoleHelper.ServerWriteError;
                    dotNettyClient.OnGetCommand += Console.ReadLine;
                    dotNettyClient.OnMessage += message => ConsoleHelper.ServerWriteLine(message);
                    dotNettyClient.OnSubMessage += (message, subTitle) => ConsoleHelper.ServerWriteLine(message, subTitle);
                    await dotNettyClient.RunAsync(ApplicationConfig.ServerConfig);
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
        private static void DotNettyServer_OnConfigHandler(IClientChannelHandler channelHandler)
        {
            channelHandler.AddLastHandler(ApplicationService.GetService<WebSocketHandler>());
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
                ApplicationService.RegisterServices(ClientDIExtension.AddServer, MateralDotNettyClientCoreDIExtension.AddMateralDotNettyClientCore);
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
