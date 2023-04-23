using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 条件节点数据
    /// </summary>
    public class IfStepData : StepData, IStepData
    {
        /// <summary>
        /// 决策条件数据
        /// </summary>
        [Required, MinLength(1)]
        public List<DecisionConditionData> Conditions { get; set; } = new();
        /// <summary>
        /// 满足条件执行的节点数据
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
        public IfStepData()
        {

        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="leftValue"></param>
        /// <param name="leftValueSource"></param>
        /// <param name="comparisonType"></param>
        /// <param name="rightValue"></param>
        /// <param name="rightValueSource"></param>
        public IfStepData(object leftValue, ValueSourceEnum leftValueSource, ComparisonTypeEnum comparisonType, object rightValue, ValueSourceEnum rightValueSource)
        {
            Conditions.Add(new(leftValue, leftValueSource, comparisonType, rightValue, rightValueSource));
        }
    }
}
