using Dy.Oscillator.Demo.Works;
using Materal.Extensions;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.PlanTriggers.DateTrigger;
using Materal.Oscillator.PlanTriggers.TimeTrigger;
using Materal.Utils;
using Microsoft.Extensions.Hosting;

namespace Materal.Oscillator.Demo.Services
{
    internal class TestService(IOscillatorHost oscillatorHost) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            IWork work = new TestWork();
            IPlanTrigger planTrigger = new RepeatPlanTrigger()
            {
                Name = "测试计划",
                DateTrigger = new DateDayTrigger
                {
                    StartDate = DateTime.Now.ToDate(),
                    Interval = 1,
                    EndDate = DateTime.Now.AddDays(1).ToDate(),
                },
                TimeTrigger = new TimeRepeatTrigger
                {
                    StartTime = new(0, 0, 0),
                    Interval = 5,
                    EndTime = new(23, 59, 59),
                    IntervalType = TimeTriggerIntervalType.Second,
                }
            };
            IOscillator oscillator = new DefaultOscillator(work, planTrigger);
            string planTriggerDescription = planTrigger.GetDescriptionText();
            ConsoleQueue.WriteLine(planTriggerDescription);
            await oscillatorHost.StartOscillatorAsync(oscillator);
        }

        public async Task StopAsync(CancellationToken cancellationToken) => await oscillatorHost.StopAsync();
    }
}
