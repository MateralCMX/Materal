using Newtonsoft.Json.Linq;

namespace Materal.HttpGenerator.Swagger.Models
{
    public class SwaggerContentModel
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string? OpenAPI { get; set; }
        /// <summary>
        /// 地址组
        /// </summary>
        public List<PathModel>? Paths { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        public ComponentModel? Components { get; set; }
        public SwaggerContentModel(JObject source)
        {
            foreach (KeyValuePair<string, JToken?> item in source)
            {
                if (item.Value == null) continue;
                switch (item.Key)
                {
                    case "openapi":
                        OpenAPI = item.Value.ToString();
                        break;
                    case "paths":
                        Paths = new();
                        foreach (JToken? path in item.Value)
                        {
                            if (path == null || path is not JProperty property) continue;
                            Paths.Add(new PathModel(property.Value, property.Name));
                        }
                        break;
                    case "components":
                        Components = new ComponentModel(item.Value);
                        break;
                }
            }
        }
    }
}
