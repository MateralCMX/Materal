using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;
using System.Text;

namespace Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 天日期触发器数据
    /// </summary>
    public class DayDateTriggerData() : DateTriggerDataBase("天")
    {
        /// <inheritdoc/>
        public override string GetDescriptionText(ITimeTriggerData timeTriggerData)
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
            return description.ToString() + timeTriggerData.GetDescriptionText();
        }
    }
}
