using Materal.Oscillator.Abstractions.Helper;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.PlanTriggers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;

namespace Materal.Oscillator.Abstractions.Services.Trigger
{
    /// <summary>
    /// 重复计划数据模型
    /// </summary>
    public class RepeatPlanTrigger : PlanTriggerBase, IPlanTrigger
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
        /// 每日触发器类型名称
        /// </summary>
        public string EveryDayTriggerTypeName => EveryDayTrigger.GetType().Name;
        /// <summary>
        /// 每日触发器
        /// </summary>
        public IEveryDayTrigger EveryDayTrigger { get; set; } = new EveryDayNotRunTrigger();
        /// <summary>
        /// 创建触发器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="group"></param>
        /// <returns></returns>
        public override ITrigger? CreateTrigger(string name, string group)
        {
            DateTimeOffset? startTime = DateTrigger.GetDateStartTime(EveryDayTrigger);
            if (startTime is null) return null;
            DateTimeOffset? endTime = DateTrigger.GetDateEndTime(EveryDayTrigger);
            ITrigger? trigger = CreateTrigger(name, group, startTime.Value.DateTime, endTime?.DateTime);
            return trigger;
        }
        /// <summary>
        /// 获得描述文本
        /// </summary>
        /// <returns></returns>
        public override string GetDescriptionText() => DateTrigger.GetDescriptionText(EveryDayTrigger);
        /// <summary>
        /// 获得下一次执行时间
        /// </summary>
        /// <param name="upRunTime"></param>
        /// <returns></returns>
        public override DateTimeOffset? GetNextRunTime(DateTimeOffset upRunTime) => DateTrigger.GetNextRunTime(upRunTime, EveryDayTrigger);
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="triggerData"></param>
        /// <returns></returns>
        public override IPlanTrigger Deserialization(string triggerData)
        {
            RepeatPlanTrigger result = new();
            JObject? jObj = JsonConvert.DeserializeObject<JObject>(triggerData);
            if (jObj == null) return result;
            string? dateFrequencyTypeName = jObj[nameof(DateTriggerTypeName)]?.ToString();
            string? dateFrequencyDataJson = jObj[nameof(DateTrigger)]?.ToJson();
            string? everyDayFrequencyTypeName = jObj[nameof(EveryDayTriggerTypeName)]?.ToString();
            string? everyDayFrequencyDataJson = jObj[nameof(EveryDayTrigger)]?.ToJson();
            if (dateFrequencyTypeName != null && !string.IsNullOrWhiteSpace(dateFrequencyTypeName) && dateFrequencyDataJson != null && !string.IsNullOrWhiteSpace(dateFrequencyDataJson))
            {
                IDateTrigger? dateTrigger = OscillatorConvertHelper.ConvertToInterface<IDateTrigger>(dateFrequencyTypeName, dateFrequencyDataJson);
                if (dateTrigger != null)
                {
                    result.DateTrigger = dateTrigger;
                }
            }
            if (everyDayFrequencyTypeName != null && !string.IsNullOrWhiteSpace(everyDayFrequencyTypeName) && everyDayFrequencyDataJson != null && !string.IsNullOrWhiteSpace(everyDayFrequencyDataJson))
            {
                IEveryDayTrigger? everyDayTrigger = OscillatorConvertHelper.ConvertToInterface<IEveryDayTrigger>(everyDayFrequencyTypeName, everyDayFrequencyDataJson);
                if (everyDayTrigger != null)
                {
                    result.EveryDayTrigger = everyDayTrigger;
                }
            }
            return result;
        }
    }
}
