using Materal.Abstractions;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 每日执行一次触发器
    /// </summary>
    public class EveryDayOneTimeTrigger : EveryDayTriggerBase, IEveryDayTrigger
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public Time StartTime { get; set; } = new Time(0, 0, 0);

        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => null;
        public override string GetDescriptionText() => $"{StartTime} 执行一次。";

        public override DateTimeOffset? GetTriggerStartTime(Date date) => date.MergeDateTimeOffset(StartTime);

        public override DateTimeOffset? GetTriggerEndTime(Date date) => null;
    }
}
