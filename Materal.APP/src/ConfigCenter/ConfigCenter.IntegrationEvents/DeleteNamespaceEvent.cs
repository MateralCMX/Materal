using Materal.TFMS.EventBus;
using System.ComponentModel.DataAnnotations;

namespace ConfigCenter.IntegrationEvents
{
    public class DeleteNamespaceEvent : IntegrationEvent
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage = "项目名称不能为空")]
        public string ProjectName { get; set; }
        /// <summary>
        /// 命名空间名称
        /// </summary>
        [Required(ErrorMessage = "命名空间名称不能为空")]
        public string NamespaceName { get; set; }
    }
}
