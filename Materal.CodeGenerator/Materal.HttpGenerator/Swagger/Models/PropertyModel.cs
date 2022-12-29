using Materal.ConvertHelper;
using Newtonsoft.Json.Linq;
using System.Text;

namespace Materal.HttpGenerator.Swagger.Models
{
    public class PropertyModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; } = string.Empty;
        /// <summary>
        /// 前缀类型
        /// </summary>
        public string PrefixType { get; set; } = string.Empty;
        /// <summary>
        /// 是列表
        /// </summary>
        public bool IsList { get; } = false;
        /// <summary>
        /// 真实类型
        /// </summary>
        public string TrueType => IsList ? $"List<{PrefixType}>" : PrefixType;
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public string CSharpType => TrueType.GetCSharpType(Format, IsNull);
        /// <summary>
        /// 说明
        /// </summary>
        public string? Description { get; }
        /// <summary>
        /// 可空
        /// </summary>
        public bool IsNull { get; } = true;
        /// <summary>
        /// 最大长度
        /// </summary>
        public int? MaxLength { get; }
        /// <summary>
        /// 最小长度
        /// </summary>
        public int? MinLength { get; }
        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly { get; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string? DefaultValueCode { get; private set; }
        /// <summary>
        /// 特性值
        /// </summary>
        public string? AttributeCode { get; }
        public PropertyModel(string name, JObject source, List<string>? requireds)
        {
            Name = name;
            #region 处理可空
            if (source["nullable"] == null)
            {
                if (requireds != null && requireds.Count > 0)
                {
                    IsNull = !requireds.Contains(name);
                }
            }
            else
            {
                IsNull = Convert.ToBoolean(source["nullable"]);
            }
            #endregion
            #region 处理基础信息
            Format = source["format"]?.ToString() ?? string.Empty;
            Description = source["description"]?.ToString() ?? string.Empty;
            ReadOnly = source["readOnly"] != null && Convert.ToBoolean(source["readOnly"]);
            MaxLength = source["maxLength"]?.ConvertTo<int>();
            MinLength = source["minLength"]?.ConvertTo<int>();
            #endregion
            #region 处理$ref
            JToken? refToken = source["$ref"];
            if (refToken != null)
            {
                Type = refToken.ToString().HandlerRef();
            }
            else
            {
                Type = source["type"]?.ToString() ?? string.Empty;
                if (Type == "array")
                {
                    JToken? arrayItems = source["items"];
                    if (arrayItems != null)
                    {
                        string? refFormat = arrayItems["$ref"]?.ToString().HandlerRef();
                        if (!string.IsNullOrWhiteSpace(refFormat))
                        {
                            Type = refFormat;
                            IsList = true;
                        }
                        else
                        {
                            string? arrayType = arrayItems["type"]?.ToString();
                            string arrayFormat = arrayItems["format"]?.ToString() ?? string.Empty;
                            if (arrayType != null)
                            {
                                arrayType = arrayType.GetCSharpType(arrayFormat);
                                Type = arrayType;
                                IsList = true;
                            }
                        }
                    }
                }
            }
            #endregion
            #region 处理默认值
            if (!IsNull)
            {
                if (refToken != null)
                {
                    DefaultValueCode = " = new();";
                }
            }
            #endregion
            #region 处理特性
            AttributeCode = string.Empty;
            if (!IsNull)
            {
                AttributeCode += "Required";
            }
            if (MaxLength != null)
            {
                if (!IsNull)
                {
                    AttributeCode += ", ";
                }
                AttributeCode += "StringLength(100";
                if (MinLength != null)
                {
                    AttributeCode += ", MinimumLength = 0";
                }
                AttributeCode += ")";
            }
            if (string.IsNullOrWhiteSpace(AttributeCode))
            {
                AttributeCode = null;
            }
            else
            {
                AttributeCode = $"[{AttributeCode}]";
            }
            #endregion
        }
        public void Init(IReadOnlyCollection<SchemaModel>? schemas, string? prefixName)
        {
            if (schemas != null && schemas.Any(m => m.PrefixName == prefixName + Type && !m.IsEnum))
            {
                PrefixType = prefixName + Type;
            }
            else
            {
                PrefixType = Type;
            }
            if(!IsNull && string.IsNullOrWhiteSpace(DefaultValueCode))
            {
                DefaultValueCode = CSharpType switch
                {
                    "string" => " = string.Empty;",
                    _ => string.Empty
                };
            }
        }
        /// <summary>
        /// 获得代码
        /// </summary>
        /// <param name="generatorBuild"></param>
        /// <returns></returns>
        public string GetCode(GeneratorBuildImpl generatorBuild)
        {
            StringBuilder codeContent = new();
            SchemaModel? targetEnumSchema = null;
            if (generatorBuild.SwaggerContent != null && generatorBuild.SwaggerContent.Schemas != null)
            {
                targetEnumSchema = generatorBuild.SwaggerContent.Schemas.FirstOrDefault(m => m.Name == TrueType && m.IsEnum);
            }
            #region 拼装注释
            if (targetEnumSchema != null)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {targetEnumSchema.Description}");
                codeContent.AppendLine($"        /// </summary>");
            }
            else if (!string.IsNullOrWhiteSpace(Description))
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {Description}");
                codeContent.AppendLine($"        /// </summary>");
            }
            #endregion
            #region 拼装特性
            if (!string.IsNullOrWhiteSpace(AttributeCode))
            {
                codeContent.AppendLine($"        {AttributeCode}");
            }
            #endregion
            #region 拼装主体
            if (targetEnumSchema != null)
            {
                codeContent.AppendLine($"        public {targetEnumSchema.Type.GetCSharpType(targetEnumSchema.Format, IsNull)} {Name} {{ get; set; }}");
            }
            else
            {
                codeContent.AppendLine($"        public {CSharpType} {Name} {{ get; set; }}{DefaultValueCode}");
            }
            #endregion
            string code = codeContent.ToString();
            return code;
        }
    }
}
