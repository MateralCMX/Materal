using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// 调度器监听器基类
    /// </summary>
    public abstract class OscillatorListenerBase : IOscillatorListener
    {
        /// <inheritdoc/>
        public virtual async Task OnOscillatorContextInitAsync(IOscillatorContext context, IOscillator oscillator)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnOscillatorEndAsync(IOscillatorContext context, IOscillator oscillator)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnOscillatorHostStartBeforAsync(IScheduler scheduler)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnOscillatorHostStopAfterAsync()
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnOscillatorRegisterAsync(IOscillatorData oscillatorData)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnOscillatorStartAsync(IOscillatorContext context, IOscillator oscillator)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnOscillatorUnRegisterAsync(IOscillatorData oscillatorData)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnWorkEndAsync(IOscillatorContext context, IWork work)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnWorkFailEndAsync(IOscillatorContext context, IWork work)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnWorkFailStartAsync(IOscillatorContext context, IWork work)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnWorkStartAsync(IOscillatorContext context, IWork work)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnWorkSuccessEndAsync(IOscillatorContext context, IWork work)
            => await Task.CompletedTask;
        /// <inheritdoc/>
        public virtual async Task OnWorkSuccessStartAsync(IOscillatorContext context, IWork work)
            => await Task.CompletedTask;
    }
}
