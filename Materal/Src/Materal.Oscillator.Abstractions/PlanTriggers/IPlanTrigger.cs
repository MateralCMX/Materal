using Materal.Oscillator.Abstractions.Oscillators;

namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 计划触发器
    /// </summary>
    public interface IPlanTrigger
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid ID { get; }
        /// <summary>
        /// 类型名称
        /// </summary>
        string TypeName { get; }
        /// <summary>
        /// 数据类型
        /// </summary>
        Type DataType { get; }
        /// <summary>
        /// 计划触发器数据
        /// </summary>
        IPlanTriggerData PlanTriggerData { get; }
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
        /// <param name="oscillatorData"></param>
        /// <returns></returns>
        ITrigger? CreateTrigger(IOscillatorData oscillatorData);
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        ITrigger? CreateTrigger(TriggerKey triggerKey);
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data"></param>
        Task SetDataAsync(IPlanTriggerData data);
    }
}
