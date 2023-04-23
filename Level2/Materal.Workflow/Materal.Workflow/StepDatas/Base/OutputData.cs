namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 输出数据
    /// </summary>
    public class OutputData
    {
        /// <summary>
        /// 运行数据属性
        /// </summary>
        public string RuntimeDataProperty { get; set; }
        /// <summary>
        /// 值属性名称
        /// </summary>
        public string StepProperty { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public OutputData() : this(string.Empty, string.Empty)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="runtimeDataProperty"></param>
        /// <param name="stepProperty"></param>
        public OutputData(string runtimeDataProperty, string stepProperty)
        {
            RuntimeDataProperty = runtimeDataProperty;
            StepProperty = stepProperty;
        }
    }
}
