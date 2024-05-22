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
        public IWork Work { get; set; }
        /// <summary>
        /// 触发器组
        /// </summary>
        public List<IPlanTrigger> Triggers { get; set; } = [];
        /// <summary>
        /// 构造方法
        /// </summary>
        public DefaultOscillator() : this(new EmptyWork()) { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="work"></param>
        /// <param name="triggers"></param>
        public DefaultOscillator(IWork work, params IPlanTrigger[] triggers)
        {
            Work = work;
            Triggers.AddRange(triggers);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override async Task DeserializationAsync(JObject data, IServiceProvider serviceProvider)
        {
            ID = data[nameof(ID)]?.ToObject<Guid>() ?? throw new OscillatorException($"反序列化失败:{nameof(ID)}");
            Enable = data[nameof(Enable)]?.ToObject<bool>() ?? throw new OscillatorException($"反序列化失败:{nameof(Enable)}");
            JToken workDataJson = data[nameof(Work)] ?? throw new OscillatorException($"反序列化失败:{nameof(Work)}");
            if (workDataJson is not JObject workObject) throw new OscillatorException($"反序列化失败:{nameof(Work)}");
            Work = await OscillatorConvertHelper.CreateNewWorkAsync(workObject, serviceProvider);
            JToken triggersData = data[nameof(Triggers)] ?? throw new OscillatorException($"反序列化失败:{nameof(Triggers)}");
            if (triggersData is not JArray triggersArrayData) throw new OscillatorException($"反序列化失败:{nameof(Triggers)}");
            foreach (JToken triggerData in triggersArrayData)
            {
                if (triggerData is not JObject triggerObject) throw new OscillatorException("反序列化失败:TriggerValue");
                IPlanTrigger trigger = await OscillatorConvertHelper.DeserializationAsync<IPlanTrigger>(triggerObject, serviceProvider);
                Triggers.Add(trigger);
            }
        }
    }
}
