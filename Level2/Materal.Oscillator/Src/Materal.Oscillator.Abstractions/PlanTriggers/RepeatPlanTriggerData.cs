using Materal.Oscillator.Abstractions.PlanTriggers.DateTriggers;
using Materal.Oscillator.Abstractions.PlanTriggers.TimeTriggers;
using Newtonsoft.Json.Linq;

namespace Materal.Oscillator.Abstractions.PlanTriggers
{
    /// <summary>
    /// 重复执行计划触发器数据
    /// </summary>
    public class RepeatPlanTriggerData() : PlanTriggerDataBase("重复执行")
    {
        /// <inheritdoc/>
        public override bool CanRepeated => true;
        /// <summary>
        /// 日期触发器
        /// </summary>
        public IDateTriggerData DateTrigger { get; set; } = new NoneDateTriggerData();
        /// <summary>
        /// 时间触发器
        /// </summary>
        public ITimeTriggerData TimeTrigger { get; set; } = new NoneTimeTriggerData();
        /// <inheritdoc/>
        public override string GetDescriptionText() => DateTrigger.GetDescriptionText(TimeTrigger);
        /// <inheritdoc/>
        public override async Task DeserializationAsync(string jsonData, IServiceProvider serviceProvider)
        {
            JObject jsonObject = JObject.Parse(jsonData);
            if (jsonObject[nameof(ID)] is JValue idValue)
            {
                ID = idValue.ToObject<Guid>();
            }
            if (jsonObject[nameof(Name)] is JValue nameValue)
            {
                Name = nameValue.ToObject<string>() ?? throw new OscillatorException($"反序列化失败:{nameof(Name)}");
            }
            if (jsonObject[nameof(Enable)] is JValue enableValue)
            {
                Enable = enableValue.ToObject<bool>();
            }
            if (jsonObject[nameof(DateTrigger)] is JObject dateTriggerObject)
            {
                DateTrigger = await IData.DeserializationAsync<IDateTriggerData>(dateTriggerObject, serviceProvider);
            }
            if (jsonObject[nameof(TimeTrigger)] is JObject timeTriggerObject)
            {
                TimeTrigger = await IData.DeserializationAsync<ITimeTriggerData>(timeTriggerObject, serviceProvider);
            }
        }
    }
}
