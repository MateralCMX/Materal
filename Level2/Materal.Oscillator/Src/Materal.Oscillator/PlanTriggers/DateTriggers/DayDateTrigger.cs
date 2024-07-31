using Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers;

namespace Materal.Oscillator.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 每天触发器
    /// </summary>
    public class DayDateTrigger : DateTriggerBase<DayDateTriggerData>, IDateTrigger
    {
        /// <inheritdoc/>
        protected override DateOnly? GetNextRunDate(DateTimeOffset upRunTime)
        {
            DateTimeOffset nextRunDate = upRunTime.AddDays(Data.Interval);
            return nextRunDate.ToDateOnly();
        }
    }
}
