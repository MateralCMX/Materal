using Materal.Oscillator.Abstractions.PlanTriggers;
using Quartz;

namespace Materal.Oscillator.PlanTriggers
{
    public class NowPlanTrigger : PlanTriggerBase, IPlanTrigger
    {
        public override bool CanRepeated => false;
        /// <summary>
        /// 开始时间
        /// </summary>
        private readonly DateTime _startTime = DateTime.Now;
        /// <summary>
        /// 获得介绍文本
        /// </summary>
        /// <returns></returns>
        public override string GetDescriptionText() => $"将在 {_startTime:yyyy-MM-dd HH:mm:ss} 执行一次。";
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => DateTime.SpecifyKind(_startTime, DateTimeKind.Local);
        public override ITrigger? CreateTrigger(string name, string group)
        {
            TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(name, group)
                .StartAt(_startTime);
            ITrigger trigger = triggerBuilder.Build();
            return trigger;
        }
    }
}
