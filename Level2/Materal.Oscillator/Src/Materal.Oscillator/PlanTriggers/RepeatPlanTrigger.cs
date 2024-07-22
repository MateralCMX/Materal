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
        public string DateTriggerTypeName => DateTrigger.GetType().FullName ?? throw new OscillatorException("获取DateTrigger类型名称失败");
        /// <summary>
        /// 日期触发器
        /// </summary>
        public IDateTrigger DateTrigger { get; set; } = new DateNotRunTrigger();
        /// <summary>
        /// 时间触发器类型名称
        /// </summary>
        public string TimeTriggerTypeName => TimeTrigger.GetType().FullName ?? throw new OscillatorException("获取TimeTrigger类型名称失败");
        /// <summary>
        /// 时间触发器
        /// </summary>
        public ITimeTrigger TimeTrigger { get; set; } = new TimeNotRunTrigger();
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
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override async Task DeserializationAsync(string jsonData, IServiceProvider serviceProvider)
        {
            await base.DeserializationAsync(jsonData, serviceProvider);
            JObject jsonObject = JObject.Parse(jsonData);
            if (jsonObject[nameof(DateTrigger)] is JObject dateTriggerObject)
            {
                DateTrigger = await OscillatorConvertHelper.DeserializationAsync<IDateTrigger>(dateTriggerObject, serviceProvider);
            }
            if (jsonObject[nameof(TimeTrigger)] is JObject timeTriggerObject)
            {
                TimeTrigger = await OscillatorConvertHelper.DeserializationAsync<ITimeTrigger>(timeTriggerObject, serviceProvider);
            }
        }
    }
}
