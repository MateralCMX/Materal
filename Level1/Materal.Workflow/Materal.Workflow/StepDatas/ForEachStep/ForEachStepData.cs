using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 项循环节点数据
    /// </summary>
    public class ForEachStepData : StepData, IStepData
    {
        /// <summary>
        /// 值
        /// </summary>
        [Required]
        public virtual object Value { get; set; } = string.Empty;
        /// <summary>
        /// 值来源
        /// </summary>
        [Required]
        public ValueSourceEnum ValueSource { get; set; }
        /// <summary>
        /// 循环节点
        /// </summary>
        [Required]
        public IStepData StepData { get; set; } = new StartStepData();
        /// <summary>
        /// 下一步
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IStepData? Next { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public ForEachStepData()
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="value"></param>
        public ForEachStepData(IEnumerable value)
        {
            Value = value;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="value"></param>
        /// <param name="valueSource"></param>
        public ForEachStepData(string value, ValueSourceEnum valueSource)
        {
            ValueSource = valueSource;
            Value = value;
        }
    }
}
