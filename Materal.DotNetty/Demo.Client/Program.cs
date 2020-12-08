using Demo.Commands;
using Demo.Common;
using Materal.DotNetty.EventBus;
using Materal.WebSocketClient.Core;
using Materal.WebSocketClient.CoreImpl;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Demo.Client
{
    public class Program
    {
        public static async Task Main()
        {
            ConsoleHelper.AppName = "Client";
            Version version = Assembly.Load("Demo.Client").GetName().Version;
            if (version != null)
            {
                Console.Title = $"Demo.Client [版本号:{version}]";
            }
            if (TryRegisterService())
            {
                try
                {
                    var config = new ClientConfig
                    {
                        Url = "ws://192.168.0.101:8801/websocket"
                    };
                    var eventBus = ApplicationService.GetService<IEventBus>();
                    IWebSocketClient client = new WebSocketClientImpl(config, eventBus);
                    client.OnException += ConsoleHelper.WriteError;
                    client.OnConnectionSuccess += async () =>
                    {
                        await client.SendCommandAsync(new SendMessageCommand
                        {
                            Message = "Hello World!"
                        });
                    };
                    client.OnSubMessage += (message, subTitle) => ConsoleHelper.WriteLine(message, subTitle);
                    client.OnConnectionFail += async () =>
                    {
                        await client.RunAsync();
                    };
                    client.OnClose += async () =>
                    {
                        await client.RunAsync();
                    };
                    await client.RunAsync();
                    ConsoleHelper.WriteLine("输入Stop停止服务");
                    var inputKey = string.Empty;
                    while (!string.Equals(inputKey, "Stop", StringComparison.Ordinal))
                    {
                        inputKey = Console.ReadLine();
                        await client.SendCommandAsync(new SendMessageCommand
                        {
                            Message = inputKey
                        });
                        if (string.Equals(inputKey, "Stop", StringComparison.Ordinal)) break;
                    }
                    await client.StopAsync();
                }
                catch (Exception exception)
                {
                    ConsoleHelper.WriteLine("发生致命错误", "错误", ConsoleColor.Red);
                    ConsoleHelper.WriteError(exception);
                }
            }
            else
            {
                ConsoleHelper.WriteLine("注册服务失败", "失败", ConsoleColor.Red);
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
                ApplicationService.RegisterServices(ClientDIExtension.AddClient, MateralWebSocketClientCoreDIExtension.AddMateralWebSocketClientCore);
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
