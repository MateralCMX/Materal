namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 输出数据
    /// </summary>
    public class OutputData
    {
        /// <summary>
        /// 运行数据名称
        /// </summary>
        public string RuntimeDataName { get; set; }
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
        /// <param name="runtimeDataName"></param>
        /// <param name="stepProperty"></param>
        public OutputData(string runtimeDataName, string stepProperty)
        {
            RuntimeDataName = runtimeDataName;
            StepProperty = stepProperty;
        }
    }
}
