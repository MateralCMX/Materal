using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Domain
{
    /// <summary>
    /// 计划
    /// </summary>
    [Serializable]
    public class PlanView : BaseDomain, IDomain, ISchedultView
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        [Required(ErrorMessage = "调度器唯一标识为空")]
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 启用标识
        /// </summary>
        [Required(ErrorMessage = "启用标识为空")]
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(400, ErrorMessage = "描述长度大于400")]
        public string? Description { get; set; }
        /// <summary>
        /// 计划触发器类型
        /// </summary>
        [Required(ErrorMessage = "计划触发器类型为空"), StringLength(100, ErrorMessage = "计划触发器类型长度大于100")]
        public string PlanTriggerType { get; set; } = string.Empty;
        /// <summary>
        /// 计划触发器数据
        /// </summary>
        [Required(ErrorMessage = "计划触发器数据为空"), StringLength(4000, ErrorMessage = "计划触发器数据长度大于4000")]
        public string PlanTriggerData { get; set; } = string.Empty;
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
