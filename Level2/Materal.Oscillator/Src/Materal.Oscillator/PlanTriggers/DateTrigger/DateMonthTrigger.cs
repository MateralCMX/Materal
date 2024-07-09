using Materal.Oscillator.PlanTriggers.TimeTrigger;

namespace Materal.Oscillator.PlanTriggers.DateTrigger
{
    /// <summary>
    /// 月度触发器
    /// </summary>
    public class DateMonthTrigger : DateTriggerBase, IDateTrigger
    {
        /// <summary>
        /// 索引
        /// </summary>
        public uint Index { get; set; } = 1;
        /// <summary>
        /// 索引类型
        /// </summary>
        public MonthFrequencyIndexType IndexType { get; set; } = MonthFrequencyIndexType.Day;
        /// <summary>
        /// 正序
        /// </summary>
        public bool IsAscending { get; set; } = true;
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override string GetDescriptionText(ITimeTrigger everyDayTrigger)
        {
            StringBuilder description = GetFrontDescriptionText();
            if (Interval == 1)
            {
                description.Append("每月 的 ");
            }
            else
            {
                description.Append($"每 {Interval} 月 的 ");
            }
            description.Append(IsAscending ? "第" : "倒数第");
            switch (IndexType)
            {
                case MonthFrequencyIndexType.Day:
                    description.Append($" {Index} {IndexType.GetDescription()} ");
                    break;
                default:
                    description.Append($" {Index} 个 {IndexType.GetDescription()} ");
                    break;
            }
            return description.ToString() + everyDayTrigger.GetDescriptionText();
        }
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetDateEndTime(ITimeTrigger everyDayTrigger)
        {
            if (Index <= 0) return null;
            return base.GetDateEndTime(everyDayTrigger);
        }
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetDateStartTime(ITimeTrigger everyDayTrigger)
        {
            if (Index <= 0) return null;
            return base.GetDateStartTime(everyDayTrigger);
        }
        /// <summary>
        /// 获得下次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, ITimeTrigger everyDayTrigger)
        {
            if (Index <= 0) return null;
            return base.GetNextRunTime(upRunTime, everyDayTrigger);
        }
        /// <summary>
        /// 获得下次运行日期
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected override DateOnly? GetNextRunDate(DateTimeOffset upRunTime)
        {
            DayOfWeek ToDayOfWeek()
            {
                return IndexType switch
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
            if (IndexType == MonthFrequencyIndexType.Day)
            {
                if (IsAscending)
                {
                    return new DateOnly(upRunTime.Year, upRunTime.Month, Convert.ToInt32(Index)).AddMonths(Convert.ToInt32(Interval));
                }
                else
                {
                    return new DateOnly(upRunTime.Year, upRunTime.Month, 1).AddMonths(Convert.ToInt32(Interval) + 1).AddDays(-1 * Convert.ToInt32(Index));
                }
            }
            DateTime nextRunDate;
            if (IsAscending)
            {
                nextRunDate = new DateTime(upRunTime.Year, upRunTime.Month, 1).AddMonths(Convert.ToInt32(Interval));
                while (nextRunDate.DayOfWeek != ToDayOfWeek())
                {
                    nextRunDate = nextRunDate.AddDays(1);
                }
                nextRunDate = nextRunDate.AddDays(Convert.ToInt32(Index - 1) * 7);
                return nextRunDate.ToDateOnly();
            }
            else
            {
                nextRunDate = new DateTime(upRunTime.Year, upRunTime.Month + 1, 1).AddMonths(Convert.ToInt32(Interval)).AddDays(-1);
                while (nextRunDate.DayOfWeek != ToDayOfWeek())
                {
                    nextRunDate = nextRunDate.AddDays(-1);
                }
                nextRunDate = nextRunDate.AddDays(Convert.ToInt32(Index - 1) * -7);
                return nextRunDate.ToDateOnly();
            }
        }
    }
}
