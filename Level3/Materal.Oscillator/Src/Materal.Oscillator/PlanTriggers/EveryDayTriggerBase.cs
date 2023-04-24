using Materal.Abstractions;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 每日触发器基类
    /// </summary>
    public abstract class EveryDayTriggerBase : IEveryDayTrigger
    {
        public virtual IEveryDayTrigger Deserialization(string everyDayTriggerData) => (IEveryDayTrigger)everyDayTriggerData.JsonToObject(GetType());
        public abstract string GetDescriptionText();
        public abstract DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        public abstract DateTimeOffset? GetTriggerEndTime(Date date);
        public abstract DateTimeOffset? GetTriggerStartTime(Date date);
    }
}
