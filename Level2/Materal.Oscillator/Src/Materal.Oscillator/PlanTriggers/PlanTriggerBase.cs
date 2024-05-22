using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Extensions;
using Materal.Oscillator.QuartZExtend;
using Newtonsoft.Json;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 计划触发器
    /// </summary>
    public abstract class PlanTriggerBase(string name) : DefaultOscillatorData, IPlanTrigger
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = name;
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 重复标识
        /// </summary>
        [JsonIgnore]
        public abstract bool CanRepeated { get; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public PlanTriggerBase() : this("新计划触发器") { }
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        public ITrigger? CreateTrigger(IOscillator oscillator)
        {
            TriggerKey triggerKey = this.GetTriggerKey(oscillator);
            return CreateTrigger(triggerKey);
        }
        /// <summary>
        /// 创建Trigger
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        protected virtual ITrigger? CreateTrigger(TriggerKey triggerKey, DateTime startTime, DateTime? endTime = null)
        {
            TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
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
        public abstract ITrigger? CreateTrigger(TriggerKey triggerKey);
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
    }
}
