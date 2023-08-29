using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 并行节点数据
    /// </summary>
    public class BranchStepData : StepData, IStepData
    {
        /// <summary>
        /// 节点数据组
        /// </summary>
        [Required, MinLength(1)]
        public List<IStepData> StepDatas { get; set; } = new();
        /// <summary>
        /// 下一步
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IStepData? Next { get; set; }
    }
}
