namespace Materal.WebSocket.Events
{
    public interface IEvent
    {
        /// <summary>
        /// 处理器名称
        /// </summary>
        string HandlerName { get; }
        /// <summary>
        /// 文本数据
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
