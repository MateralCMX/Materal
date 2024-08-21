using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Demo.Works;
using Materal.Utils;

namespace Dy.Oscillator.Demo.Works
{
    public class TestWork : WorkBase<TestWorkData>
    {
        protected override async Task ExcuteWorkAsync(IOscillatorContext oscillatorContext)
        {
            ConsoleQueue.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]{Data.Message}");
            await Task.CompletedTask;
        }
        protected override async Task ExcuteSuccessWorkAsync(IOscillatorContext oscillatorContext)
        {
            ConsoleQueue.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]TestWork Success!");
            await Task.CompletedTask;
        }
        protected override async Task ExcuteFailWorkAsync(IOscillatorContext oscillatorContext)
        {
            ConsoleQueue.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]TestWork Fail!");
            await Task.CompletedTask;
        }
    }
}
