using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;

namespace Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers
{
    /// <summary>
    /// 日期触发器
    /// </summary>
    public interface IDateTrigger
    {
        /// <summary>
        /// 数据类型
        /// </summary>
        Type DataType { get; }
        /// <summary>
        /// 日期触发器数据
        /// </summary>
        IDateTriggerData DateTriggerData { get; }
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="timeTrigger"></param>
        /// <returns></returns>
        DateTimeOffset? GetDateStartTime(ITimeTrigger timeTrigger);
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="timeTrigger"></param>
        /// <returns></returns>
        DateTimeOffset? GetDateEndTime(ITimeTrigger timeTrigger);
        /// <summary>
        /// 获得下次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <param name="timeTrigger"></param>
        /// <returns></returns>
        DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, ITimeTrigger timeTrigger);
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task SetDataAsync(IDateTriggerData data);
    }
}
