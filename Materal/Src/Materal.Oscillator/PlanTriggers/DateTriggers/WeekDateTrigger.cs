using Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.Oscillator.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 周触发器
    /// </summary>
    public class WeekDateTrigger : DateTriggerBase<WeekDateTriggerData>, IDateTrigger
    {
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetDateEndTime(ITimeTrigger everyDayTrigger)
        {
            if (Data.Weeks is null || Data.Weeks.Count <= 0) return null;
            return base.GetDateEndTime(everyDayTrigger);
        }
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetDateStartTime(ITimeTrigger everyDayTrigger)
        {
            if (Data.Weeks is null || Data.Weeks.Count <= 0) return null;
            return base.GetDateStartTime(everyDayTrigger);
        }
        /// <summary>
        /// 获得下一次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, ITimeTrigger everyDayTrigger)
        {
            if (Data.Weeks is null || Data.Weeks.Count <= 0) return null;
            return base.GetNextRunTime(upRunTime, everyDayTrigger);
        }
        /// <summary>
        /// 获得下次运行日期
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        protected override DateOnly? GetNextRunDate(DateTimeOffset upRunTime)
        {
            var nextDate = upRunTime.AddDays(1);
            if (nextDate.DayOfWeek != DayOfWeek.Sunday)
            {
                while (true)
                {
                    if (Data.Weeks.Contains(nextDate.DayOfWeek)) return nextDate.ToDateOnly();
                    if (nextDate.DayOfWeek == DayOfWeek.Sunday) break;
                    nextDate = nextDate.AddDays(1);
                }
            }
            nextDate = nextDate.AddDays((Data.Interval - 1) * 7 + 1);
            while (!Data.Weeks.Contains(nextDate.DayOfWeek))
            {
                nextDate = nextDate.AddDays(1);
            }
            DateOnly result = nextDate.ToDateOnly();
            return result;
        }
    }
}
