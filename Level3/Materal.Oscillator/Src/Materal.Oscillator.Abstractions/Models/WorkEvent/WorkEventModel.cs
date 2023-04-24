using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.WorkEvent
{
    public class WorkEventModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "值为空"), StringLength(100, ErrorMessage = "值长度大于100")]
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
    }
}
