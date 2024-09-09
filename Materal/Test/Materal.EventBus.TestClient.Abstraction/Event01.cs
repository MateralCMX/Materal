using Materal.EventBus.Abstraction;

namespace Materal.EventBus.TestClient.Abstraction
{
    public class Event01 : IEvent
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
