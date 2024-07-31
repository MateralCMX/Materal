using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;
using Newtonsoft.Json.Linq;

namespace Materal.Oscillator.Abstractions.Oscillators
{
    /// <summary>
    /// 调度器数据基类
    /// </summary>
    /// <param name="name"></param>
    public abstract class OscillatorDataBase(string name) : DefaultData, IOscillatorData
    {
        /// <inheritdoc/>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <inheritdoc/>
        public string Name { get; set; } = name;
        /// <inheritdoc/>
        public bool Enable { get; set; } = true;
        /// <inheritdoc/>
        public IWorkData Work { get; set; } = new EmptyWorkData();
        /// <inheritdoc/>
        public List<IPlanTriggerData> PlanTriggers { get; set; } = [];
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
            if (jsonObject[nameof(Work)] is JObject workObject)
            {
                Work = await IData.DeserializationAsync<IWorkData>(workObject, serviceProvider);
            }
            if (jsonObject[nameof(PlanTriggers)] is JArray planTriggersArray)
            {
                foreach (JToken PlanTriggerToken in planTriggersArray)
                {
                    if (PlanTriggerToken is not JObject nodeObject) throw new OscillatorException($"反序列化失败:{nameof(PlanTriggers)}");
                    IPlanTriggerData planTrigger = await IData.DeserializationAsync<IPlanTriggerData>(nodeObject, serviceProvider);
                    PlanTriggers.Add(planTrigger);
                }
            }
        }
    }
}
