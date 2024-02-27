using Materal.Oscillator.Abstractions.Models;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 每日计划触发器
    /// </summary>
    public interface IEveryDayTrigger : IOscillatorOperationModel<IEveryDayTrigger>
    {
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTimeOffset? GetTriggerStartTime(Date date);
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTimeOffset? GetTriggerEndTime(Date date);
        /// <summary>
        /// 获得下次执行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <returns></returns>
        public string GetDescriptionText();
    }
}
