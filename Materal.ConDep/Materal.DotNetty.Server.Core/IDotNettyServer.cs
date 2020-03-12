﻿using System;
using System.Threading.Tasks;

namespace Materal.DotNetty.Server.Core
{
    public interface IDotNettyServer
    {
        /// <summary>
        /// 产生消息
        /// </summary>
        event Action<string> OnMessage;
        /// <summary>
        /// 产生消息
        /// </summary>
        event Action<string, string> OnSubMessage;
        /// <summary>
        /// 产生消息
        /// </summary>
        event Action<Exception> OnException;
        /// <summary>
        /// 获取命令
        /// </summary>
        event Func<string> OnGetCommand;
        /// <summary>
        /// 
        /// </summary>
        event Action<IMateralChannelHandler> OnConfigHandler;
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        Task RunServerAsync(ServerConfig serverConfig);
    }
}
