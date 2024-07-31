using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 无计划
    /// </summary>
    public class NonePlanTrigger : PlanTriggerBase<NonePlanTriggerData>, IPlanTrigger
    {
        /// <inheritdoc/>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => null;
        /// <inheritdoc/>
        public override ITrigger? CreateTrigger(TriggerKey triggerKey) => null;
    }
}
