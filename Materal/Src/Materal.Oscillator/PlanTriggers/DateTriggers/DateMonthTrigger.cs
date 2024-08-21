using Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.Oscillator.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 月度触发器
    /// </summary>
    public class MonthDateTrigger : DateTriggerBase<MonthDateTriggerData>, IDateTrigger
    {
        /// <inheritdoc/>
        public override DateTimeOffset? GetDateEndTime(ITimeTrigger everyDayTrigger)
        {
            if (Data.Index <= 0) return null;
            return base.GetDateEndTime(everyDayTrigger);
        }
        /// <inheritdoc/>
        public override DateTimeOffset? GetDateStartTime(ITimeTrigger everyDayTrigger)
        {
            if (Data.Index <= 0) return null;
            return base.GetDateStartTime(everyDayTrigger);
        }
        /// <inheritdoc/>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, ITimeTrigger everyDayTrigger)
        {
            if (Data.Index <= 0) return null;
            return base.GetNextRunTime(upRunTime, everyDayTrigger);
        }
        /// <inheritdoc/>
        protected override DateOnly? GetNextRunDate(DateTimeOffset upRunTime)
        {
            DayOfWeek ToDayOfWeek()
            {
                return Data.IndexType switch
                {
                    MonthFrequencyIndexType.Sunday => DayOfWeek.Sunday,
                    MonthFrequencyIndexType.Monday => DayOfWeek.Monday,
                    MonthFrequencyIndexType.Tuesday => DayOfWeek.Tuesday,
                    MonthFrequencyIndexType.Wednesday => DayOfWeek.Wednesday,
                    MonthFrequencyIndexType.Thursday => DayOfWeek.Thursday,
                    MonthFrequencyIndexType.Friday => DayOfWeek.Friday,
                    MonthFrequencyIndexType.Saturday => DayOfWeek.Saturday,
                    _ => throw new ArgumentException("未识别位序类型"),
                };
            }
            if (Data.IndexType == MonthFrequencyIndexType.Day)
            {
                if (Data.IsAscending)
                {
                    return new DateOnly(upRunTime.Year, upRunTime.Month, Convert.ToInt32(Data.Index)).AddMonths(Convert.ToInt32(Data.Interval));
                }
                else
                {
                    return new DateOnly(upRunTime.Year, upRunTime.Month, 1).AddMonths(Convert.ToInt32(Data.Interval) + 1).AddDays(-1 * Convert.ToInt32(Data.Index));
                }
            }
            DateTime nextRunDate;
            if (Data.IsAscending)
            {
                nextRunDate = new DateTime(upRunTime.Year, upRunTime.Month, 1).AddMonths(Convert.ToInt32(Data.Interval));
                while (nextRunDate.DayOfWeek != ToDayOfWeek())
                {
                    nextRunDate = nextRunDate.AddDays(1);
                }
                nextRunDate = nextRunDate.AddDays(Convert.ToInt32(Data.Index - 1) * 7);
                return nextRunDate.ToDateOnly();
            }
            else
            {
                nextRunDate = new DateTime(upRunTime.Year, upRunTime.Month + 1, 1).AddMonths(Convert.ToInt32(Data.Interval)).AddDays(-1);
                while (nextRunDate.DayOfWeek != ToDayOfWeek())
                {
                    nextRunDate = nextRunDate.AddDays(-1);
                }
                nextRunDate = nextRunDate.AddDays(Convert.ToInt32(Data.Index - 1) * -7);
                return nextRunDate.ToDateOnly();
            }
        }
    }
}
