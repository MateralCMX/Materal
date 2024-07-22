using Materal.ContextCache;
using Materal.Extensions;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Demo.Works;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.PlanTriggers.DateTrigger;
using Materal.Oscillator.PlanTriggers.TimeTrigger;
using Materal.Utils;
using Microsoft.Extensions.Hosting;

namespace Materal.Oscillator.Demo.Services
{
    internal class TestService(IContextCacheService contextCacheService, IOscillatorHost oscillatorHost, IServiceProvider serviceProvider) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await contextCacheService.RenewAsync();
            IWorkData workData = new TestWorkData() { Message = "你好，世界！" };
            RepeatPlanTrigger planTrigger = new()
            {
                Name = "测试计划",
                DateTrigger = new DateDayTrigger
                {
                    StartDate = DateTime.Now.ToDateOnly(),
                    Interval = 1,
                    EndDate = DateTime.Now.AddDays(1).ToDateOnly(),
                },
                TimeTrigger = new TimeRepeatTrigger
                {
                    StartTime = new(0, 0, 0),
                    Interval = 5,
                    EndTime = new(23, 59, 59),
                    IntervalType = TimeTriggerIntervalType.Second,
                }
            };
            OneTimePlanTrigger planTrigger2 = new()
            {
                Name = "测试计划",
                StartTime = DateTime.Now.AddSeconds(12)
            };
            IOscillator oscillator = new DefaultOscillator(workData, planTrigger, planTrigger2);
            string oscillatorJson = await oscillator.SerializationAsync();
            oscillator = await OscillatorConvertHelper.DeserializationAsync<IOscillator>(oscillatorJson, serviceProvider);
            foreach (IPlanTrigger trigger in oscillator.Triggers)
            {
                string description = trigger.GetDescriptionText();
                ConsoleQueue.WriteLine(description);
            }
            await oscillatorHost.StartOscillatorAsync(oscillator);
        }

        public async Task StopAsync(CancellationToken cancellationToken) => await oscillatorHost.StopAsync();
    }
}
