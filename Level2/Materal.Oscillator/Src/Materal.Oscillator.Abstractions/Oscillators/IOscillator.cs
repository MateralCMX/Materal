using Materal.Oscillator.Abstractions.PlanTriggers;

namespace Materal.Oscillator.Abstractions.Oscillators
{
    /// <summary>
    /// 调度器
    /// </summary>
    public interface IOscillator
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
        /// 调度器数据
        /// </summary>
        IOscillatorData OscillatorData { get; }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="jobExecutionContext"></param>
        /// <param name="planTriggerData"></param>
        /// <returns></returns>
        Task ExecuteAsync(IJobExecutionContext jobExecutionContext, IPlanTriggerData planTriggerData);
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data"></param>
        Task SetDataAsync(IOscillatorData data);
    }
}
