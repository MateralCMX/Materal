﻿using Materal.DotNetty.Server.Core;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Demo.Common;
using Materal.DotNetty.Server.CoreImpl;

namespace Demo.Server
{
    public class Program
    {
        public static async Task Main()
        {
            string version = Assembly.Load("Demo.Server").GetName().Version.ToString();
            Console.Title = $"Demo.Server [版本号:{version}]";
            if (TryRegisterService())
            {
                try
                {
                    var dotNettyServer = ApplicationService.GetService<IDotNettyServer>();
                    dotNettyServer.OnConfigHandler += DotNettyServer_OnConfigHandler;
                    dotNettyServer.OnException += ConsoleHelper.ServerWriteError;
                    dotNettyServer.OnGetCommand += Console.ReadLine;
                    dotNettyServer.OnMessage += message => ConsoleHelper.ServerWriteLine(message);
                    dotNettyServer.OnSubMessage += (message, subTitle) => ConsoleHelper.ServerWriteLine(message, subTitle);
                    await dotNettyServer.RunServerAsync(ApplicationConfig.ServerConfig);
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

        private static void DotNettyServer_OnConfigHandler(IMateralChannelHandler channelHandler)
        {
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
                ConsoleHelper.ServerWriteError(exception);
                return false;
            }
        }
        #endregion
    }
}
