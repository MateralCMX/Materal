using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 活动节点数据
    /// </summary>
    public class ActivityStepData : StepData, IStepData
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; } = string.Empty;
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// 值来源
        /// </summary>
        public ValueSourceEnum ValueySource { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public TimeSpan? ValidTime { get; set; }
        /// <summary>
        /// 输出
        /// </summary>
        public List<OutputData> Outputs { get; set; } = new();
        /// <summary>
        /// 下一步
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IStepData? Next { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public ActivityStepData() : this("MyActivity", "ActivityValue")
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="activityName"></param>
        /// <param name="value"></param>
        public ActivityStepData(string activityName, string value) : this(activityName, value, ValueSourceEnum.Constant)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="activityName"></param>
        /// <param name="value"></param>
        /// <param name="valueySource"></param>
        public ActivityStepData(string activityName, string value, ValueSourceEnum valueySource)
        {
            ActivityName = activityName;
            Value = value;
            ValueySource = valueySource;
        }
    }
}
