using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 单次执行计划触发器
    /// </summary>
    public class OneTimePlanTrigger() : PlanTriggerBase("执行一次"), IPlanTrigger
    {
        /// <summary>
        /// 重复标识
        /// </summary>
        public override bool CanRepeated => false;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="startTime"></param>
        public OneTimePlanTrigger(DateTime startTime) : this()
        {
            StartTime = startTime;
        }
        /// <summary>
        /// 获得介绍文本
        /// </summary>
        /// <returns></returns>
        public override string GetDescriptionText() => $"将在 {StartTime:yyyy-MM-dd HH:mm:ss} 执行一次。";
        /// <summary>
        /// 获得下一次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime)
        {
            DateTime startTime = DateTime.SpecifyKind(StartTime, DateTimeKind.Local);
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
            ITrigger? trigger = CreateTrigger(triggerKey, StartTime);
            return trigger;
        }
    }
}
