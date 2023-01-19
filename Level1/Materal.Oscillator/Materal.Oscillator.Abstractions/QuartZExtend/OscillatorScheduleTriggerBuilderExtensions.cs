using Quartz;

namespace Materal.Oscillator.Abstractions.QuartZExtend
{
    public static class OscillatorScheduleTriggerBuilderExtensions
    {
        public static TriggerBuilder WithOscillatorSchedule(this TriggerBuilder triggerBuilder)
        {
            OscillatorScheduleBuilder builder = OscillatorScheduleBuilder.Create();
            return triggerBuilder.WithSchedule(builder);
        }

        public static TriggerBuilder WithOscillatorSchedule(this TriggerBuilder triggerBuilder, Action<OscillatorScheduleBuilder> action)
        {
            OscillatorScheduleBuilder builder = OscillatorScheduleBuilder.Create();
            action(builder);
            return triggerBuilder.WithSchedule(builder);
        }
    }
}
