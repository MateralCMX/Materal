using Materal.TFMS.EventBus;

namespace XMJ.Authority.IntegrationEvents
{
    public class ProjectDeleteEvent : IntegrationEvent
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public Guid ProjectID { get; set; }
    }
}