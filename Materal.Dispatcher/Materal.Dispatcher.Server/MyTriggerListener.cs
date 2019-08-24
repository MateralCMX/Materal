using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Materal.Dispatcher.Server
{
    public class MyTriggerListener : ITriggerListener
    {
        public string Name => "MyTriggerListener";

        public async Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"[{DateTime.Now}]MyTriggerListener 触发器完毕 {trigger.Description}");
            }, cancellationToken);
        }

        public async Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"[{DateTime.Now}]MyTriggerListener 触发器被触发 {trigger.Description}");
            }, cancellationToken);
        }

        public async Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"[{DateTime.Now}]MyTriggerListener 触发器错误 {trigger.Description}");
            }, cancellationToken);
        }

        public async Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"[{DateTime.Now}]MyTriggerListener 作业执行完毕 {trigger.Description}");
            }, cancellationToken);
            return false;//false才能继续执行
        }
    }
}
