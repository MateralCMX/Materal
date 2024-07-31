using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator
{
    /// <summary>
    /// 调度器上下文
    /// </summary>
    public class OscillatorContext(IOscillatorData oscillatorData, IPlanTriggerData planTriggerData, ITrigger trigger, IServiceProvider serviceProvider) : IOscillatorContext
    {
        /// <inheritdoc/>
        public IOscillatorData OscillatorData { get; } = oscillatorData;
        /// <inheritdoc/>
        public IWorkData WorkData => OscillatorData.Work;
        /// <inheritdoc/>
        public IPlanTriggerData PlanTriggerData { get; } = planTriggerData;
        /// <inheritdoc/>
        public ITrigger Trigger { get; set; } = trigger;
        /// <inheritdoc/>
        public bool IsSuccess { get; set; } = true;
        /// <inheritdoc/>
        public Exception? Exception { get; set; }
        /// <inheritdoc/>
        public Exception? WorkException { get; set; }
        /// <inheritdoc/>
        public Exception? SuccessWorkException { get; set; }
        /// <inheritdoc/>
        public Exception? FailWorkException { get; set; }
        /// <inheritdoc/>
        public Dictionary<string, object?> ContextData { get; } = [];
        /// <inheritdoc/>
        public DateTime StartTime { get; set; } = DateTime.Now;
        /// <inheritdoc/>
        public DateTime EndTime { get; set; }
        /// <inheritdoc/>
        public long ElapsedTime { get; set; }
        /// <inheritdoc/>
        public List<IOscillatorListener> Listeners { get; } = serviceProvider.GetServices<IOscillatorListener>().ToList();
    }
}
