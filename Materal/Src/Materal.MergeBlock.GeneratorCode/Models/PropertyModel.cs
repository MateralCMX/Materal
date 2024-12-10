using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string PredefinedType { get; set; }
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool CanNull => PredefinedType.EndsWith('?');
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
        public string? Initializer { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; }
        /// <summary>
        /// 验证特性组
        /// </summary>
        public List<AttributeModel> VerificationAttributes => Attributes.Where(m => _validationAttributeWhiteList.Contains(m.Name)).ToList();
        /// <summary>
        /// 查询特性组
        /// </summary>
        public List<AttributeModel> QueryAttributes => Attributes.Where(m => _queryAttributeWhiteList.Contains(m.Name)).ToList();
        /// <summary>
        /// 是否有查询特性
        /// </summary>
        public bool HasQueryAttribute => QueryAttributes.Count > 0;
        /// <summary>
        /// 验证特性白名单
        /// </summary>
        private static readonly string[] _validationAttributeWhiteList =
        [
            nameof(RequiredAttribute).RemoveAttributeSuffix(),
            nameof(MinLengthAttribute).RemoveAttributeSuffix(),
            nameof(MaxLengthAttribute).RemoveAttributeSuffix(),
            nameof(StringLengthAttribute).RemoveAttributeSuffix()
        ];
        /// <summary>
        /// 查询特性白名单
        /// </summary>
        private static readonly string[] _queryAttributeWhiteList =
        [
                nameof(EqualAttribute).RemoveAttributeSuffix(),
                nameof(NotEqualAttribute).RemoveAttributeSuffix(),
                nameof(ContainsAttribute).RemoveAttributeSuffix(),
                nameof(GreaterThanAttribute).RemoveAttributeSuffix(),
                nameof(GreaterThanOrEqualAttribute).RemoveAttributeSuffix(),
                nameof(LessThanAttribute).RemoveAttributeSuffix(),
                nameof(LessThanOrEqualAttribute).RemoveAttributeSuffix(),
                nameof(StartContainsAttribute).RemoveAttributeSuffix(),
                nameof(BetweenAttribute).RemoveAttributeSuffix()
        ];
        /// <summary>
        /// 构造方法
        /// </summary>
        public PropertyModel()
        {
            Name = string.Empty;
            PredefinedType = string.Empty;
            Initializer = null;
            Annotation = null;
            Attributes = [];
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="node"></param>
        public PropertyModel(PropertyDeclarationSyntax node)
        {
            Name = GetName(node);
            PredefinedType = GetPredefinedType(node);
            Initializer = GetInitializer(node);
            Annotation = node.GetAnnotation();
            Attributes = node.GetAttributes();
        }
        /// <summary>
        /// 获得验证特性代码
        /// </summary>
        /// <returns></returns>
        public string? GetVerificationAttributesCode() => VerificationAttributes.GetCode();
        /// <summary>
        /// 获得查询特性代码
        /// </summary>
        /// <returns></returns>
        public string? GetQueryAttributesCode() => QueryAttributes.GetCode();
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetName(PropertyDeclarationSyntax node)
            => node.Identifier.Text;
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string GetPredefinedType(PropertyDeclarationSyntax node)
            => node.Type.ToString();
        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static string? GetInitializer(PropertyDeclarationSyntax node)
        {
            if (node.Initializer is null) return null;
            return node.Initializer.Value is LiteralExpressionSyntax literalExpression
                ? literalExpression.Token.ValueText
                : node.Initializer.Value.ToString();
        }
    }
}
