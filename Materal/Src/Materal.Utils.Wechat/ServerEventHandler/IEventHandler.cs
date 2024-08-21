using Materal.Utils.Wechat.Model;

namespace Materal.Utils.Wechat.ServerEventHandler
{
    /// <summary>
    /// 事件处理器
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<TEvent>
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task<ReplyMessageModel?> HandlerAsync(TEvent @event);
    }
}
