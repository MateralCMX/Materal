using Materal.Oscillator.Abstractions.PlanTriggers;
using Quartz;

namespace Materal.Oscillator.PlanTriggers
{
    public class OneTimePlanTrigger : PlanTriggerBase, IPlanTrigger
    {
        public override bool CanRepeated => false;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;
        public OneTimePlanTrigger()
        {
        }
        public OneTimePlanTrigger(DateTime startTime)
        {
            StartTime = startTime;
        }
        /// <summary>
        /// 获得介绍文本
        /// </summary>
        /// <returns></returns>
        public override string GetDescriptionText() => $"将在 {StartTime:yyyy-MM-dd HH:mm:ss} 执行一次。";
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => DateTime.SpecifyKind(StartTime, DateTimeKind.Local);
        public override ITrigger? CreateTrigger(string name, string group)
        {
            ITrigger? trigger = CreateTrigger(name, group, StartTime);
            return trigger;
        }
    }
}
