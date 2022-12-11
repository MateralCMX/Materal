using Materal.ConvertHelper;

namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    public static class PlanTriggerExtension
    {
        /// <summary>
        /// 序列化计划触发器
        /// </summary>
        /// <param name="planTrigger">计划触发器</param>
        /// <returns></returns>
        public static string Serialize(this IPlanTrigger planTrigger) => planTrigger.ToJson();
    }
}
