using Materal.BusinessFlow.Abstractions.DTO;
using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.Abstractions.Domain
{
    /// <summary>
    /// 数据模型
    /// </summary>
    public class DataModel : BaseDomain, IDTO
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(40)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
