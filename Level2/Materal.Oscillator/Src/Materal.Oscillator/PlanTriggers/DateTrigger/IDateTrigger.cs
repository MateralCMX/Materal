using Materal.Oscillator.PlanTriggers.TimeTrigger;

namespace Materal.Oscillator.PlanTriggers.DateTrigger
{
    /// <summary>
    /// 日期触发器
    /// </summary>
    public interface IDateTrigger : IOscillatorData
    {
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        string GetDescriptionText(ITimeTrigger everyDayTrigger);
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        DateTimeOffset? GetDateStartTime(ITimeTrigger everyDayTrigger);
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        DateTimeOffset? GetDateEndTime(ITimeTrigger everyDayTrigger);
        /// <summary>
        /// 获得下次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <param name="everyDayTrigger"></param>
        /// <returns></returns>
        DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime, ITimeTrigger everyDayTrigger);
    }
}
