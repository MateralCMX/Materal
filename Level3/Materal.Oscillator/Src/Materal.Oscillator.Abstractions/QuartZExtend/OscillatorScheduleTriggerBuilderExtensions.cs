using Quartz;

namespace Materal.Oscillator.Abstractions.QuartZExtend
{
    /// <summary>
    /// 调度器触发器构建扩展
    /// </summary>
    public static class OscillatorScheduleTriggerBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="triggerBuilder"></param>
        /// <returns></returns>
        public static TriggerBuilder WithOscillatorSchedule(this TriggerBuilder triggerBuilder)
        {
            OscillatorScheduleBuilder builder = OscillatorScheduleBuilder.Create();
            return triggerBuilder.WithSchedule(builder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="triggerBuilder"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TriggerBuilder WithOscillatorSchedule(this TriggerBuilder triggerBuilder, Action<OscillatorScheduleBuilder> action)
        {
            OscillatorScheduleBuilder builder = OscillatorScheduleBuilder.Create();
            action(builder);
            return triggerBuilder.WithSchedule(builder);
        }
    }
}
