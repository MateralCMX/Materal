using Materal.Oscillator.Abstractions.PlanTriggers;
using Quartz.Spi;

namespace Materal.Oscillator.Abstractions.QuartZExtend
{
    /// <summary>
    /// Oscillator调度器构建
    /// </summary>
    public class OscillatorScheduleBuilder : ScheduleBuilder<IOscillatorTrigger>
    {
        /// <summary>
        /// 间隔时间
        /// </summary>
        private TimeSpan interval = TimeSpan.Zero;
        /// <summary>
        /// 重复次数
        /// </summary>
        private int repeatCount = -1;
        /// <summary>
        /// 触发数据
        /// </summary>
        private IPlanTrigger? _planTriggerData;
        /// <summary>
        /// 策略
        /// </summary>
        private int misfireInstruction = MisfireInstruction.SmartPolicy;
        /// <summary>
        /// 构造方法
        /// </summary>
        protected OscillatorScheduleBuilder() { }
        /// <summary>
        /// 创建OscillatorScheduleBuilder.
        /// </summary>
        /// <returns>the new OscillatorScheduleBuilder</returns>
        public static OscillatorScheduleBuilder Create() => new();
        /// <summary>
        /// 创建触发器
        /// </summary>
        public override IMutableTrigger Build() => new OscillatorTrigger()
        {
            PlanTriggerData = _planTriggerData,
            RepeatInterval = interval,
            RepeatCount = repeatCount,
            MisfireInstruction = misfireInstruction
        };
        /// <summary>
        /// 设置触发器数据
        /// </summary>
        /// <param name="planTriggerData"></param>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithTriggerData(IPlanTrigger planTriggerData)
        {
            _planTriggerData = planTriggerData;
            WithInterval(TimeSpan.FromSeconds(1));
            return this;
        }
        /// <summary>
        /// 指定以毫秒为单位的重复间隔
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithInterval(TimeSpan timeSpan)
        {
            interval = timeSpan;
            return this;
        }
        /// <summary>
        /// 指定以秒为单位的重复间隔
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithIntervalInSeconds(int seconds)
            => WithInterval(TimeSpan.FromSeconds(seconds));
        /// <summary>
        /// 指定以分钟为单位的重复间隔
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithIntervalInMinutes(int minutes)
            => WithInterval(TimeSpan.FromMinutes(minutes));
        /// <summary>
        /// 指定以小时为单位的重复间隔
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithIntervalInHours(int hours)
            => WithInterval(TimeSpan.FromHours(hours));
        /// <summary>
        /// 指定重复次数
        /// </summary>
        /// <param name="repeatCount"></param>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithRepeatCount(int repeatCount)
        {
            this.repeatCount = repeatCount;
            return this;
        }
        /// <summary>
        /// 指定永久重复
        /// </summary>
        /// <returns></returns>
        public OscillatorScheduleBuilder RepeatForever()
        {
            repeatCount = OscillatorTrigger.RepeatIndefinitely;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionIgnoreMisfires()
        {
            misfireInstruction = MisfireInstruction.IgnoreMisfirePolicy;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionFireNow()
        {
            misfireInstruction = MisfireInstruction.SimpleTrigger.FireNow;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionNextWithExistingCount()
        {
            misfireInstruction = MisfireInstruction.SimpleTrigger.RescheduleNextWithExistingCount;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionNextWithRemainingCount()
        {
            misfireInstruction = MisfireInstruction.SimpleTrigger.RescheduleNextWithRemainingCount;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionNowWithExistingCount()
        {
            misfireInstruction = MisfireInstruction.SimpleTrigger.RescheduleNowWithExistingRepeatCount;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionNowWithRemainingCount()
        {
            misfireInstruction = MisfireInstruction.SimpleTrigger.RescheduleNowWithRemainingRepeatCount;
            return this;
        }
    }
}
