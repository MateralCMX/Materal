using Materal.Oscillator.PlanTriggers.TimeTrigger;

namespace Materal.Oscillator.PlanTriggers.DateTrigger
{
    /// <summary>
    /// 每天触发器
    /// </summary>
    public class DateDayTrigger : DateTriggerBase, IDateTrigger
    {
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
                description.Append("每天 的 ");
            }
            else
            {
                description.Append($"每 {Interval} 天 的 ");
            }
            return description.ToString() + everyDayTrigger.GetDescriptionText();
        }
        /// <summary>
        /// 获得下次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        protected override DateOnly? GetNextRunDate(DateTimeOffset upRunTime)
        {
            DateTimeOffset nextRunDate = upRunTime.AddDays(Interval);
            return nextRunDate.ToDateOnly();
        }
    }
}
