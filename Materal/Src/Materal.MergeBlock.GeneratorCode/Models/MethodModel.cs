namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 方法模型
    /// </summary>
    public class MethodModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 返回类型
        /// </summary>
        public string ReturnType { get; set; } = string.Empty;
        /// <summary>
        /// 是否Task返回类型
        /// </summary>
        public bool IsTaskReturnType => ReturnType.StartsWith("Task");
        /// <summary>
        /// 无Task返回类型
        /// </summary>
        public string NotTaskReturnType { get; set; } = string.Empty;
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public List<MethodArgumentModel> Arguments { get; set; } = [];
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = [];
    }
}
