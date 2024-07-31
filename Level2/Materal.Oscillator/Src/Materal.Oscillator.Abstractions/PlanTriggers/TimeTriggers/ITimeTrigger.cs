namespace Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers
{
    /// <summary>
    /// 时间触发器
    /// </summary>
    public interface ITimeTrigger
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
        /// 时间触发器数据
        /// </summary>
        ITimeTriggerData TimeTriggerData { get; }
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTimeOffset? GetTriggerStartTime(DateOnly date);
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTimeOffset? GetTriggerEndTime(DateOnly date);
        /// <summary>
        /// 获得下次执行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task SetDataAsync(ITimeTriggerData data);
    }
}
