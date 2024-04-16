using Materal.Utils;

namespace Materal.Logger.Test
{
    public class TestLogMonitor : ILogMonitor
    {
        public async Task OnLogAsync(Log log)
        {
            ConsoleQueue.WriteLine(log.Message);
            await Task.CompletedTask;
        }
    }
}