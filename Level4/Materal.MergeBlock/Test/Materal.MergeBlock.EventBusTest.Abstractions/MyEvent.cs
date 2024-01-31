using Materal.EventBus.Abstraction;

namespace Materal.MergeBlock.EventBusTest.Abstractions
{
    /// <summary>
    /// 我的事件
    /// </summary>
    public class MyEvent : IEvent
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
