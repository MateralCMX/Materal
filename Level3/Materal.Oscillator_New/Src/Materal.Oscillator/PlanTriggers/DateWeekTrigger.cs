using Materal.Abstractions;
using System.Text;

namespace Materal.Oscillator.PlanTriggers
{
    public class DateWeekTrigger : DateTriggerBase, IDateTrigger
    {
        /// <summary>
        /// 星期数据
        /// </summary>
        public List<DayOfWeek> Weeks { get; set; } = new();
        public override string GetDescriptionText(IEveryDayTrigger everyDayTrigger)
        {
            StringBuilder description = GetFrontDescriptionText();
            if (Interval == 1)
            {
                description.Append("每周 的 ");
            }
            else
            {
                description.Append($"每 {Interval} 周 的 ");
            }
            Weeks.Sort();
            List<string> weekDescription = new();
            foreach (var week in Weeks)
            {
                switch (week)
                {
                    case DayOfWeek.Sunday:
                        weekDescription.Add("周日");
                        break;
                    case DayOfWeek.Monday:
                        weekDescription.Add("周一");
                        break;
                    case DayOfWeek.Tuesday:
                        weekDescription.Add("周二");
                        break;
                    case DayOfWeek.Wednesday:
                        weekDescription.Add("周三");
                        break;
                    case DayOfWeek.Thursday:
                        weekDescription.Add("周四");
                        break;
                    case DayOfWeek.Friday:
                        weekDescription.Add("周五");
                        break;
                    case DayOfWeek.Saturday:
                        weekDescription.Add("周六");
                        break;
                }
            }
            description.Append(string.Join("、", weekDescription));
            description.Append(' ');
            return description.ToString() + everyDayTrigger.GetDescriptionText();
        }
        public override DateTimeOffset? GetDateEndTime(IEveryDayTrigger everyDayTrigger)
        {
            if (Weeks == null || Weeks.Count <= 0) return null;
            return base.GetDateEndTime(everyDayTrigger);
        }
        public override DateTimeOffset? GetDateStartTime(IEveryDayTrigger everyDayTrigger)
        {
            if (Weeks == null || Weeks.Count <= 0) return null;
            return base.GetDateStartTime(everyDayTrigger);
        }
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, IEveryDayTrigger everyDayTrigger)
        {
            if (Weeks == null || Weeks.Count <= 0) return null;
            return base.GetNextRunTime(upRunTime, everyDayTrigger);
        }
        protected override Date? GetNextRunDate(DateTimeOffset upRunTime)
        {
            var nextDate = upRunTime.AddDays(1);
            if (nextDate.DayOfWeek != DayOfWeek.Sunday)
            {
                while (true)
                {
                    if (Weeks.Contains(nextDate.DayOfWeek)) return nextDate.ToDate();
                    if (nextDate.DayOfWeek == DayOfWeek.Sunday) break;
                    nextDate = nextDate.AddDays(1);
                }
            }
            nextDate = nextDate.AddDays((Interval - 1) * 7 + 1);
            while (!Weeks.Contains(nextDate.DayOfWeek))
            {
                nextDate = nextDate.AddDays(1);
            }
            Date result = nextDate.ToDate();
            return result;
        }
    }
}
