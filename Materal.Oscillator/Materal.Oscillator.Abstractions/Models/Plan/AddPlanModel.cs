using System.ComponentModel.DataAnnotations;

namespace Materal.Oscillator.Abstractions.Models.Plan
{
    public class AddPlanModel : PlanModel
    {
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
        /// 计划触发器类型
        /// </summary>
        [Required(ErrorMessage = "计划触发器类型为空"), StringLength(100, ErrorMessage = "计划触发器类型长度大于100")]
        public string PlanTriggerType => PlanTriggerData.GetType().Name;
    }
}
