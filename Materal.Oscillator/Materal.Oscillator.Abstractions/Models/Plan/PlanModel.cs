using Materal.Oscillator.Abstractions.PlanTriggers;
using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.Plan
{
    public class PlanModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称为空"), StringLength(100, ErrorMessage = "名称长度大于100")]
        public string Name { get; set; } = string.Empty;
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
    }
}
