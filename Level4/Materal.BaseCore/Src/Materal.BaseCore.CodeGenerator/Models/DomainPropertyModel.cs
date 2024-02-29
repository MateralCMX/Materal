using Materal.BaseCore.CodeGenerator.Extensions;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class DomainPropertyModel
    {
        /// <summary>
        /// 默认值
        /// </summary>
        public string? Initializer { get; set; } = null;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public string PredefinedType { get; set; } = string.Empty;
        /// <summary>
        /// 可空类型
        /// </summary>
        public string NullPredefinedType => CanNull ? PredefinedType : $"{PredefinedType}?";
        /// <summary>
        /// 不可空类型
        /// </summary>
        public string NotNullPredefinedType => CanNull ? PredefinedType[..^1] : PredefinedType;
        /// <summary>
        /// 是否可空
        /// </summary>
        public bool CanNull { get; set; } = false;
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = new List<AttributeModel>();
        /// <summary>
        /// 有验证特性
        /// </summary>
        public bool HasValidationAttribute => ValidationAttributes.Count > 0;
        /// <summary>
        /// 验证特性
        /// </summary>
        public List<AttributeModel> ValidationAttributes { get; set; } = new List<AttributeModel>();
        /// <summary>
        /// 有查询特性
        /// </summary>
        public bool HasQueryAttribute => QueryAttributes.Count > 0;
        /// <summary>
        /// 查询特性
        /// </summary>
        public List<AttributeModel> QueryAttributes { get; set; } = new List<AttributeModel>();
        /// <summary>
        /// 是之间
        /// </summary>
        public bool IsBetween { get; set; }
        /// <summary>
        /// 是包含
        /// </summary>
        public bool IsContains { get; set; }
        /// <summary>
        /// 是字符串包含
        /// </summary>
        public bool IsStringContains { get; set; }
        /// <summary>
        /// 生成实体配置
        /// </summary>
        public bool GeneratorEntityConfig { get; set; }
        /// <summary>
        /// 生成数据传输模型
        /// </summary>
        public bool GeneratorDTO { get; set; }
        /// <summary>
        /// 生成列表数据传输模型
        /// </summary>
        public bool GeneratorListDTO { get; set; }
        /// <summary>
        /// 生成添加模型
        /// </summary>
        public bool GeneratorAddModel { get; set; }
        /// <summary>
        /// 生成修改模型
        /// </summary>
        public bool GeneratorEditModel { get; set; }
        /// <summary>
        /// 是位序分组属性
        /// </summary>
        public bool IsIndexGourpProperty { get; set; }
        /// <summary>
        /// 是树分组属性
        /// </summary>
        public bool IsTreeGourpProperty { get; set; }
        public DomainPropertyModel() { }
        public DomainPropertyModel(string[] codes, int classLineIndex)
        {
            string propertyCode = codes[classLineIndex].Trim();
            #region 解析类型
            propertyCode = propertyCode["public ".Length..];
            int blankIndex = propertyCode.IndexOf(' ');
            PredefinedType = propertyCode[..blankIndex];
            CanNull = PredefinedType.EndsWith("?");
            propertyCode = propertyCode[(blankIndex + 1)..];
            #endregion
            #region 解析名称
            blankIndex = propertyCode.IndexOf(' ');
            Name = propertyCode[..blankIndex];
            propertyCode = propertyCode[(blankIndex + 1)..];
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
            GeneratorListDTO = GeneratorDTO && !Attributes.HasAttribute<NotListDTOGeneratorAttribute>();
            GeneratorAddModel = !Attributes.HasAttribute<NotAddGeneratorAttribute>();
            GeneratorEditModel = !Attributes.HasAttribute<NotEditGeneratorAttribute>();
            IsBetween = Attributes.HasAttribute<BetweenAttribute>();
            IsIndexGourpProperty = Attributes.HasAttribute<IndexGroupAttribute>();
            IsTreeGourpProperty = Attributes.HasAttribute<TreeGroupAttribute>();
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
            IsContains = QueryAttributes.Count == 1 && QueryAttributes[0].Name == "Contains";
            IsStringContains = IsContains && NullPredefinedType == "string?";
            #endregion
            #region 解析注释
            Annotation = codes.GetAnnotation(ref startIndex);
            #endregion
        }
    }
}
