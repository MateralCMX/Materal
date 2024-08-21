using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 单次执行计划触发器
    /// </summary>
    public class OneTimePlanTrigger : PlanTriggerBase<OneTimePlanTriggerData>, IPlanTrigger
    {
        /// <summary>
        /// 获得下一次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime)
        {
            DateTime startTime = DateTime.SpecifyKind(Data.StartTime, DateTimeKind.Local);
            if (startTime < upRunTime) return null;
            return startTime;
        }
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        public override ITrigger? CreateTrigger(TriggerKey triggerKey) => CreateTrigger(triggerKey, Data.StartTime);
    }
}
