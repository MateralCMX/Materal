namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 输入数据
    /// </summary>
    public class InputData
    {
        /// <summary>
        /// 节点属性名称
        /// </summary>
        public string StepProperty { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 值来源
        /// </summary>
        public InputValueSourceEnum ValueSource { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public InputData() : this(string.Empty, new())
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="stepProperty"></param>
        /// <param name="value"></param>
        public InputData(string stepProperty, object value) : this(stepProperty, value, InputValueSourceEnum.Constant)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="stepProperty"></param>
        /// <param name="valueSource"></param>
        /// <param name="value"></param>
        public InputData(string stepProperty, object value, InputValueSourceEnum valueSource)
        {
            StepProperty = stepProperty;
            ValueSource = valueSource;
            Value = value;
        }
    }
}
