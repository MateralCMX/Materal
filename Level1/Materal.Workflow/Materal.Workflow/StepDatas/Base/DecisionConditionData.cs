namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 决策条件数据
    /// </summary>
    public class DecisionConditionData
    {
        /// <summary>
        /// 左边值
        /// </summary>
        public object LeftValue { get; set; } = string.Empty;
        /// <summary>
        /// 左边值来源
        /// </summary>
        public ValueSourceEnum LeftValueSource { get; set; }
        /// <summary>
        /// 比较类型
        /// </summary>
        public ComparisonTypeEnum ComparisonType { get; set; }
        /// <summary>
        /// 右边值
        /// </summary>
        public object RightValue { get; set; } = string.Empty;
        /// <summary>
        /// 右边值来源
        /// </summary>
        public ValueSourceEnum RightValueSource { get; set; }
        /// <summary>
        /// 条件类型
        /// </summary>
        public ConditionEnum Condition { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public DecisionConditionData()
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
        /// <param name="condition"></param>
        public DecisionConditionData(object leftValue, ValueSourceEnum leftValueSource, ComparisonTypeEnum comparisonType, object rightValue, ValueSourceEnum rightValueSource, ConditionEnum condition = ConditionEnum.And)
        {
            LeftValue = leftValue;
            LeftValueSource = leftValueSource;
            ComparisonType = comparisonType;
            RightValue = rightValue;
            RightValueSource = rightValueSource;
            Condition = condition;
        }
    }
}
