﻿using Materal.Oscillator.PlanTriggers.TimeTrigger;

namespace Materal.Oscillator.PlanTriggers.DateTrigger
{
    /// <summary>
    /// 日期触发器
    /// </summary>
    public abstract class DateTriggerBase : DefaultOscillatorData, IDateTrigger
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public Date StartDate { get; set; } = new(DateTime.Now);
        /// <summary>
        /// 结束日期
        /// </summary>
        public Date? EndDate { get; set; }
        /// <summary>
        /// 间隔
        /// </summary>
        public uint Interval { get; set; } = 1;
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public virtual DateTimeOffset? GetDateEndTime(ITimeTrigger everyDayTrigger)
        {
            if (Interval <= 0) return null;
            if (EndDate == null) return null;
            DateTimeOffset? result = everyDayTrigger.GetTriggerEndTime(EndDate.Value);
            return result;
        }
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public virtual DateTimeOffset? GetDateStartTime(ITimeTrigger everyDayTrigger)
        {
            if (Interval <= 0) return null;
            DateTimeOffset? result = everyDayTrigger.GetTriggerStartTime(StartDate);
            return result;
        }
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
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public abstract string GetDescriptionText(ITimeTrigger everyDayTrigger);
        /// <summary>
        /// 获得下次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        public virtual DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, ITimeTrigger everyDayTrigger)
        {
            if (Interval <= 0) return null;
            DateTimeOffset? result = everyDayTrigger.GetNextRunTime(upRunTime);
            if (result != null) return result;
            Date? nextRunDate = GetNextRunDate(upRunTime);
            if (nextRunDate == null) return null;
            if (EndDate != null && nextRunDate > EndDate) return null;
            result = everyDayTrigger.GetTriggerStartTime(nextRunDate.Value);
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