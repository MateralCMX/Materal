using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Materal.Dispatcher.Server
{
    public class MySchedulerListener : ISchedulerListener
    {
        public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 添加作业 {jobDetail.Description}");
            }, cancellationToken);
        }

        public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 移除作业 {jobKey.Name}");
            }, cancellationToken);
        }

        public async Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 作业被打断 {jobKey.Name}");
            }, cancellationToken);
        }

        public async Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 作业暂定 {jobKey.Name}");
            }, cancellationToken);
        }

        public async Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 作业恢复 {jobKey.Name}");
            }, cancellationToken);
        }

        public async Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener JobScheduled {trigger.Description}");
            }, cancellationToken);
        }

        public async Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 作业组暂停 {jobGroup}");
            }, cancellationToken);
        }

        public async Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 作业组恢复 {jobGroup}");
            }, cancellationToken);
        }

        public async Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener JobUnscheduled {triggerKey.Name}");
            }, cancellationToken);
        }

        public async Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 调度器错误 {msg}");
            }, cancellationToken);
        }

        public async Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 调度器待机");
            }, cancellationToken);
        }

        public async Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 调度器已关闭");
            }, cancellationToken);
        }

        public async Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 调度器关闭中");
            }, cancellationToken);
        }

        public async Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 调度器启动完毕");
            }, cancellationToken);
        }

        public async Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 调度器启动中");
            }, cancellationToken);
        }

        public async Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 调度器数据清除");
            }, cancellationToken);
        }

        public async Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 触发器被触发 {trigger.Description}");
            }, cancellationToken);
        }

        public async Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 触发器暂停 {triggerKey.Name}");
            }, cancellationToken);
        }

        public async Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 触发器恢复 {triggerKey.Name}");
            }, cancellationToken);
        }

        public async Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 触发器组暂停 {triggerGroup}");
            }, cancellationToken);
        }

        public async Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine();
                Console.WriteLine($"[{DateTime.Now}]MySchedulerListener 触发器组恢复 {triggerGroup}");
            }, cancellationToken);
        }
    }
}
