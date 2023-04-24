using Materal.Abstractions;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 每日计划触发器
    /// </summary>
    public interface IEveryDayTrigger
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
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="everyDayTriggerData"></param>
        /// <returns></returns>
        public IEveryDayTrigger Deserialization(string everyDayTriggerData);
    }
}
