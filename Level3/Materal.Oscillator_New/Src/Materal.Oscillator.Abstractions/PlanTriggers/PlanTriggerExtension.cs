namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 计划触发器扩展
    /// </summary>
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
