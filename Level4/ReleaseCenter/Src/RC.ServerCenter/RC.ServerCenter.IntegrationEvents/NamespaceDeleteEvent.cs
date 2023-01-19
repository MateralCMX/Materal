using Materal.TFMS.EventBus;

namespace XMJ.Authority.IntegrationEvents
{
    public class NamespaceDeleteEvent : IntegrationEvent
    {
        /// <summary>
        /// 命名空间唯一标识
        /// </summary>
        public Guid NamespaceID { get; set; }
    }
}