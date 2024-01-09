using Materal.Utils.Model;
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
        /// <summary>
        /// 验证特性组
        /// </summary>
        public List<AttributeModel> VerificationAttributes { get; set; } = [];
        /// <summary>
        /// 查询特性组
        /// </summary>
        public List<AttributeModel> QueryAttributes { get; set; } = [];
        /// <summary>
        /// 获得验证特性代码
        /// </summary>
        /// <returns></returns>
        public string? GetVerificationAttributesCode()
        {
            if (VerificationAttributes.Count <= 0)
            {
                string[] validationWhiteList =
                [
                    nameof(RequiredAttribute).RemoveAttributeSuffix(),
                    nameof(MinLengthAttribute).RemoveAttributeSuffix(),
                    nameof(MaxLengthAttribute).RemoveAttributeSuffix(),
                    nameof(StringLengthAttribute).RemoveAttributeSuffix()
                ];
                foreach (AttributeModel attribute in Attributes)
                {
                    if (!validationWhiteList.Contains(attribute.Name)) continue;
                    VerificationAttributes.Add(attribute);
                }
            }
            return VerificationAttributes.GetCode();
        }
        /// <summary>
        /// 获得查询特性代码
        /// </summary>
        /// <returns></returns>
        public string? GetQueryAttributesCode()
        {
            if (QueryAttributes.Count <= 0)
            {
                string[] queryWhiteList =
                [
                    nameof(EqualAttribute).RemoveAttributeSuffix(),
                    nameof(NotEqualAttribute).RemoveAttributeSuffix(),
                    nameof(ContainsAttribute).RemoveAttributeSuffix(),
                    nameof(GreaterThanAttribute).RemoveAttributeSuffix(),
                    nameof(GreaterThanOrEqualAttribute).RemoveAttributeSuffix(),
                    nameof(LessThanAttribute).RemoveAttributeSuffix(),
                    nameof(LessThanOrEqualAttribute).RemoveAttributeSuffix(),
                    nameof(StartContainsAttribute).RemoveAttributeSuffix()
                ];
                foreach (AttributeModel attribute in Attributes)
                {
                    if (!queryWhiteList.Contains(attribute.Name)) continue;
                    QueryAttributes.Add(attribute);
                }
            }
            return QueryAttributes.GetCode();
        }
    }
}
