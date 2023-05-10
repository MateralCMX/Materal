using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.WebAPIControllers.Models.FlowTemplate
{
    public class EditFlowTemplateModel : AddFlowTemplateModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required]
        public Guid ID { get; set; }
    }
}
