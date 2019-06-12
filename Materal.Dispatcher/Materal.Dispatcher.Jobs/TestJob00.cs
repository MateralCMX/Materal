using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Materal.Dispatcher.Jobs
{
    [DisallowConcurrentExecution]
    public class TestJob00 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => {
                Console.WriteLine();
                Console.WriteLine("********************************************");
                Console.WriteLine($"[{DateTime.Now}]TestJob00执行,线程ID:{Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine("********************************************");
            });
        }
    }
}
