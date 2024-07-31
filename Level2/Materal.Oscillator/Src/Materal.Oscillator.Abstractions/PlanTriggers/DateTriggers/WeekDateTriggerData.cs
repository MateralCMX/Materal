using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;
using System.Text;

namespace Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 周日期触发器数据
    /// </summary>
    public class WeekDateTriggerData() : DateTriggerDataBase("周")
    {
        /// <summary>
        /// 星期数据
        /// </summary>
        public List<DayOfWeek> Weeks { get; set; } = [];
        /// <inheritdoc/>
        public override string GetDescriptionText(ITimeTriggerData timeTriggerData)
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
            List<string> weekDescription = [];
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
            description.Append(string.Join('、', weekDescription));
            description.Append(' ');
            return description.ToString() + timeTriggerData.GetDescriptionText();
        }
    }
}
