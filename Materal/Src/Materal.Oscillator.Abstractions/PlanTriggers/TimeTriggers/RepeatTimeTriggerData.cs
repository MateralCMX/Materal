namespace Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers
{
    /// <summary>
    /// 重复执行时间触发器数据
    /// </summary>
    public class RepeatTimeTriggerData() : TimeTriggerDataBase("重复执行")
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public TimeOnly StartTime { get; set; } = new(0, 0, 0);
        /// <summary>
        /// 结束时间
        /// </summary>
        public TimeOnly EndTime { get; set; } = new(23, 59, 59);
        /// <summary>
        /// 间隔
        /// </summary>
        public int Interval { get; set; } = 1;
        /// <summary>
        /// 间隔类型
        /// </summary>
        public TimeTriggerIntervalType IntervalType { get; set; } = TimeTriggerIntervalType.Hour;
        /// <inheritdoc/>
        public override string GetDescriptionText() => $"{StartTime} 至 {EndTime} 之间、每 {Interval} {IntervalType.GetDescription()} 执行一次。";
    }
}
