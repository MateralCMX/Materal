using Materal.WebSocket.Commands;
using Materal.WebSocket.Model;
using System;
using System.Threading.Tasks;

namespace Materal.WebSocket
{
    /// <inheritdoc />
    /// <summary>
    /// WebStock客户端
    /// </summary>
    public interface IClient : IDisposable
    {
        #region 事件
        /// <summary>
        /// 配置更改时
        /// </summary>
        event ConfigEvent OnConfigChange;
        /// <summary>
        /// 当服务器状态发生改变时
        /// </summary>
        event ConnectServerEvent OnStateChange;
        /// <summary>
        /// 当有消息传递时
        /// </summary>
        event ReceiveEventEvent OnReceiveEvent;
        /// <summary>
        /// 当有消息传递时
        /// </summary>
        event SendCommandEvent OnSendCommand;
        #endregion
        ClientConfigModel Config { get; }
        ClientStateEnum State { get;}
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="config">配置对象</param>
        void SetConfig(ClientConfigModel config);
        /// <summary>
        /// 启动客户端
        /// </summary>
        /// <returns></returns>
        Task StartAsync();
        /// <summary>
        /// 重启客户端
        /// </summary>
        /// <returns></returns>
        Task ReloadAsync();
        /// <summary>
        /// 停止客户端
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        Task SendCommandAsync(ICommand command);
        /// <summary>
        /// 发送String命令
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        Task SendCommandByStringAsync(ICommand command);
        /// <summary>
        /// 发送ByteArray命令
        /// </summary>
        /// <param name="command">命令对象</param>
        /// <returns></returns>
        Task SendCommandByBytesAsync(ICommand command);
        /// <summary>
        /// 开始监听事件
        /// </summary>
        Task StartListeningEventAsync();
    }
    #region 委托定义
    /// <summary>
    /// 配置事件委托
    /// </summary>
    /// <param name="args">参数</param>
    public delegate void ConfigEvent(ConfigEventArgs args);
    /// <summary>
    /// 连接服务器事件委托
    /// </summary>
    /// <param name="args">参数</param>
    public delegate void ConnectServerEvent(ConnectServerEventArgs args);
    /// <summary>
    /// 接收事件事件委托
    /// </summary>
    /// <param name="args">参数</param>
    public delegate void ReceiveEventEvent(ReceiveEventEventArgs args);
    /// <summary>
    /// 发送命令事件委托
    /// </summary>
    /// <param name="args">参数</param>
    public delegate void SendCommandEvent(SendCommandEventArgs args);
    #endregion
}
