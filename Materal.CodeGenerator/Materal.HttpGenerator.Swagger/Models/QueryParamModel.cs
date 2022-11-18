using Materal.Common;
using Newtonsoft.Json.Linq;

namespace Materal.HttpGenerator.Swagger.Models
{
    /// <summary>
    /// 查询参数模型
    /// </summary>
    public class QueryParamModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 必填
        /// </summary>
        public bool Required { get; set; } = false;
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string TrueType
        {
            get
            {
                string result = PropertyModel.GetCSharpType(Type, Format, !Required);
                return result;
            }
        }
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public QueryParamModel(JObject source)
        {
            Name = source["name"]?.ToString() ?? string.Empty;
            Description = source["description"]?.ToString() ?? string.Empty;
            Required = source["required"] != null && Convert.ToBoolean(source["required"]);
            Type = source["schema"]?["type"]?.ToString() ?? string.Empty;
            Format = source["schema"]?["format"]?.ToString() ?? string.Empty;
        }
    }
}
