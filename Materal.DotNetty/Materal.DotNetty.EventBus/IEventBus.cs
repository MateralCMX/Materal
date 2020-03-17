namespace Materal.DotNetty.EventBus
{
    public interface IEventBus
    {
        /// <summary>
        /// 获得事件处理器
        /// </summary>
        /// <param name="eventHandlerName"></param>
        /// <returns></returns>
        IEventHandler GetEventHandler(string eventHandlerName);
    }
}
