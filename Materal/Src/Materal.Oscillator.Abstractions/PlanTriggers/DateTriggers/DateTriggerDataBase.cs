using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;
using System.Text;

namespace Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 时间触发器数据基类
    /// </summary>
    /// <param name="name"></param>
    public abstract class DateTriggerDataBase(string name) : DefaultData, IDateTriggerData
    {
        /// <inheritdoc/>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <inheritdoc/>
        public string Name { get; set; } = name;
        /// <inheritdoc/>
        public DateOnly StartDate { get; set; } = DateTime.Now.ToDateOnly();
        /// <inheritdoc/>
        public DateOnly? EndDate { get; set; }
        /// <inheritdoc/>
        public uint Interval { get; set; } = 1;
        /// <inheritdoc/>
        public abstract string GetDescriptionText(ITimeTriggerData timeTriggerData);
        /// <summary>
        /// 获得前半段说明文本
        /// </summary>
        /// <returns></returns>
        protected virtual StringBuilder GetFrontDescriptionText()
        {
            StringBuilder description = new();
            description.Append($"将从 {StartDate} ");
            if (EndDate != null)
            {
                description.Append($"到 {EndDate} 之间 ");
            }
            else
            {
                description.Append("开始 ");
            }
            return description;
        }
    }
}
