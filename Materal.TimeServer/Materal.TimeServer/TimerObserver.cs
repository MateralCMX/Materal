using Materal.DateTimeHelper;
using System;

namespace Materal.TimeServer
{
    public abstract class TimerObserver : ITimerObserver
    {
        public TimerObserverCategory Category { get; }
        public DateTime NextExecuteTime { get; private set; }
        private readonly DateTimeTypeEnum _dateTimeType;
        private readonly int _timeValue;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="timeValue"></param>
        /// <param name="dateTimeType"></param>
        protected TimerObserver(int timeValue, DateTimeTypeEnum dateTimeType)
        {
            Category = TimerObserverCategory.Interval;
            _dateTimeType = dateTimeType;
            _timeValue = timeValue;
            UpdateNextExecuteTime();
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateTimeType"></param>
        protected TimerObserver(DateTime dateTime, DateTimeTypeEnum dateTimeType)
        {
            Category = TimerObserverCategory.Timing;
            _dateTimeType = dateTimeType;
            NextExecuteTime = dateTime;
        }
        /// <summary>
        /// 更新下次执行时间
        /// </summary>
        public void UpdateNextExecuteTime()
        {
            switch (Category)
            {
                case TimerObserverCategory.Timing:
                    switch (_dateTimeType)
                    {
                        case DateTimeTypeEnum.Year:
                            NextExecuteTime = NextExecuteTime.AddYears(1);
                            break;
                        case DateTimeTypeEnum.Month:
                            NextExecuteTime = NextExecuteTime.AddMonths(1);
                            break;
                        case DateTimeTypeEnum.Day:
                            NextExecuteTime = NextExecuteTime.AddDays(1);
                            break;
                        case DateTimeTypeEnum.Hour:
                            NextExecuteTime = NextExecuteTime.AddHours(1);
                            break;
                        case DateTimeTypeEnum.Minute:
                            NextExecuteTime = NextExecuteTime.AddMinutes(1);
                            break;
                        case DateTimeTypeEnum.Second:
                            NextExecuteTime = NextExecuteTime.AddSeconds(1);
                            break;
                        case DateTimeTypeEnum.Millisecond:
                            NextExecuteTime = NextExecuteTime.AddMilliseconds(1);
                            break;
                    }
                    break;
                case TimerObserverCategory.Interval:
                    NextExecuteTime = DateTime.Now.AddMilliseconds(DateTimeManager.ToMilliseconds(_timeValue, _dateTimeType));
                    break;
            }
        }
    }
}
