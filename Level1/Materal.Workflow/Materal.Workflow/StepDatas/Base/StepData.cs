using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 步骤数据
    /// </summary>
    public abstract class StepData : IStepData
    {
        /// <summary>
        /// 节点数据类型名称
        /// </summary>
        public string StepDataType => GetType().Name;
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public virtual string Name { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Description { get; set; }
        /// <summary>
        /// 构建数据
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, object?>? BuildData { get; set; }
    }
}
