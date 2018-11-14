using Materal.WebSocket.Commands;
using System;

namespace Materal.WebSocket.Client.Model
{
    /// <inheritdoc />
    /// <summary>
    /// 发送命令事件参数
    /// </summary>
    public class SendCommandEventArgs : EventArgs
    {
        /// <summary>
        /// 显示消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 命令对象
        /// </summary>
        public ICommand Command { get; set; }
    }
}
