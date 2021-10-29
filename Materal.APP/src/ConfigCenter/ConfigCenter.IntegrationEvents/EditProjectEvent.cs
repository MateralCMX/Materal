using System.ComponentModel.DataAnnotations;
using Materal.TFMS.EventBus;

namespace ConfigCenter.IntegrationEvents
{
    public class EditProjectEvent : IntegrationEvent
    {
        /// <summary>
        /// 旧项目名称
        /// </summary>
        [Required(ErrorMessage = "项目名称不能为空")]
        public string OldProjectName { get; set; }
        /// <summary>
        /// 新项目名称
        /// </summary>
        [Required(ErrorMessage = "项目名称不能为空")]
        public string NewProjectName { get; set; }
    }
}
