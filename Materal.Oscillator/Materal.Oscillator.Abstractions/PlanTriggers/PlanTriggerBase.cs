using Materal.ConvertHelper;
using Materal.Oscillator.Abstractions.QuartZExtend;
using Quartz;

namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    public abstract class PlanTriggerBase : IPlanTrigger
    {
        public abstract bool CanRepeated { get; }
        /// <summary>
        /// 创建Trigger
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public virtual ITrigger? CreateTrigger(string name, string group, DateTime startTime, DateTime? endTime = null)
        {
            if (startTime < DateTime.Now && !CanRepeated) return null;
            TriggerBuilder triggerBuilder = TriggerBuilder.Create()
                .WithIdentity(name, group)
                .StartAt(startTime);
            if (endTime.HasValue)
            {
                triggerBuilder = triggerBuilder.EndAt(endTime.Value);
            }
            if (CanRepeated)
            {
                triggerBuilder = triggerBuilder.WithOscillatorSchedule(m => m.WithTriggerData(this).RepeatForever());
            }
            ITrigger trigger = triggerBuilder.Build();
            return trigger;
        }
        public virtual ITrigger? CreateTrigger(TriggerKey triggerKey)
        {
            return CreateTrigger(triggerKey.Name, triggerKey.Group);
        }
        public abstract ITrigger? CreateTrigger(string name, string group);
        public abstract string GetDescriptionText();
        public abstract DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime);
        public virtual IPlanTrigger Deserialization(string triggerData) => (IPlanTrigger)triggerData.JsonToObject(GetType());
    }
}
