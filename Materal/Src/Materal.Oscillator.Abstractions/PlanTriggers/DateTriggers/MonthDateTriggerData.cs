using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;
using System.Text;

namespace Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 月日期触发器数据
    /// </summary>
    public class MonthDateTriggerData() : DateTriggerDataBase("月")
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
        /// <inheritdoc/>
        public override string GetDescriptionText(ITimeTriggerData timeTriggerData)
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
            return description.ToString() + timeTriggerData.GetDescriptionText();
        }
    }
}
