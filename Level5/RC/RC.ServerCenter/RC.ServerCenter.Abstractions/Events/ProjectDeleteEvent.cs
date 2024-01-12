using Materal.TFMS.EventBus;

namespace RC.ServerCenter.Abstractions.Events
{
    /// <summary>
    /// 项目删除事件
    /// </summary>
    public class ProjectDeleteEvent : IntegrationEvent
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public Guid ProjectID { get; set; }
    }
}