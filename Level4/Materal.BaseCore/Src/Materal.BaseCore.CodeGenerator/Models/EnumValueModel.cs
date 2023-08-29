namespace Materal.BaseCore.CodeGenerator.Models
{
    /// <summary>
    /// 枚举值模型
    /// </summary>
    public class EnumValueModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = new List<AttributeModel>();
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
    }
}
