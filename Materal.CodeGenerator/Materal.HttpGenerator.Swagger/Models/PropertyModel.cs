using Materal.Common;
using Newtonsoft.Json.Linq;

namespace Materal.HttpGenerator.Swagger.Models
{
    public class PropertyModel
    {
        /// <summary>
        /// 最大长度
        /// </summary>
        public int? MaxLength { get; set; }
        /// <summary>
        /// 最小长度
        /// </summary>
        public int? MinLength { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public string TrueType
        {
            get
            {
                string result = GetCSharpType(Type, Format, Nullable);
                if (result == "array")
                {
                    if (ItemType == null) throw new MateralException("未找到类型");
                    result = $"List<{ItemType}>";
                }
                if (Nullable)
                {
                    result += "?";
                }
                return result;
            }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string? Format { get; set; }
        /// <summary>
        /// 可为空
        /// </summary>
        public bool Nullable { get; set; } = false;
        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly { get; set; } = false;
        /// <summary>
        /// 子集类型
        /// </summary>
        public string? ItemType { get; set; }
        public static string GetCSharpType(string type, string? format, bool isNull = false)
        {
            string result = type;
            if (result == "integer")
            {
                result = format switch
                {
                    "int32" => "int",
                    "int64" => "long",
                    _ => result
                };
                if (isNull)
                {
                    result += "?";
                }
            }
            else if (result == "number")
            {
                result = format switch
                {
                    "float" => "int",
                    "double" => "decimal",
                    _ => result
                };
                if (isNull)
                {
                    result += "?";
                }
            }
            else if (result == "string")
            {
                result = format switch
                {
                    "uuid" => "Guid",
                    "date-time" => "DateTime",
                    _ => result
                };
            }
            else if (result == "boolean")
            {
                result = "bool";
            }
            return result;
        }
        public PropertyModel(JToken source)
        {
            foreach (JToken item in source)
            {
                if (item is not JProperty property) continue;
                this.SetDefaultProperty(property.Name, property.Value.ToString());
                if (property.Name == "$ref")
                {
                    Type = property.Value.ToString().HandlerRef();
                }
                else if (property.Name == "items")
                {
                    if (property.Value["$ref"] != null)
                    {
                        ItemType = property.Value["$ref"]?.ToString().HandlerRef();
                    }
                    else
                    {
                        string? type = property.Value["type"]?.ToString();
                        string? format = property.Value["format"]?.ToString();
                        string? nullable = property.Value["nullable"]?.ToString();
                        if (type != null && format != null)
                        {
                            bool isNull = false;
                            if(nullable != null)
                            {
                                isNull = Convert.ToBoolean(nullable);
                            }
                            ItemType = GetCSharpType(type, format, isNull);
                        }
                        else if (string.IsNullOrWhiteSpace(type)) throw new MateralException("未识别类型");
                        else if (string.IsNullOrWhiteSpace(format))
                        {
                            ItemType = type;
                        }
                    }
                }
            }
        }
    }
}
