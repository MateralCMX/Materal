namespace Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers
{
    /// <summary>
    /// 不执行时间触发器数据
    /// </summary>
    public class NoneTimeTriggerData() : TimeTriggerDataBase("不执行"), ITimeTriggerData
    {
        /// <inheritdoc/>
        public override string GetDescriptionText() => $"的 任何时间点都不会执行。";
    }
}
