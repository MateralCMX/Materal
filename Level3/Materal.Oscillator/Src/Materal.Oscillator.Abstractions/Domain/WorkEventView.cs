using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Domain
{
    /// <summary>
    /// 任务事件
    /// </summary>
    [Serializable]
    public class WorkEventView : BaseDomain, IDomain, ISchedultView
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
        /// 调度唯一标识
        /// </summary>
        [Required(ErrorMessage = "调度器唯一标识为空")]
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
        /// <summary>
        /// 调度器名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string ScheduleName { get; set; } = string.Empty;
        /// <summary>
        /// 业务领域
        /// </summary>
        [Required(ErrorMessage = "业务领域为空"), StringLength(100, ErrorMessage = "业务领域长度大于100")]
        public string Territory { get; set; } = string.Empty;
    }
}
