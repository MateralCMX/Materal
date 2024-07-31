namespace Materal.Oscillator.Abstractions.QuartZExtend
{
    /// <summary>
    /// 调度器触发器构建扩展
    /// </summary>
    public static class TriggerBuilderExtensions
    {
        /// <summary>
        /// 带入Oscillator调度器
        /// </summary>
        /// <param name="triggerBuilder"></param>
        /// <returns></returns>
        public static TriggerBuilder WithOscillatorSchedule(this TriggerBuilder triggerBuilder)
        {
            OscillatorScheduleBuilder builder = OscillatorScheduleBuilder.Create();
            return triggerBuilder.WithSchedule(builder);
        }
        /// <summary>
        /// 带入Oscillator调度器
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
