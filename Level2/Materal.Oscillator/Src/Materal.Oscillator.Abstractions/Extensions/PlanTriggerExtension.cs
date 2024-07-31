using Materal.Oscillator.Abstractions.Oscillators;
using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.Abstractions.Extensions
{
    /// <summary>
    /// 计划触发器扩展
    /// </summary>
    internal static class PlanTriggerExtension
    {
        /// <summary>
        /// 获得TriggerKey
        /// </summary>
        /// <param name="planTriggerData"></param>
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        public static TriggerKey GetTriggerKey(this IPlanTriggerData planTriggerData, IOscillatorData oscillatorData)
        {
            string group = oscillatorData.GetGroup();
            TriggerKey triggerKey = new($"{planTriggerData.ID}_{planTriggerData.Name}", group);
            return triggerKey;
        }
    }
}
