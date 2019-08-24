using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Materal.Dispatcher.Server
{
    public class MyJobListener : IJobListener
    {
        public string Name => "MyJobListener";

        public async Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MyJobListener 作业执行错误 {context.JobDetail.Description}");
            }, cancellationToken);
        }

        public async Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MyJobListener 作业开始执行 {context.JobDetail.Description}");
            }, cancellationToken);
        }

        public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MyJobListener 作业执行完毕 {context.JobDetail.Description}");
            }, cancellationToken);
        }
    }
}
