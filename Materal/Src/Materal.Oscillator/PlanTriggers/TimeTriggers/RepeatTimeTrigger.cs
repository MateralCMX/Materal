using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.Oscillator.PlanTriggers.TimeTriggers
{
    /// <summary>
    /// 时间重复执行触发器
    /// </summary>
    public class RepeatTimeTrigger : TimeTriggerBase<RepeatTimeTriggerData>, ITimeTrigger
    {
        /// <inheritdoc/>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime)
        {
            DateTimeOffset? result = null;
            DateTime nowDateTime = DateTime.Now;
            DateTimeOffset nowTime = nowDateTime.ToDateTimeOffset();
            DateOnly nowDate = nowTime.ToDateOnly();
            DateTimeOffset startTime = Data.StartTime.ToDateTimeOffset(nowDate);
            if (nowTime < startTime)
            {
                result = startTime;
            }
            else
            {
                DateTimeOffset endTime = Data.EndTime.ToDateTimeOffset(nowDate);
                int count = GetEstimatedNumber(startTime, endTime);
                int nowCount = GetEstimatedNumber(startTime, upRunTime);
                if (nowCount < count)
                {
                    switch (Data.IntervalType)
                    {
                        case TimeTriggerIntervalType.Hour:
                            result = startTime.Add(TimeSpan.FromHours(Data.Interval * (nowCount + 1)));
                            break;
                        case TimeTriggerIntervalType.Minute:
                            result = startTime.Add(TimeSpan.FromMinutes(Data.Interval * (nowCount + 1)));
                            break;
                        case TimeTriggerIntervalType.Second:
                            result = startTime.Add(TimeSpan.FromSeconds(Data.Interval * (nowCount + 1)));
                            break;
                    }
                }
            }
            return result;
        }
        /// <inheritdoc/>
        public override DateTimeOffset? GetTriggerStartTime(DateOnly date) => date.ToDateTimeOffset(Data.StartTime);
        /// <inheritdoc/>
        public override DateTimeOffset? GetTriggerEndTime(DateOnly date) => date.ToDateTimeOffset(Data.EndTime);
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
            switch (Data.IntervalType)
            {
                case TimeTriggerIntervalType.Hour:
                    result = (int)(time / TimeSpan.FromHours(Data.Interval).Ticks);
                    break;
                case TimeTriggerIntervalType.Minute:
                    result = (int)(time / TimeSpan.FromMinutes(Data.Interval).Ticks);
                    break;
                case TimeTriggerIntervalType.Second:
                    result = (int)(time / TimeSpan.FromSeconds(Data.Interval).Ticks);
                    break;
            }
            return result;
        }
        #endregion
    }
}
