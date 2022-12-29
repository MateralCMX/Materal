using Newtonsoft.Json.Linq;

namespace Materal.HttpGenerator.Swagger.Models
{
    public class SwaggerContentModel
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string? OpenAPI { get; }
        /// <summary>
        /// 地址组
        /// </summary>
        public List<PathModel>? Paths { get; }
        /// <summary>
        /// 组件
        /// </summary>
        public List<SchemaModel>? Schemas { get; }
        public SwaggerContentModel(JObject source)
        {
            string[] schemasBlackList = new[]
            {
                "PageModel"
            };
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
                            if (path == null || path is not JProperty pathItem) continue;
                            Paths.Add(new(pathItem.Value, pathItem.Name));
                        }
                        break;
                    case "components":
                        JToken? schemas = item.Value["schemas"];
                        if (schemas == null) continue;
                        Schemas = new();
                        foreach (JToken schema in schemas)
                        {
                            if (schema is not JProperty schemaProperty) continue;
                            if (schemaProperty.Name.EndsWith("ResultModel")) continue;
                            if (schemasBlackList.Contains(schemaProperty.Name)) continue;
                            Schemas.Add(new(schemaProperty));
                        }
                        break;
                }
            }
        }
    }
}
