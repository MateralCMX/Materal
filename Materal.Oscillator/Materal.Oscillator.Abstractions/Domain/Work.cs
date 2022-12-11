using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Domain
{
    /// <summary>
    /// 任务
    /// </summary>
    [Serializable]
    public class Work : BaseDomain, IDomain
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 任务类型
        /// </summary>
        [Required(ErrorMessage = "任务类型为空"), StringLength(100, ErrorMessage = "任务类型长度大于100")]
        public string WorkType { get; set; } = string.Empty;
        /// <summary>
        /// 任务数据
        /// </summary>
        [Required(ErrorMessage = "任务数据为空"), StringLength(4000, ErrorMessage = "任务数据长度大于4000")]
        public string WorkData { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
    }
}
