using System.ComponentModel.DataAnnotations;
using Materal.TFMS.EventBus;

namespace ConfigCenter.IntegrationEvents
{
    public class DeleteProjectEvent : IntegrationEvent
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage = "项目名称不能为空")]
        public string ProjectName { get; set; }
    }
}
