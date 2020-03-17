namespace Materal.DotNetty.EventBus
{
    public interface IEvent
    {
        /// <summary>
        /// 事件
        /// </summary>
        string Event { get; }
        /// <summary>
        /// 事件处理器
        /// </summary>
        string EventHandler { get; }
    }
}
