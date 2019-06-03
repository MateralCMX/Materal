using System;
using System.ComponentModel.DataAnnotations;
namespace Authority.PresentationModel.Incident.Request
{
    /// <summary>
    /// 事件修改请求模型
    /// </summary>
    public class EditIncidentRequestModel : AddIncidentRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不可以为空")]
        public Guid ID { get; set; }
    }
}
