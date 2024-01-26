using Materal.TFMS.EventBus;

namespace Materal.MergeBlock.EventBusTest.Events
{
    /// <summary>
    /// 我的事件
    /// </summary>
    public class MyEvent : IntegrationEvent
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
