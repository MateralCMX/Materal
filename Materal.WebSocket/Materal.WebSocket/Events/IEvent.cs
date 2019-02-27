namespace Materal.WebSocket.Events
{
    public interface IEvent
    {
        /// <summary>
        /// 处理器名称
        /// </summary>
        string HandlerName { get; }
    }
}
