using Materal.Abstractions;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 每日触发器基类
    /// </summary>
    public abstract class EveryDayTriggerBase : IEveryDayTrigger
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="everyDayTriggerData"></param>
        /// <returns></returns>
        public virtual IEveryDayTrigger Deserialization(string everyDayTriggerData) => (IEveryDayTrigger)everyDayTriggerData.JsonToObject(GetType());
        /// <summary>
        /// 获得说明文本
        /// </summary>
        /// <returns></returns>
        public abstract string GetDescriptionText();
        /// <summary>
        /// 获得下一次运行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public abstract DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        /// <summary>
        /// 获得结束时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public abstract DateTimeOffset? GetTriggerEndTime(Date date);
        /// <summary>
        /// 获得开始时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public abstract DateTimeOffset? GetTriggerStartTime(Date date);
    }
}
