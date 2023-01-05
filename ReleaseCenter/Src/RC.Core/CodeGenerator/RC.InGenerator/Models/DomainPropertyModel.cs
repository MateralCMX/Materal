using Microsoft.CodeAnalysis.CSharp.Syntax;
using RC.InGenerator.Attribtues;
using RC.InGenerator.Utils;
using System.Collections.Generic;
using System.Linq;

namespace RC.InGenerator.Models
{
    public class DomainPropertyModel
    {
        /// <summary>
        /// 修饰符
        /// </summary>
        public string Modifier { get; private set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string? Initializer { get; private set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string PredefinedType { get; private set; }
        /// <summary>
        /// 可空类型
        /// </summary>
        public string NullPredefinedType { get; private set; }
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool CanNull { get; private set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; private set; }
        /// <summary>
        /// 查询特性
        /// </summary>
        public string? QueryAttribute { get; private set; }
        /// <summary>
        /// 验证特性
        /// </summary>
        public string? ValidationAttribute { get; private set; }
        /// <summary>
        /// 放入添加模型
        /// </summary>
        public bool CanAdd { get; private set; }
        /// <summary>
        /// 使用查询
        /// </summary>
        public bool UseQuery => QueryAttribute != null;
        /// <summary>
        /// 使用验证
        /// </summary>
        public bool UseValidation => ValidationAttribute != null;
        /// <summary>
        /// 生成修改模型
        /// </summary>
        public bool CanEdit { get; private set; }
        /// <summary>
        /// 之间
        /// </summary>
        public bool IsBetween { get; private set; }
        /// <summary>
        /// 生成列表数据传输模型
        /// </summary>
        public bool GeneratorListDTO { get; private set; }
        /// <summary>
        /// 生成数据传输模型
        /// </summary>
        public bool GeneratorDTO { get; private set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; private set; }
        public DomainPropertyModel(PropertyDeclarationSyntax propertyDeclaration)
        {
            Name = propertyDeclaration.Identifier.ValueText;
            Modifier = propertyDeclaration.Modifiers.First().ToString();
            PredefinedType = propertyDeclaration.Type.ToString();
            CanNull = PredefinedType.EndsWith("?");
            NullPredefinedType = PredefinedType + (CanNull ? "" : "?");
            if (propertyDeclaration.Initializer != null)
            {
                Initializer = propertyDeclaration.Initializer.ToString() + ";";
            }
            Annotation = propertyDeclaration.GetAnnotation();
            Attributes = InitAttributes(propertyDeclaration);
            CanAdd = !Attributes.HasAttribute<NotAddGeneratorAttribute>();
            CanEdit = !Attributes.HasAttribute<NotEditGeneratorAttribute>();
            GeneratorDTO = !Attributes.HasAttribute<NotDTOGeneratorAttribute>();
            if (!GeneratorDTO)
            {
                GeneratorListDTO = false;
            }
            else
            {
                GeneratorListDTO = !Attributes.HasAttribute<NotListDTOGeneratorAttribute>();
            }
        }
        /// <summary>
        /// 初始化查询特性
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        private List<AttributeModel> InitAttributes(PropertyDeclarationSyntax propertyDeclaration)
        {
            List<AttributeModel> result = new();
            QueryAttribute = null;
            if (propertyDeclaration.AttributeLists.Count <= 0) return result;
            string[] queryWhiteList = new[]
            {
                "EqualAttribute".RemoveAttributeSuffix(),
                "NotEqualAttribute".RemoveAttributeSuffix(),
                "ContainsAttribute".RemoveAttributeSuffix(),
                "GreaterThanAttribute".RemoveAttributeSuffix(),
                "GreaterThanOrEqualAttribute".RemoveAttributeSuffix(),
                "LessThanAttribute".RemoveAttributeSuffix(),
                "LessThanOrEqualAttribute".RemoveAttributeSuffix(),
                "StartContainsAttribute".RemoveAttributeSuffix()
            };
            string[] validationWhiteList = new[]
            {
                "RequiredAttribute".RemoveAttributeSuffix(),
                "MinLengthAttribute".RemoveAttributeSuffix(),
                "MaxLengthAttribute".RemoveAttributeSuffix(),
                "StringLengthAttribute".RemoveAttributeSuffix(),
                "MinAttribute".RemoveAttributeSuffix(),
                "MaxAttribute".RemoveAttributeSuffix()
            };
            foreach (AttributeListSyntax attributeListSyntax in propertyDeclaration.AttributeLists)
            {
                if (attributeListSyntax.Attributes.Count <= 0) continue;
                foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                {
                    if (string.IsNullOrWhiteSpace(QueryAttribute) && queryWhiteList.Contains(attributeSyntax.Name.ToString()))
                    {
                        QueryAttribute = attributeListSyntax.ToString();
                    }
                    if (string.IsNullOrWhiteSpace(ValidationAttribute) && validationWhiteList.Contains(attributeSyntax.Name.ToString()))
                    {
                        ValidationAttribute = attributeListSyntax.ToString();
                    }
                    if (attributeSyntax.Name.ToString() == nameof(BetweenAttribute).RemoveAttributeSuffix())
                    {
                        IsBetween = true;
                    }
                    result.Add(new AttributeModel(attributeSyntax));
                }
            }
            return result;
        }
    }
}
