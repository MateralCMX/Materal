using Materal.Extensions;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Demo.Works;
using Microsoft.Extensions.Hosting;

namespace Materal.Oscillator.Test
{
    public class OscillatorHostedService(IOscillatorHost oscillatorHost) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //NonePlanTriggerData trigger = new();
            //NowPlanTriggerData trigger = new();
            //OneTimePlanTriggerData trigger = new() { StartTime = DateTime.Now.AddSeconds(5) };
            RepeatPlanTriggerData trigger = new()
            {
                DateTrigger = new DayDateTriggerData { StartDate = DateTime.Now.ToDateOnly(), EndDate = null, Interval = 1 },
                TimeTrigger = new RepeatTimeTriggerData { StartTime = new(0, 0, 0), EndTime = new(23, 59, 59), Interval = 5, IntervalType = TimeTriggerIntervalType.Second }
            };
            await StartOscillatorAsync([trigger]);
        }
        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
        /// <summary>
        /// 开始一个调度器
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="planTriggerDatas"></param>
        /// <returns></returns>
        protected async Task StartOscillatorAsync(List<IPlanTriggerData> planTriggerDatas)
        {
            TestWorkData workData = new() { Message = "你好,世界！" };
            await StartOscillatorAsync(workData, planTriggerDatas);
        }
        /// <summary>
        /// 开始一个调度器
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="planTriggerDatas"></param>
        /// <returns></returns>
        protected async Task StartOscillatorAsync(IWorkData workData, List<IPlanTriggerData> planTriggerDatas)
        {
            NormalOscillatorData oscillatorData = new()
            {
                Name = "测试调度器",
                Enable = true,
                PlanTriggers = planTriggerDatas,
                Work = workData
            };
            await oscillatorHost.StartOscillatorAsync(oscillatorData);
        }
    }
}
