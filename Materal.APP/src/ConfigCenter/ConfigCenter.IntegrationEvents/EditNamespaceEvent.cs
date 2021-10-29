using System.ComponentModel.DataAnnotations;
using Materal.TFMS.EventBus;

namespace ConfigCenter.IntegrationEvents
{
    public class EditNamespaceEvent : IntegrationEvent
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage = "项目名称不能为空")]
        public string ProjectName { get; set; }
        /// <summary>
        /// 旧命名空间名称
        /// </summary>
        [Required(ErrorMessage = "命名空间名称不能为空")]
        public string OldNamespaceName { get; set; }
        /// <summary>
        /// 新命名空间名称
        /// </summary>
        [Required(ErrorMessage = "命名空间名称不能为空")]
        public string NewNamespaceName { get; set; }
    }
}
