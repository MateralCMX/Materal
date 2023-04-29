using Materal.Abstractions;
using System.Text;

namespace Materal.Oscillator.PlanTriggers
{
    public class DateDayTrigger : DateTriggerBase, IDateTrigger
    {
        public override string GetDescriptionText(IEveryDayTrigger everyDayTrigger)
        {
            StringBuilder description = GetFrontDescriptionText();
            if (Interval == 1)
            {
                description.Append("每天 的 ");
            }
            else
            {
                description.Append($"每 {Interval} 天 的 ");
            }
            return description.ToString() + everyDayTrigger.GetDescriptionText();
        }
        protected override Date? GetNextRunDate(DateTimeOffset upRunTime)
        {
            DateTimeOffset nextRunDate = upRunTime.AddDays(Interval);
            return nextRunDate.ToDate();
        }
    }
}
