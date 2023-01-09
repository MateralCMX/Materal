using Materal.CodeGenerator;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MateralVSHelper.CodeGenerator
{
    public class DomainPropertyModel
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public string Initializer { get; } = null;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 类型
        /// </summary>
        public string PredefinedType { get; }
        /// <summary>
        /// 可空类型
        /// </summary>
        public string NullPredefinedType { get; }
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool CanNull { get; } = false;
        /// <summary>
        /// 类型
        /// </summary>
        public string Type => CanNull ? NullPredefinedType : PredefinedType;
        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; } = new List<AttributeModel>();
        /// <summary>
        /// 生成实体配置
        /// </summary>
        public bool GeneratorEntityConfig { get; }
        /// <summary>
        /// 生成数据传输模型
        /// </summary>
        public bool GeneratorDTO { get; }
        /// <summary>
        /// 生成列表数据传输模型
        /// </summary>
        public bool GeneratorListDTO { get; }
        /// <summary>
        /// 生成添加模型
        /// </summary>
        public bool GeneratorAddModel { get; }
        /// <summary>
        /// 生成修改模型
        /// </summary>
        public bool GeneratorEditModel { get; }
        /// <summary>
        /// 有验证特性
        /// </summary>
        public bool HasValidationAttribute => ValidationAttributes.Count > 0;
        /// <summary>
        /// 验证特性
        /// </summary>
        public List<AttributeModel> ValidationAttributes { get; } = new List<AttributeModel>();
        /// <summary>
        /// 有查询特性
        /// </summary>
        public bool HasQueryAttribute => QueryAttributes.Count > 0;
        /// <summary>
        /// 查询特性
        /// </summary>
        public List<AttributeModel> QueryAttributes { get; } = new List<AttributeModel>();
        public DomainPropertyModel(string[] codes, int classLineIndex)
        {
            string propertyCode = codes[classLineIndex].Trim();
            #region 解析类型
            propertyCode = propertyCode.Substring("public ".Length);
            int blankIndex = propertyCode.IndexOf(' ');
            PredefinedType = propertyCode.Substring(0, blankIndex);
            if (PredefinedType.EndsWith("?"))
            {
                CanNull = true;
                NullPredefinedType = PredefinedType;
                PredefinedType = PredefinedType.Substring(0, PredefinedType.Length - 1);
            }
            propertyCode = propertyCode.Substring(blankIndex + 1);
            #endregion
            #region 解析名称
            blankIndex = propertyCode.IndexOf(' ');
            Name = propertyCode.Substring(0, blankIndex);
            propertyCode = propertyCode.Substring(blankIndex + 1);
            #endregion
            #region 解析默认值
            if (propertyCode != "{ get; set ;}")
            {
                string[] tempInitializers = propertyCode.Split('=');
                if (tempInitializers.Length == 2)
                {
                    Initializer = $" = {tempInitializers[1].Trim()}";
                }
            }
            #endregion
            int startIndex = classLineIndex - 1;
            #region 解析特性
            do
            {
                if (startIndex < 0) break;
                string attributeCode = codes[startIndex].Trim();
                if (!attributeCode.StartsWith("[") || !attributeCode.EndsWith("]")) break;
                startIndex -= 1;
                List<string> attributeCodes = attributeCode.GetAttributeCodes();
                Attributes.AddRange(attributeCodes.Select(attributeName => new AttributeModel(attributeName.Trim())));
            } while (true);
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
                nameof(RequiredAttribute).RemoveAttributeSuffix(),
                nameof(MinLengthAttribute).RemoveAttributeSuffix(),
                nameof(MaxLengthAttribute).RemoveAttributeSuffix(),
                nameof(StringLengthAttribute).RemoveAttributeSuffix(),
                nameof(RequiredAttribute).RemoveAttributeSuffix(),
                nameof(RequiredAttribute).RemoveAttributeSuffix(),
                nameof(RequiredAttribute).RemoveAttributeSuffix(),
                nameof(RequiredAttribute).RemoveAttributeSuffix()
            };
            GeneratorEntityConfig = !Attributes.HasAttribute<NotEntityConfigGeneratorAttribute>();
            GeneratorDTO = !Attributes.HasAttribute<NotDTOGeneratorAttribute>();
            GeneratorListDTO = GeneratorDTO ? !Attributes.HasAttribute<NotListDTOGeneratorAttribute>() : false;
            GeneratorAddModel = !Attributes.HasAttribute<NotAddGeneratorAttribute>();
            GeneratorEditModel = !Attributes.HasAttribute<NotEditGeneratorAttribute>();
            foreach (AttributeModel attribute in Attributes)
            {
                if (validationWhiteList.Contains(attribute.Name))
                {
                    ValidationAttributes.Add(attribute);
                }
                if (queryWhiteList.Contains(attribute.Name))
                {
                    QueryAttributes.Add(attribute);
                }
            }
            #endregion
            #region 解析注释
            Annotation = codes.GetAnnotation(ref startIndex);
            #endregion
        }
    }
}
