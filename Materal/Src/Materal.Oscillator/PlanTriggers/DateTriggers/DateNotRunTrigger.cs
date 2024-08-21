using Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.Oscillator.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 不运行触发器
    /// </summary>
    public class NoneDateTrigger : DateTriggerBase<NoneDateTriggerData>, IDateTrigger
    {
        /// <inheritdoc/>
        public override DateTimeOffset? GetDateEndTime(ITimeTrigger everyDayTrigger) => null;
        /// <inheritdoc/>
        public override DateTimeOffset? GetDateStartTime(ITimeTrigger everyDayTrigger) => null;
        /// <inheritdoc/>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, ITimeTrigger everyDayTrigger) => null;
        /// <inheritdoc/>
        protected override DateOnly? GetNextRunDate(DateTimeOffset upRunTime) => null;
    }
}
