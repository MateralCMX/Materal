namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 执行一次计划触发器数据
    /// </summary>
    public class OneTimePlanTriggerData() : PlanTriggerDataBase("执行一次")
    {
        /// <inheritdoc/>
        public override bool CanRepeated => false;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;
        /// <inheritdoc/>
        public override string GetDescriptionText() => $"将在 {StartTime:yyyy-MM-dd HH:mm:ss} 执行一次。";
    }
}
