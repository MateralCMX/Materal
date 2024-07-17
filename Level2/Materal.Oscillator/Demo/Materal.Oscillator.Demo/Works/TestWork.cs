using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Demo.Works;
using Materal.Oscillator.Works;
using Materal.Utils;

namespace Dy.Oscillator.Demo.Works
{
    public class TestWork : WorkBase<TestWorkData>
    {
        public override async Task ExecuteAsync(IWorkContext workContext)
        {
            ConsoleQueue.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]{Data.Message}");
            await Task.CompletedTask;
        }
        public override async Task SuccessExecuteAsync(IWorkContext workContext)
        {
            ConsoleQueue.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]TestWork Success!");
            await Task.CompletedTask;
        }
        public override async Task FailExecuteAsync(IWorkContext workContext)
        {
            ConsoleQueue.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]TestWork Fail!");
            await Task.CompletedTask;
        }
    }
}
