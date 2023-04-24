using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.Abstractions.DataTransmitModel
{
    public class PlanDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 计划触发器数据
        /// </summary>
        public IPlanTrigger PlanTriggerData { get; set; } = new NonePlanTrigger();
        /// <summary>
        /// 调度器唯一标识
        /// </summary>
        public Guid ScheduleID { get; set; }
        /// <summary>
        /// 调度器名称
        /// </summary>
        public string ScheduleName { get; set; } = string.Empty;
    }
}
