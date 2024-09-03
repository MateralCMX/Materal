using Materal.MergeBlock.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions;

namespace MMB.Demo.Application.Oscillator
{
    public class MyWork(ILogger<MyWork>? loggger = null) : MergeBlockWork<MyWorkData>
    {
        protected override async Task WorkExecuteAsync(IOscillatorContext workContext)
        {
            loggger?.LogInformation("开始执行任务");
            await Task.CompletedTask;
        }
        protected override async Task WorkInitAsync(IOscillatorContext workContext)
        {
            loggger?.LogInformation("开始初始化");
            await Task.CompletedTask;
        }
    }
}
