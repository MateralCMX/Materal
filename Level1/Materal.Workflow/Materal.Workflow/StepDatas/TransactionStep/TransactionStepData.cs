using Materal.Workflow.Steps;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 事务节点数据
    /// </summary>
    public class TransactionStepData : StepData, IStepData
    {
        /// <summary>
        /// 处理节点
        /// </summary>
        public IStepData StepData { get; set; } = new ThenStepData<EmptyStep>();
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
    }
}
