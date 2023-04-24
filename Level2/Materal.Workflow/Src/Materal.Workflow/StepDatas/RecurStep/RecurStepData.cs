using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 重复节点数据
    /// </summary>
    public class RecurStepData : StepData, IStepData
    {
        /// <summary>
        /// 间隔时间
        /// </summary>
        [Required]
        public TimeSpan Interval { get; set; } = TimeSpan.Zero;
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
        public RecurStepData()
        {

        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="interval"></param>
        public RecurStepData(TimeSpan interval)
        {
            Interval = interval;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="leftValue"></param>
        /// <param name="leftValueSource"></param>
        /// <param name="comparisonType"></param>
        /// <param name="rightValue"></param>
        /// <param name="rightValueSource"></param>
        public RecurStepData(TimeSpan interval, object leftValue, ValueSourceEnum leftValueSource, ComparisonTypeEnum comparisonType, object rightValue, ValueSourceEnum rightValueSource) : this(interval)
        {
            Conditions.Add(new(leftValue, leftValueSource, comparisonType, rightValue, rightValueSource));
        }
    }
}
