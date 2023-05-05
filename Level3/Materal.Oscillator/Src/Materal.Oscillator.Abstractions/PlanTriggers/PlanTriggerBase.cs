using Materal.Oscillator.Abstractions.QuartZExtend;
using Quartz;

namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 计划触发器
    /// </summary>
    public abstract class PlanTriggerBase : IPlanTrigger
    {
        /// <summary>
        /// 重复标识
        /// </summary>
        public abstract bool CanRepeated { get; }
        /// <summary>
        /// 创建Trigger
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual ITrigger? CreateTrigger(string name, string group, DateTime startTime, DateTime? endTime = null)
        {
            if (startTime < DateTime.Now && !CanRepeated) return null;
            TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(name, group)
                .StartAt(startTime);
            if (endTime.HasValue)
            {
                triggerBuilder = triggerBuilder.EndAt(endTime.Value);
            }
            if (CanRepeated)
            {
                triggerBuilder = triggerBuilder.WithOscillatorSchedule(m => m.WithTriggerData(this).RepeatForever());
            }
            ITrigger trigger = triggerBuilder.Build();
            return trigger;
        }
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        public virtual ITrigger? CreateTrigger(TriggerKey triggerKey)
        {
            return CreateTrigger(triggerKey.Name, triggerKey.Group);
        }
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public abstract ITrigger? CreateTrigger(string name, string group);
        /// <summary>
        /// 获得描述文本
        /// </summary>
        /// <returns></returns>
        public abstract string GetDescriptionText();
        /// <summary>
        /// 获得下一次执行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public abstract DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="triggerData"></param>
        /// <returns></returns>
        public virtual IPlanTrigger Deserialization(string triggerData) => (IPlanTrigger)triggerData.JsonToObject(GetType());
    }
}
