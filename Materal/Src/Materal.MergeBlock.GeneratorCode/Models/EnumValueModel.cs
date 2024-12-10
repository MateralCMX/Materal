using Materal.MergeBlock.GeneratorCode.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Materal.MergeBlock.GeneratorCode.Models
{
    /// <summary>
    /// 枚举值模型
    /// </summary>
    public class EnumValueModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string? Value { get; set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public EnumValueModel()
        {
            Name = string.Empty;
            Value = null;
            Attributes = [];
            Annotation = null;
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        public EnumValueModel(EnumMemberDeclarationSyntax node)
        {
            Name = GetName(node);
            Value = GetValue(node);
            Attributes = node.GetAttributes();
            Annotation = node.GetAnnotation();
        }
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetName(EnumMemberDeclarationSyntax node)
            => node.Identifier.Text;
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string? GetValue(EnumMemberDeclarationSyntax node)
            => node.EqualsValue?.Value.ToString();
    }
}
