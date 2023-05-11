using Materal.BusinessFlow.Abstractions.DTO;
using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Domain
{
    /// <summary>
    /// 流程模版
    /// </summary>
    public class FlowTemplate : BaseDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 数据模型唯一标识
        /// </summary>
        [Required]
        public Guid DataModelID { get; set; }
    }
}
