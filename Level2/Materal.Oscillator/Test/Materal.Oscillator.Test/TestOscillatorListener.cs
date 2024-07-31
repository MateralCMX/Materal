using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;
using Materal.Utils;
using Quartz;
using System.Text;

namespace Materal.Oscillator.Test
{
    public class TestOscillatorListener : OscillatorListenerBase, IOscillatorListener
    {
        public override async Task OnOscillatorHostStartBeforAsync(IScheduler scheduler)
        {
            WriteLine($"调度器主机启动");
            await Task.CompletedTask;
        }
        public override async Task OnOscillatorHostStopAfterAsync()
        {
            WriteLine($"调度器主机停止");
            await Task.CompletedTask;
        }
        public override async Task OnOscillatorRegisterAsync(IOscillatorData oscillatorData)
        {
            StringBuilder messageBuilder = new();
            messageBuilder.AppendLine($"调度器[{oscillatorData.Name}_{oscillatorData.TypeName}]注册");
            foreach (IPlanTriggerData planTriggerData in oscillatorData.PlanTriggers)
            {
                messageBuilder.AppendLine(planTriggerData.GetDescriptionText());
            }
            WriteLine(messageBuilder.ToString().Trim());
            await Task.CompletedTask;
        }
        public override async Task OnOscillatorUnRegisterAsync(IOscillatorData oscillatorData)
        {
            WriteLine($"调度器[{oscillatorData.Name}_{oscillatorData.TypeName}]反注册");
            await Task.CompletedTask;
        }
        public override async Task OnOscillatorContextInitAsync(IOscillatorContext context, IOscillator oscillator)
        {
            WriteLine($"触发计划[{context.PlanTriggerData.Name}_{context.PlanTriggerData.TypeName}]");
            WriteLine($"调度器[{context.OscillatorData.Name}_{context.OscillatorData.TypeName}]初始化");
            await Task.CompletedTask;
        }
        public override async Task OnOscillatorEndAsync(IOscillatorContext context, IOscillator oscillator)
        {
            WriteLine($"调度器[{context.OscillatorData.Name}_{context.OscillatorData.TypeName}]执行结束");
            await Task.CompletedTask;
        }
        public override async Task OnOscillatorStartAsync(IOscillatorContext context, IOscillator oscillator)
        {
            WriteLine($"调度器[{context.OscillatorData.Name}_{context.OscillatorData.TypeName}]开始执行");
            await Task.CompletedTask;
        }
        public override async Task OnWorkStartAsync(IOscillatorContext context, IWork work)
        {
            WriteLine($"任务[{context.WorkData.Name}_{context.WorkData.TypeName}]开始执行");
            await Task.CompletedTask;
        }
        public override async Task OnWorkEndAsync(IOscillatorContext context, IWork work)
        {
            WriteLine($"任务[{context.WorkData.Name}_{context.WorkData.TypeName}]执行结束");
            await Task.CompletedTask;
        }
        public override async Task OnWorkSuccessStartAsync(IOscillatorContext context, IWork work)
        {
            WriteLine($"任务[{context.WorkData.Name}_{context.WorkData.TypeName}]成功开始执行");
            await Task.CompletedTask;
        }
        public override async Task OnWorkSuccessEndAsync(IOscillatorContext context, IWork work)
        {
            WriteLine($"任务[{context.WorkData.Name}_{context.WorkData.TypeName}]成功执行结束");
            await Task.CompletedTask;
        }
        public override async Task OnWorkFailStartAsync(IOscillatorContext context, IWork work)
        {
            WriteLine($"任务[{context.WorkData.Name}_{context.WorkData.TypeName}]失败开始执行");
            await Task.CompletedTask;
        }
        public override async Task OnWorkFailEndAsync(IOscillatorContext context, IWork work)
        {
            WriteLine($"任务[{context.WorkData.Name}_{context.WorkData.TypeName}]失败执行结束");
            await Task.CompletedTask;
        }
        private static void WriteLine(string message) => ConsoleQueue.WriteLine($"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}]{message}");
    }
}
