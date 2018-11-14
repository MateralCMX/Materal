namespace Materal.WebSocket.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// 处理器名称
        /// </summary>
        string HandlerName { get; }
        /// <summary>
        /// 字符串数据
        /// </summary>
        string StringData { get; set; }
        /// <summary>
        /// 二进制数据
        /// </summary>
        byte[] ByteArrayData { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        string Message { get; set; }
    }
}
