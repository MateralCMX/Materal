using System;
using System.Threading.Tasks;
using Materal.DotNetty.CommandBus;

namespace Materal.WebSocketClient.Core
{
    public interface IWebSocketClient
    {
        /// <summary>
        /// 连接成功
        /// </summary>
        event Action OnConnectionSuccess;
        /// <summary>
        /// 连接失败
        /// </summary>
        event Action OnConnectionFail;
        /// <summary>
        /// 连接关闭
        /// </summary>
        event Action OnClose;
        /// <summary>
        /// 产生消息
        /// </summary>
        event Action<string, string> OnSubMessage;
        /// <summary>
        /// 产生消息
        /// </summary>
        event Action<Exception> OnException;
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        Task RunAsync();
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task SendCommandAsync(ICommand command);
    }
}
