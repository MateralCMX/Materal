using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.Oscillators
{
    /// <summary>
    /// 普通调度器
    /// </summary>
    /// <param name="serviceProvider"></param>
    public class NormalOscillator(IServiceProvider serviceProvider) : OscillatorBase<NormalOscillatorData>(serviceProvider)
    {
        /// <inheritdoc/>
        protected override IOscillatorContext CreateOscillatorContext(IJobExecutionContext jobExecutionContext, IPlanTriggerData planTriggerData) => new OscillatorContext(Data, planTriggerData, jobExecutionContext.Trigger, ServiceProvider);
    }
}
