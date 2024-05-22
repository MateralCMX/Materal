using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 立即执行计划触发器
    /// </summary>
    public class NowPlanTrigger() : PlanTriggerBase("立即执行"), IPlanTrigger
    {
        /// <summary>
        /// 重复执行的
        /// </summary>
        public override bool CanRepeated => false;
        /// <summary>
        /// 开始时间
        /// </summary>
        private readonly DateTime _startTime = DateTime.Now;
        /// <summary>
        /// 获得介绍文本
        /// </summary>
        /// <returns></returns>
        public override string GetDescriptionText() => $"将在 {_startTime:yyyy-MM-dd HH:mm:ss} 执行一次。";
        /// <summary>
        /// 获得下一次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime)
        {
            DateTime startTime = DateTime.SpecifyKind(_startTime, DateTimeKind.Local);
            if (startTime < upRunTime) return null;
            return startTime;
        }
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        public override ITrigger? CreateTrigger(TriggerKey triggerKey)
        {
            TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .StartAt(_startTime);
            ITrigger trigger = triggerBuilder.Build();
            return trigger;
        }
    }
}
