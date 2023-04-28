using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Domain
{
    /// <summary>
    /// 步骤
    /// </summary>
    public class Step : BaseDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 流程模版唯一标识
        /// </summary>
        [Required]
        public Guid FlowTemplateID { get; set; }
        /// <summary>
        /// 下一步唯一标识
        /// </summary>
        public Guid? NextID { get; set; }
        /// <summary>
        /// 上一步唯一标识
        /// </summary>
        public Guid? UpID { get; set; }
    }
}
