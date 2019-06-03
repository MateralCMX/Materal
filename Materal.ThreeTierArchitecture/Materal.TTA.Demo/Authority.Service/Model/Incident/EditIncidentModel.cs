using System;
namespace Authority.Service.Model.Incident
{
    /// <summary>
    /// 事件修改模型
    /// </summary>
    public class EditIncidentModel : AddIncidentModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
    }
}
