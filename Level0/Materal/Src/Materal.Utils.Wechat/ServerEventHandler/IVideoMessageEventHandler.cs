using Materal.Utils.Wechat.Model.Event;

namespace Materal.Utils.Wechat.ServerEventHandler
{
    /// <summary>
    /// 视频消息事件处理器
    /// </summary>
    public interface IVideoMessageEventHandler : IEventHandler<VideoMessageEvent>
    {
    }
}
