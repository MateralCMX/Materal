using Newtonsoft.Json;

namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 计划触发器
    /// </summary>
    public interface IPlanTrigger : IOscillatorData
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 重复执行的
        /// </summary>
        [JsonIgnore]
        bool CanRepeated { get; }
        /// <summary>
        /// 启用标识
        /// </summary>
        bool Enable { get; set; }
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <returns></returns>
        string GetDescriptionText();
        /// <summary>
        /// 获得下一次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="oscillator"></param>
        /// <returns></returns>
        ITrigger? CreateTrigger(IOscillator oscillator);
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        ITrigger? CreateTrigger(TriggerKey triggerKey);
    }
}
