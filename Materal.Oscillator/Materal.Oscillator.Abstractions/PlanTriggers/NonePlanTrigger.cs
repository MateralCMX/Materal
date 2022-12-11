using Quartz;

namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    public class NonePlanTrigger : PlanTriggerBase, IPlanTrigger
    {
        public override bool CanRepeated => false;
        public override string GetDescriptionText() => "不会自动执行，将在 手动 或 响应触发 执行。";
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => null;
        public override ITrigger? CreateTrigger(string name, string group)
        {
            return null;
        }
    }
}
