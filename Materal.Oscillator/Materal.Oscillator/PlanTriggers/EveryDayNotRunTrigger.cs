using Materal.DateTimeHelper;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 每日不会执行触发器
    /// </summary>
    public class EveryDayNotRunTrigger : EveryDayTriggerBase, IEveryDayTrigger
    {
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => null;
        public override string GetDescriptionText() => $"的 任何时间点都不会执行。";

        public override DateTimeOffset? GetTriggerStartTime(Date date) => null;

        public override DateTimeOffset? GetTriggerEndTime(Date date) => null;
    }
}
