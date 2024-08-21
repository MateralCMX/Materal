namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 不执行计划触发器数据
    /// </summary>
    public class NonePlanTriggerData() : PlanTriggerDataBase("不执行")
    {
        /// <inheritdoc/>
        public override bool CanRepeated => false;
        /// <inheritdoc/>
        public override string GetDescriptionText() => "不会执行。";
    }
}
