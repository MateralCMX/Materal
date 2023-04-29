using Materal.Oscillator.Abstractions.PlanTriggers;
using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models
{
    /// <summary>
    /// 添加计划模型
    /// </summary>
    public class AddPlanModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
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
        /// 触发器配置
        /// </summary>
        [Required(ErrorMessage = "计划触发器数据为空")]
        public IPlanTrigger PlanTriggerData { get; set; } = new NonePlanTrigger();
        /// <summary>
        /// 计划触发器类型
        /// </summary>
        [Required(ErrorMessage = "计划触发器类型为空"), StringLength(100, ErrorMessage = "计划触发器类型长度大于100")]
        public string PlanTriggerType => PlanTriggerData.GetType().Name;
    }
}
