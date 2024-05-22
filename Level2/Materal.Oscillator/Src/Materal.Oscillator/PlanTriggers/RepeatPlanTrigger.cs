using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.PlanTriggers.DateTrigger;
using Materal.Oscillator.PlanTriggers.TimeTrigger;
using Newtonsoft.Json.Linq;

namespace Materal.Oscillator.PlanTriggers
{
    /// <summary>
    /// 重复执行计划触发器
    /// </summary>
    public class RepeatPlanTrigger() : PlanTriggerBase("重复执行计划"), IPlanTrigger
    {
        /// <summary>
        /// 重复标识
        /// </summary>
        public override bool CanRepeated => true;
        /// <summary>
        /// 日期触发器类型名称
        /// </summary>
        public string DateTriggerTypeName => DateTrigger.GetType().Name;
        /// <summary>
        /// 日期触发器
        /// </summary>
        public IDateTrigger DateTrigger { get; set; } = new DateNotRunTrigger();
        /// <summary>
        /// 时间触发器类型名称
        /// </summary>
        public string TimeTriggerTypeName => TimeTrigger.GetType().Name;
        /// <summary>
        /// 时间触发器
        /// </summary>
        public ITimeTrigger TimeTrigger { get; set; } = new TimeNotRunTrigger();
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override async Task DeserializationAsync(JObject data, IServiceProvider serviceProvider)
        {
            object dataObj = data.ToObject(GetType()) ?? throw new OscillatorException("反序列化失败");
            dataObj.CopyProperties(this, [nameof(DateTrigger), nameof(TimeTrigger)]);
            string dateTriggerTypeName = data[nameof(DateTriggerTypeName)]?.ToString() ?? throw new OscillatorException($"反序列化失败:{nameof(DateTriggerTypeName)}");
            JToken dateTriggerData = data[nameof(DateTrigger)] ?? throw new OscillatorException($"反序列化失败:{nameof(DateTrigger)}");
            if (dateTriggerData is not JObject dateTriggerObject) throw new OscillatorException($"反序列化失败:{nameof(DateTrigger)}");
            DateTrigger = await OscillatorConvertHelper.DeserializationAsync<IDateTrigger>(dateTriggerTypeName, dateTriggerObject, serviceProvider);
            string timeTriggerTypeName = data[nameof(TimeTriggerTypeName)]?.ToString() ?? throw new OscillatorException($"反序列化失败:{nameof(TimeTriggerTypeName)}");
            JToken timeTriggerData = data[nameof(TimeTrigger)] ?? throw new OscillatorException($"反序列化失败:{nameof(TimeTrigger)}");
            if (timeTriggerData is not JObject timeTriggerObject) throw new OscillatorException($"反序列化失败:{nameof(TimeTrigger)}");
            TimeTrigger = await OscillatorConvertHelper.DeserializationAsync<ITimeTrigger>(timeTriggerTypeName, timeTriggerObject, serviceProvider);
        }
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <returns></returns>
        public override ITrigger? CreateTrigger(TriggerKey triggerKey)
        {
            DateTimeOffset? startTime = DateTrigger.GetDateStartTime(TimeTrigger);
            if (startTime is null) return null;
            DateTimeOffset? endTime = DateTrigger.GetDateEndTime(TimeTrigger);
            ITrigger? trigger = CreateTrigger(triggerKey, startTime.Value.DateTime, endTime?.DateTime);
            return trigger;
        }
        /// <summary>
        /// 获得描述文本
        /// </summary>
        /// <returns></returns>
        public override string GetDescriptionText() => DateTrigger.GetDescriptionText(TimeTrigger);
        /// <summary>
        /// 获得下一次执行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => DateTrigger.GetNextRunTime(upRunTime, TimeTrigger);
    }
}
