using Materal.DateTimeHelper;
using Materal.Common;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 每日重复执行触发器
    /// </summary>
    public class EveryDayRepeatTrigger : EveryDayTriggerBase, IEveryDayTrigger
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public Time StartTime { get; set; } = new Time(0, 0, 0);
        /// <summary>
        /// 结束时间
        /// </summary>
        public Time EndTime { get; set; } = new Time(23, 59, 59);
        /// <summary>
        /// 间隔
        /// </summary>
        public int Interval { get; set; } = 1;
        /// <summary>
        /// 间隔类型
        /// </summary>
        public EveryDayIntervalType IntervalType { get; set; } = EveryDayIntervalType.Hour;

        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime)
        {
            DateTimeOffset? result = null;
            DateTime nowDateTime = DateTime.Now;
            DateTimeOffset nowTime = nowDateTime.ToDateTimeOffset();
            Date nowDate = nowTime.ToDate();
            DateTimeOffset startTime = StartTime.MergeDateTimeOffset(nowDate);
            if (nowTime < startTime)
            {
                result = startTime;
            }
            else
            {
                DateTimeOffset endTime = EndTime.MergeDateTimeOffset(nowDate);
                int count = GetEstimatedNumber(startTime, endTime);
                int nowCount = GetEstimatedNumber(startTime, upRunTime);
                if (nowCount < count)
                {
                    switch (IntervalType)
                    {
                        case EveryDayIntervalType.Hour:
                            result = upRunTime.Add(TimeSpan.FromHours(Interval));
                            break;
                        case EveryDayIntervalType.Minute:
                            result = upRunTime.Add(TimeSpan.FromMinutes(Interval));
                            break;
                        case EveryDayIntervalType.Second:
                            result = upRunTime.Add(TimeSpan.FromSeconds(Interval));
                            break;
                    }
                }
            }
            return result;
        }

        public override string GetDescriptionText() => $"{StartTime} 至 {EndTime} 之间、每 {Interval} {IntervalType.GetDescription()} 执行一次。";

        public override DateTimeOffset? GetTriggerStartTime(Date date) => date.MergeDateTimeOffset(StartTime);

        public override DateTimeOffset? GetTriggerEndTime(Date date) => date.MergeDateTimeOffset(EndTime);

        #region 私有方法
        /// <summary>
        /// 获得预计执行次数
        /// </summary>
        /// <param name="startTimeUtc"></param>
        /// <param name="endTimeUtc"></param>
        /// <returns></returns>
        private int GetEstimatedNumber(DateTimeOffset startTimeUtc, DateTimeOffset endTimeUtc)
        {
            int result = 0;
            long time = (endTimeUtc - startTimeUtc).Ticks;
            switch (IntervalType)
            {
                case EveryDayIntervalType.Hour:
                    result = (int)(time / TimeSpan.FromHours(Interval).Ticks);
                    break;
                case EveryDayIntervalType.Minute:
                    result = (int)(time / TimeSpan.FromMinutes(Interval).Ticks);
                    break;
                case EveryDayIntervalType.Second:
                    result = (int)(time / TimeSpan.FromSeconds(Interval).Ticks);
                    break;
            }
            return result;
        }
        #endregion
    }
}
