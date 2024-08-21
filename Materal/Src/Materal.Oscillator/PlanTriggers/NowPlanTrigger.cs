using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 立即执行计划触发器
    /// </summary>
    public class NowPlanTrigger : PlanTriggerBase<NowPlanTriggerData>, IPlanTrigger
    {
        /// <summary>
        /// 获得下一次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => Data.StartTime.ToDateTimeOffset(DateTimeKind.Local);
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        public override ITrigger? CreateTrigger(TriggerKey triggerKey)
        {
            TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .StartAt(DateTime.Now);
            ITrigger trigger = triggerBuilder.Build();
            return trigger;
        }
    }
}
