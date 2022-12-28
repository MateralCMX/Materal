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
        public string Name { get; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// 可空
        /// </summary>
        public bool IsNull { get; } = true;
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; }
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string CSharpType => Type.GetCSharpType(Format, IsNull);
        public QueryParamModel(JObject source)
        {
            Name = source["name"]?.ToString() ?? string.Empty;
            Description = source["description"]?.ToString() ?? string.Empty;
            IsNull = source["required"] == null || !Convert.ToBoolean(source["required"]);
            Type = source["schema"]?["type"]?.ToString() ?? string.Empty;
            Format = source["schema"]?["format"]?.ToString() ?? string.Empty;
        }
    }
}
