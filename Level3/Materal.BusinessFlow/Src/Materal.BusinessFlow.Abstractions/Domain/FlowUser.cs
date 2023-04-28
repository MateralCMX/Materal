using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Domain
{
    /// <summary>
    /// 流程用户映射
    /// </summary>
    public class FlowUser : BaseDomain
    {
        /// <summary>
        /// 流程模版唯一标识
        /// </summary>
        [Required]
        public Guid FlowTemplateID { get; set; }
        /// <summary>
        /// 流程唯一标识
        /// </summary>
        [Required]
        public Guid FlowID { get; set; }
        /// <summary>
        /// 流程记录唯一标识
        /// </summary>
        [Required]
        public Guid FlowRecordID { get; set; }
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required]
        public Guid UserID { get; set; }
    }
}
