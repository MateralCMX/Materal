using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.QuartZExtension;
using Quartz;
using Quartz.Spi;

namespace Materal.Oscillator.Abstractions.QuartZExtend
{
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
        protected OscillatorScheduleBuilder()
        {
        }
        /// <summary>
        /// 创建OscillatorScheduleBuilder.
        /// </summary>
        /// <returns>the new OscillatorScheduleBuilder</returns>
        public static OscillatorScheduleBuilder Create()
        {
            return new OscillatorScheduleBuilder();
        }
        /// <summary>
        /// 创建触发器
        /// </summary>
        public override IMutableTrigger Build()
        {
            OscillatorTriggerImpl st = new()
            {
                PlanTriggerData = _planTriggerData,
                RepeatInterval = interval,
                RepeatCount = repeatCount,
                MisfireInstruction = misfireInstruction
            };
            return st;
        }
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
        /// <param name="minutes"></param>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithInterval(TimeSpan timeSpan)
        {
            interval = timeSpan;
            return this;
        }
        /// <summary>
        /// 指定以秒为单位的重复间隔
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithIntervalInSeconds(int seconds)
        {
            return WithInterval(TimeSpan.FromSeconds(seconds));
        }
        /// <summary>
        /// 指定以分钟为单位的重复间隔
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithIntervalInMinutes(int minutes)
        {
            return WithInterval(TimeSpan.FromMinutes(minutes));
        }
        /// <summary>
        /// 指定以小时为单位的重复间隔
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public OscillatorScheduleBuilder WithIntervalInHours(int hours)
        {
            return WithInterval(TimeSpan.FromHours(hours));
        }
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
            repeatCount = OscillatorTriggerImpl.RepeatIndefinitely;
            return this;
        }
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionIgnoreMisfires()
        {
            misfireInstruction = MisfireInstruction.IgnoreMisfirePolicy;
            return this;
        }
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionFireNow()
        {
            misfireInstruction = MisfireInstruction.SimpleTrigger.FireNow;
            return this;
        }
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionNextWithExistingCount()
        {
            misfireInstruction = MisfireInstruction.SimpleTrigger.RescheduleNextWithExistingCount;
            return this;
        }
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionNextWithRemainingCount()
        {
            misfireInstruction = MisfireInstruction.SimpleTrigger.RescheduleNextWithRemainingCount;
            return this;
        }
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionNowWithExistingCount()
        {
            misfireInstruction = MisfireInstruction.SimpleTrigger.RescheduleNowWithExistingRepeatCount;
            return this;
        }
        public OscillatorScheduleBuilder WithMisfireHandlingInstructionNowWithRemainingCount()
        {
            misfireInstruction = MisfireInstruction.SimpleTrigger.RescheduleNowWithRemainingRepeatCount;
            return this;
        }
    }
}
