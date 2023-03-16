using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 事件节点数据
    /// </summary>
    public class EventStepData : StepData, IStepData
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; set; }
        /// <summary>
        /// 事件Key
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 事件Key来源
        /// </summary>
        public ValueSourceEnum EventKeySource { get; set; }
        /// <summary>
        /// 等待时间
        /// </summary>
        public TimeSpan? ValidTime { get; set; }
        /// <summary>
        /// 输出
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<OutputData>? Outputs { get; set; }
        /// <summary>
        /// 下一步
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IStepData? Next { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public EventStepData() : this("MyEvent", "MainKey")
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventKey"></param>
        public EventStepData(string eventName, string eventKey) : this(eventName, eventKey, ValueSourceEnum.Constant)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="eventKey"></param>
        /// <param name="eventKeySource"></param>
        public EventStepData(string eventName, string eventKey, ValueSourceEnum eventKeySource)
        {
            EventName = eventName;
            EventKey = eventKey;
            EventKeySource = eventKeySource;
        }
    }
}
