using Materal.ConvertHelper;
using Newtonsoft.Json.Linq;

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
        /// 格式
        /// </summary>
        public string Format { get; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public string CSharpType => Type.GetCSharpType(Format, IsNull);
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
        public int? MaxLength { get; set; }
        /// <summary>
        /// 最小长度
        /// </summary>
        public int? MinLength { get; set; }
        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string? DefaultValue { get; set; }
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
                if(Type == "array")
                {
                    JToken? arrayItems = source["items"];
                    if(arrayItems != null)
                    {
                        string? refFormat = arrayItems["$ref"]?.ToString().HandlerRef();
                        if(!string.IsNullOrWhiteSpace(refFormat))
                        {
                            Type = $"List<{refFormat}>";
                        }
                        else
                        {
                            string? arrayType = arrayItems["type"]?.ToString();
                            string arrayFormat = arrayItems["format"]?.ToString() ?? string.Empty;
                            if (arrayType != null)
                            {
                                arrayType = arrayType.GetCSharpType(arrayFormat);
                                Type = $"List<{arrayType}>";
                            }
                        }
                    }
                }
            }
            #endregion
            #region 处理默认值
            if (!IsNull)
            {
                if(refToken != null)
                {
                    DefaultValue = " = new();";
                }
                else
                {
                    DefaultValue = CSharpType switch
                    {
                        "string" => " = string.Empty;",
                        _ => string.Empty
                    };
                }
            }
            #endregion
        }
    }
}
