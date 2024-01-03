namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 属性模型
    /// </summary>
    public class PropertyModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public string PredefinedType { get; set; } = string.Empty;
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool CanNull { get; set; } = false;
        /// <summary>
        /// 可空类型
        /// </summary>
        public string NullPredefinedType => CanNull ? PredefinedType : $"{PredefinedType}?";
        /// <summary>
        /// 不可空类型
        /// </summary>
        public string NotNullPredefinedType => CanNull ? PredefinedType[..^1] : PredefinedType;
        /// <summary>
        /// 默认值
        /// </summary>
        public string? Initializer { get; set; } = null;
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = [];
    }
}
