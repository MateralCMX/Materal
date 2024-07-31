namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 立即执行计划触发器数据
    /// </summary>
    public class NowPlanTriggerData() : PlanTriggerDataBase("立即执行")
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; } = DateTime.Now;
        /// <inheritdoc/>
        public override bool CanRepeated => false;
        /// <inheritdoc/>
        public override string GetDescriptionText() => $"将在 {StartTime:yyyy-MM-dd HH:mm:ss} 执行一次。";
    }
}
