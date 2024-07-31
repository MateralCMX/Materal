using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.Oscillator.PlanTriggers.TimeTriggers
{
    /// <summary>
    /// 时间执行一次触发器
    /// </summary>
    public class OneTimeTimeTrigger : TimeTriggerBase<OneTimeTimeTriggerData>, ITimeTrigger
    {
        /// <inheritdoc/>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => null;
        /// <inheritdoc/>
        public override DateTimeOffset? GetTriggerStartTime(DateOnly date) => date.ToDateTimeOffset(Data.StartTime);
        /// <inheritdoc/>
        public override DateTimeOffset? GetTriggerEndTime(DateOnly date) => null;
    }
}
