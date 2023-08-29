using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 开始节点数据
    /// </summary>
    public class StartStepData : StepData, IStepData
    {
        /// <summary>
        /// 下一步
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IStepData? Next { get; set; }
    }
}
