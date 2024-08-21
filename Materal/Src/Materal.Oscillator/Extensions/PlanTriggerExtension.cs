//using Materal.Oscillator.Abstractions.Oscillator;
//using Materal.Oscillator.Abstractions.PlanTriggers;

//namespace Materal.Oscillator.Extensions
//{
//    /// <summary>
//    /// 计划触发器扩展
//    /// </summary>
//    internal static class PlanTriggerExtension
//    {
//        /// <summary>
//        /// 获得TriggerKey
//        /// </summary>
//        /// <param name="trigger"></param>
//        /// <param name="oscillator"></param>
//        /// <returns></returns>
//        public static TriggerKey GetTriggerKey(this IPlanTrigger trigger, IOscillator oscillator)
//        {
//            string group = oscillator.GetGroup();
//            TriggerKey triggerKey = new($"{trigger.ID}_{trigger.Name}", group);
//            return triggerKey;
//        }
//    }
//}
