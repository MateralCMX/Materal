using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.Oscillator.PlanTriggers.TimeTriggers;

/// <summary>
/// 不执行时间触发器
/// </summary>
public class NoneTimeTrigger : TimeTriggerBase<NoneTimeTriggerData>, ITimeTrigger
{
    /// <inheritdoc/>
    public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => null;
    /// <inheritdoc/>
    public override DateTimeOffset? GetTriggerStartTime(DateOnly date) => null;
    /// <inheritdoc/>
    public override DateTimeOffset? GetTriggerEndTime(DateOnly date) => null;
}
