using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.Works.EmptyWork;
using Newtonsoft.Json.Linq;

namespace Materal.Oscillator
{
    /// <summary>
    /// 默认调度器
    /// </summary>
    public class DefaultOscillator : DefaultOscillatorData, IOscillator
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 作业明细
        /// </summary>
        public IWorkData WorkData { get; set; }
        /// <summary>
        /// 触发器组
        /// </summary>
        public List<IPlanTrigger> Triggers { get; set; } = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        public DefaultOscillator() : this(new EmptyWorkData()) { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="triggers"></param>
        public DefaultOscillator(IWorkData workData, params IPlanTrigger[] triggers)
        {
            WorkData = workData;
            Triggers.AddRange(triggers);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override async Task DeserializationAsync(string jsonData, IServiceProvider serviceProvider)
        {
            JObject jsonObject = JObject.Parse(jsonData);
            if (jsonObject[nameof(ID)] is JValue idValue)
            {
                ID = idValue.ToObject<Guid>();
            }
            if (jsonObject[nameof(Enable)] is JValue enableValue)
            {
                Enable = enableValue.ToObject<bool>();
            }
            if (jsonObject[nameof(WorkData)] is JObject workDataObject)
            {
                WorkData = await OscillatorConvertHelper.DeserializationAsync<IWorkData>(workDataObject, serviceProvider);
            }
            if (jsonObject[nameof(Triggers)] is JArray triggersArray)
            {
                foreach (JToken triggerToken in triggersArray)
                {
                    IPlanTrigger trigger = await OscillatorConvertHelper.DeserializationAsync<IPlanTrigger>(triggerToken, serviceProvider);
                    Triggers.Add(trigger);
                }
            }
        }
    }
}
