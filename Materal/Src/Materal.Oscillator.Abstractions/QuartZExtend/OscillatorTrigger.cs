using Materal.Oscillator.Abstractions.PlanTriggers;
using Quartz.Impl.Triggers;

namespace Materal.Oscillator.Abstractions.QuartZExtend
{
    /// <summary>
    /// 触发器实现
    /// </summary>
    [Serializable]
    public class OscillatorTrigger : AbstractTrigger, IOscillatorTrigger
    {
        /// <summary>
        /// 触发数据
        /// </summary>
        public IPlanTrigger? PlanTriggerData { get; set; }
        /// <summary>
        /// 重复下去标识
        /// </summary>
        public const int RepeatIndefinitely = -1;
        /// <summary>
        /// 放弃调度时间标识
        /// </summary>
        private const int YearToGiveupSchedulingAt = 2299;
        /// <summary>
        /// 下一次执行时间
        /// </summary>
        private DateTimeOffset? nextFireTimeUtc;
        /// <summary>
        /// 以前执行时间
        /// </summary>
        private DateTimeOffset? previousFireTimeUtc;
        /// <summary>
        /// 重复次数
        /// </summary>
        private int repeatCount;
        /// <summary>
        /// 重复次数,-1为永远重复
        /// </summary>
        public int RepeatCount
        {
            get => repeatCount;
            set
            {
                if (value < 0 && value != RepeatIndefinitely)
                {
                    throw new ArgumentException("重复次数必须 >= 0, 使用-1则表示无限次数.");
                }
                repeatCount = value;
            }
        }
        /// <summary>
        /// 重复间隔
        /// </summary>
        private TimeSpan repeatInterval = TimeSpan.Zero;
        /// <summary>
        /// 重复间隔
        /// </summary>
        public TimeSpan RepeatInterval
        {
            get => repeatInterval;

            set
            {
                if (value < TimeSpan.Zero)
                {
                    throw new ArgumentException("重复间隔必须 >= 0");
                }

                repeatInterval = value;
            }
        }
        /// <summary>
        /// 时间触发
        /// </summary>
        public virtual int TimesTriggered { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public OscillatorTrigger() { }
        /// <summary>
        /// 获得调度器构建
        /// </summary>
        /// <returns></returns>
        public override IScheduleBuilder GetScheduleBuilder()
        {
            OscillatorScheduleBuilder sb = OscillatorScheduleBuilder.Create()
                .WithInterval(RepeatInterval)
                .WithRepeatCount(RepeatCount);
            switch (MisfireInstruction)
            {
                case Quartz.MisfireInstruction.SimpleTrigger.FireNow:
                    sb.WithMisfireHandlingInstructionFireNow();
                    break;
                case Quartz.MisfireInstruction.SimpleTrigger.RescheduleNextWithExistingCount:
                    sb.WithMisfireHandlingInstructionNextWithExistingCount();
                    break;
                case Quartz.MisfireInstruction.SimpleTrigger.RescheduleNextWithRemainingCount:
                    sb.WithMisfireHandlingInstructionNextWithRemainingCount();
                    break;
                case Quartz.MisfireInstruction.SimpleTrigger.RescheduleNowWithExistingRepeatCount:
                    sb.WithMisfireHandlingInstructionNowWithExistingCount();
                    break;
                case Quartz.MisfireInstruction.SimpleTrigger.RescheduleNowWithRemainingRepeatCount:
                    sb.WithMisfireHandlingInstructionNowWithRemainingCount();
                    break;
                case Quartz.MisfireInstruction.IgnoreMisfirePolicy:
                    sb.WithMisfireHandlingInstructionIgnoreMisfires();
                    break;
            }
            return sb;
        }
        /// <summary>
        /// 返回 <see cref="IOscillatorTrigger" /> 最终触发时间, 如果重复次数为无限，则返回null
        /// </summary>
        public override DateTimeOffset? FinalFireTimeUtc
        {
            get
            {
                if (repeatCount == 0) return StartTimeUtc;
                if (repeatCount == RepeatIndefinitely && !EndTimeUtc.HasValue) return null;
                if (repeatCount == RepeatIndefinitely) return GetFireTimeBefore(EndTimeUtc);
                DateTimeOffset lastTrigger = StartTimeUtc.AddTicks(repeatCount * repeatInterval.Ticks);
                if (!EndTimeUtc.HasValue || lastTrigger < EndTimeUtc.Value) return lastTrigger;
                return GetFireTimeBefore(EndTimeUtc);
            }
        }
        /// <summary>
        /// 时间精度是否是毫秒级
        /// </summary>
        /// <value></value>
        public override bool HasMillisecondPrecision => false;
        /// <summary>
        /// 验证误发
        /// </summary>
        /// <param name="misfireInstruction">The misfire instruction.</param>
        /// <returns></returns>
        protected override bool ValidateMisfireInstruction(int misfireInstruction)
        {
            if (misfireInstruction < Quartz.MisfireInstruction.IgnoreMisfirePolicy) return false;
            if (misfireInstruction > Quartz.MisfireInstruction.SimpleTrigger.RescheduleNextWithExistingCount) return false;
            return true;
        }
        /// <summary>
        /// 根据失败指令类型刷新状态
        /// </summary>
        public override void UpdateAfterMisfire(ICalendar? cal)
        {
            int instr = MisfireInstruction;
            if (instr == Quartz.MisfireInstruction.SmartPolicy)
            {
                if (RepeatCount == 0)
                {
                    instr = Quartz.MisfireInstruction.SimpleTrigger.FireNow;
                }
                else if (RepeatCount == RepeatIndefinitely)
                {
                    instr = Quartz.MisfireInstruction.SimpleTrigger.RescheduleNextWithRemainingCount;

                }
                else
                {
                    instr = Quartz.MisfireInstruction.SimpleTrigger.RescheduleNowWithExistingRepeatCount;
                }
            }
            else if (instr == Quartz.MisfireInstruction.SimpleTrigger.FireNow && RepeatCount != 0)
            {
                instr = Quartz.MisfireInstruction.SimpleTrigger.RescheduleNowWithRemainingRepeatCount;
            }
            if (instr == Quartz.MisfireInstruction.SimpleTrigger.FireNow)
            {
                nextFireTimeUtc = SystemTime.UtcNow();
            }
            else if (instr == Quartz.MisfireInstruction.SimpleTrigger.RescheduleNextWithExistingCount)
            {
                DateTimeOffset? newFireTime = GetFireTimeAfter(SystemTime.UtcNow());
                while (newFireTime.HasValue && cal != null && !cal.IsTimeIncluded(newFireTime.Value))
                {
                    newFireTime = GetFireTimeAfter(newFireTime);

                    if (!newFireTime.HasValue)
                    {
                        break;
                    }
                    if (newFireTime.Value.Year > YearToGiveupSchedulingAt)
                    {
                        newFireTime = null;
                    }
                }
                nextFireTimeUtc = newFireTime;
            }
            else if (instr == Quartz.MisfireInstruction.SimpleTrigger.RescheduleNextWithRemainingCount)
            {
                DateTimeOffset? newFireTime = GetFireTimeAfter(SystemTime.UtcNow());
                while (newFireTime.HasValue && cal != null && !cal.IsTimeIncluded(newFireTime.Value))
                {
                    newFireTime = GetFireTimeAfter(newFireTime);

                    if (!newFireTime.HasValue)
                    {
                        break;
                    }
                    if (newFireTime.Value.Year > YearToGiveupSchedulingAt)
                    {
                        newFireTime = null;
                    }
                }
                if (newFireTime.HasValue)
                {
                    int timesMissed = ComputeNumTimesFiredBetween(nextFireTimeUtc!.Value, newFireTime!.Value);
                    TimesTriggered += timesMissed;
                }
                nextFireTimeUtc = newFireTime;
            }
            else if (instr == Quartz.MisfireInstruction.SimpleTrigger.RescheduleNowWithExistingRepeatCount)
            {
                DateTimeOffset newFireTime = SystemTime.UtcNow();
                if (repeatCount != 0 && repeatCount != RepeatIndefinitely)
                {
                    RepeatCount -= TimesTriggered;
                    TimesTriggered = 0;
                }
                if (EndTimeUtc.HasValue && EndTimeUtc.Value < newFireTime)
                {
                    nextFireTimeUtc = null;
                }
                else
                {
                    StartTimeUtc = newFireTime;
                    nextFireTimeUtc = newFireTime;
                }
            }
            else if (instr == Quartz.MisfireInstruction.SimpleTrigger.RescheduleNowWithRemainingRepeatCount)
            {
                DateTimeOffset newFireTime = SystemTime.UtcNow();
                int timesMissed = ComputeNumTimesFiredBetween(nextFireTimeUtc!.Value, newFireTime);
                if (repeatCount != 0 && repeatCount != RepeatIndefinitely)
                {
                    int remainingCount = RepeatCount - (TimesTriggered + timesMissed);
                    if (remainingCount <= 0)
                    {
                        remainingCount = 0;
                    }
                    RepeatCount = remainingCount;
                    TimesTriggered = 0;
                }
                if (EndTimeUtc.HasValue && EndTimeUtc.Value < newFireTime)
                {
                    nextFireTimeUtc = null;
                }
                else
                {
                    StartTimeUtc = newFireTime;
                    nextFireTimeUtc = newFireTime;
                }
            }
        }
        /// <summary>
        /// 触发器触发
        /// </summary>
        /// <seealso cref="JobExecutionException" />
        public override void Triggered(ICalendar? cal)
        {
            TimesTriggered++;
            previousFireTimeUtc = nextFireTimeUtc;
            nextFireTimeUtc = GetFireTimeAfter(nextFireTimeUtc);
            while (nextFireTimeUtc.HasValue && cal != null && !cal.IsTimeIncluded(nextFireTimeUtc.Value))
            {
                nextFireTimeUtc = GetFireTimeAfter(nextFireTimeUtc);
                if (!nextFireTimeUtc.HasValue)
                {
                    break;
                }
                if (nextFireTimeUtc.Value.Year > YearToGiveupSchedulingAt)
                {
                    nextFireTimeUtc = null;
                }
            }
        }
        /// <summary>
        /// 使用新的日历更新实例
        /// </summary>
        /// <param name="calendar">The calendar.</param>
        /// <param name="misfireThreshold">The misfire threshold.</param>
		public override void UpdateWithNewCalendar(ICalendar calendar, TimeSpan misfireThreshold)
        {
            nextFireTimeUtc = GetFireTimeAfter(previousFireTimeUtc);
            if (nextFireTimeUtc == null || calendar == null) return;
            DateTimeOffset now = SystemTime.UtcNow();
            while (nextFireTimeUtc.HasValue && !calendar.IsTimeIncluded(nextFireTimeUtc.Value))
            {
                nextFireTimeUtc = GetFireTimeAfter(nextFireTimeUtc);
                if (!nextFireTimeUtc.HasValue) break;
                if (nextFireTimeUtc.Value.Year > YearToGiveupSchedulingAt)
                {
                    nextFireTimeUtc = null;
                }
                if (nextFireTimeUtc != null && nextFireTimeUtc.Value < now)
                {
                    TimeSpan diff = now - nextFireTimeUtc.Value;
                    if (diff >= misfireThreshold)
                    {
                        nextFireTimeUtc = GetFireTimeAfter(nextFireTimeUtc);
                    }
                }
            }
        }
        /// <summary>
        /// 计算第一次执行时间
        /// </summary>
        /// <returns></returns>
        public override DateTimeOffset? ComputeFirstFireTimeUtc(ICalendar? cal)
        {
            nextFireTimeUtc = StartTimeUtc;
            while (cal != null && !cal.IsTimeIncluded(nextFireTimeUtc.Value))
            {
                nextFireTimeUtc = GetFireTimeAfter(nextFireTimeUtc);
                if (!nextFireTimeUtc.HasValue) break;
                if (nextFireTimeUtc.Value.Year > YearToGiveupSchedulingAt) return null;
            }
            return nextFireTimeUtc;
        }
        /// <summary>
        /// 获得下一次执行时间
        /// </summary>
        public override DateTimeOffset? GetNextFireTimeUtc() => nextFireTimeUtc;
        /// <summary>
        /// 设置下一次执行时间
        /// </summary>
        /// <param name="nextFireTime"></param>
        public override void SetNextFireTimeUtc(DateTimeOffset? nextFireTime) => nextFireTimeUtc = nextFireTime;
        /// <summary>
        /// 设置上一次执行时间
        /// </summary>
        /// <param name="previousFireTime"></param>
        public override void SetPreviousFireTimeUtc(DateTimeOffset? previousFireTime) => previousFireTimeUtc = previousFireTime;
        /// <summary>
        /// 获得上一次执行时间
        /// </summary>
        public override DateTimeOffset? GetPreviousFireTimeUtc() => previousFireTimeUtc;
        /// <summary>
        /// 获得下一个触发时间，为null则不触发
        /// </summary>
        /// <param name="afterTimeUtc"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetFireTimeAfter(DateTimeOffset? afterTimeUtc)
        {
            if (TimesTriggered > repeatCount && repeatCount != RepeatIndefinitely) return null;
            if (!afterTimeUtc.HasValue) afterTimeUtc = SystemTime.UtcNow();
            if (repeatCount == 0 && afterTimeUtc.Value.CompareTo(StartTimeUtc) >= 0) return null;
            DateTimeOffset startMillis = StartTimeUtc;
            DateTimeOffset afterMillis = afterTimeUtc.Value;
            DateTimeOffset endMillis = EndTimeUtc ?? DateTimeOffset.MaxValue;
            if (endMillis <= afterMillis) return null;
            if (afterMillis < startMillis) return startMillis;
            if (PlanTriggerData == null) return null;
            DateTimeOffset? result = PlanTriggerData.GetNextRunTime(afterMillis);
            return result;
        }
        /// <summary>
        /// 获得最后触发的时间。返回null则不触发
        /// </summary>
        public virtual DateTimeOffset? GetFireTimeBefore(DateTimeOffset? endUtc)
        {
            if (endUtc != null && endUtc.Value < StartTimeUtc) return null;
            int numFires = ComputeNumTimesFiredBetween(StartTimeUtc, endUtc!.Value);
            return StartTimeUtc.AddTicks(numFires * repeatInterval.Ticks);
        }
        /// <summary>
        /// 计算触发了几次
        /// </summary>
        /// <param name="startTimeUtc"></param>
        /// <param name="endTimeUtc"></param>
        /// <returns></returns>
        public virtual int ComputeNumTimesFiredBetween(DateTimeOffset startTimeUtc, DateTimeOffset endTimeUtc)
        {
            long time = (endTimeUtc - startTimeUtc).Ticks;
            return (int)(time / repeatInterval.Ticks);
        }
        /// <summary>
        /// 确定触发器能否触发
        /// </summary>
        public override bool GetMayFireAgain()
        {
            return GetNextFireTimeUtc().HasValue;
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <exception cref="SchedulerException"></exception>
        public override void Validate()
        {
            base.Validate();

            if (repeatCount != 0 && repeatInterval.Ticks < 1)
            {
                throw new SchedulerException("重复间隔不能小于1");
            }
        }
    }
}
