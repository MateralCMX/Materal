using Materal.EventBus.Abstraction;

namespace Materal.EventBus.TestClient.Abstraction
{
    public class Event02 : IEvent
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
