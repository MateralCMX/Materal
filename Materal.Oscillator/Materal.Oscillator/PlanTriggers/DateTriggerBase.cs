using Materal.ConvertHelper;
using Materal.DateTimeHelper;
using System.Text;

namespace Materal.Oscillator.PlanTriggers
{
    public abstract class DateTriggerBase : IDateTrigger
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public Date StartDate { get; set; } = new Date(DateTime.Now);
        /// <summary>
        /// 结束日期
        /// </summary>
        public Date? EndDate { get; set; }
        /// <summary>
        /// 间隔
        /// </summary>
        public uint Interval { get; set; } = 1;

        public virtual DateTimeOffset? GetDateEndTime(IEveryDayTrigger everyDayTrigger)
        {
            if (Interval <= 0) return null;
            if (EndDate == null) return null;
            DateTimeOffset? result = everyDayTrigger.GetTriggerEndTime(EndDate);
            return result;
        }
        public virtual DateTimeOffset? GetDateStartTime(IEveryDayTrigger everyDayTrigger)
        {
            if (Interval <= 0) return null;
            DateTimeOffset? result = everyDayTrigger.GetTriggerStartTime(StartDate);
            return result;
        }
        public virtual IDateTrigger Deserialization(string dateTriggerData) => (IDateTrigger)dateTriggerData.JsonToObject(GetType());
        /// <summary>
        /// 获得前半段说明文本
        /// </summary>
        /// <returns></returns>
        protected StringBuilder GetFrontDescriptionText()
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
        public abstract string GetDescriptionText(IEveryDayTrigger everyDayTrigger);
        public virtual DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, IEveryDayTrigger everyDayTrigger)
        {
            if (Interval <= 0) return null;
            DateTimeOffset? result = everyDayTrigger.GetNextRunTime(upRunTime);
            if (result != null) return result;
            Date? nextRunDate = GetNextRunDate(upRunTime);
            if (nextRunDate == null) return null;
            if (EndDate != null && nextRunDate > EndDate) return null;
            result = everyDayTrigger.GetTriggerStartTime(nextRunDate);
            return result;
        }
        /// <summary>
        /// 获得下次运行的日期
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        protected abstract Date? GetNextRunDate(DateTimeOffset upRunTime);
    }
}
