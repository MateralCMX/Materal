using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 不执行
    /// </summary>
    public class NoneDateTriggerData() : DateTriggerDataBase("不执行"), IDateTriggerData
    {
        /// <inheritdoc/>
        public override string GetDescriptionText(ITimeTriggerData timeTriggerData) => "不会执行。";
    }
}
