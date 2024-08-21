namespace Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers
{
    /// <summary>
    /// 执行一次时间触发器数据
    /// </summary>
    public class OneTimeTimeTriggerData() : TimeTriggerDataBase("执行一次")
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeOnly StartTime { get; set; } = new(0, 0, 0);
        /// <inheritdoc/>        
        public override string GetDescriptionText() => $"{StartTime} 执行一次。";
    }
}
