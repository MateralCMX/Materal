using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Demo.Works;
using Materal.Oscillator.Works;
using Materal.Utils;

namespace Dy.Oscillator.Demo.Works
{
    public class TestWork() : WorkBase<TestWorkData>("测试任务")
    {
        public override async Task ExecuteAsync(IWorkContext workContext)
        {
            ConsoleQueue.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]Hello World!");
            await Task.CompletedTask;
        }
    }
}
