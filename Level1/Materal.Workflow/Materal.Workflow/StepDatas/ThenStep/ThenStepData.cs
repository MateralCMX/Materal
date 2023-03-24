using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WorkflowCore.Interface;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 普通节点数据
    /// </summary>
    public class ThenStepData : StepData, IStepData
    {
        /// <summary>
        /// 节点类型名称
        /// </summary>
        public virtual string StepBodyType { get; set; } = string.Empty;
        /// <summary>
        /// 输入
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<InputData>? Inputs { get; set; }
        /// <summary>
        /// 输出
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<OutputData>? Outputs { get; set; }
        /// <summary>
        /// 补偿处理节点
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ThenStepData? CompensateStep { get; set; }
        /// <summary>
        /// 下一步
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IStepData? Next { get; set; }
        /// <summary>
        /// 错误处理
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ErrorHandler? Error { get; set; }
    }
    /// <summary>
    /// 普通节点数据
    /// </summary>
    public class ThenStepData<TStepBody> : ThenStepData
        where TStepBody : IStepBody
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override string StepBodyType
        {
            get => typeof(TStepBody).Name;
            set => base.StepBodyType = value;
        }
    }
}
