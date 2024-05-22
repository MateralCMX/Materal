using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 无计划
    /// </summary>
    public class NonePlanTrigger() : PlanTriggerBase("无计划"), IPlanTrigger
    {
        /// <summary>
        /// 重复执行的
        /// </summary>
        public override bool CanRepeated => false;
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <returns></returns>
        public override string GetDescriptionText() => "不会自动执行，将在 手动 或 响应触发 执行。";
        /// <summary>
        /// 获得下一次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => null;
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        public override ITrigger? CreateTrigger(TriggerKey triggerKey) => null;
    }
}
