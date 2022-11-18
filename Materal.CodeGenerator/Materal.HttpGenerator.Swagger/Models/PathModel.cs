using Materal.NetworkHelper;
using Newtonsoft.Json.Linq;

namespace Materal.HttpGenerator.Swagger.Models
{
    public class PathModel
    {
        public HttpMethodType HttpType { get; set; } = HttpMethodType.Get;
        /// <summary>
        /// 方法名称
        /// </summary>
        public string? ActionName => Url?.Split('/').LastOrDefault();
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string? ControllerName => Url?.Split('/')[2];
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// 查询参数
        /// </summary>
        public List<QueryParamModel>? QueryParams { get; set; }
        /// <summary>
        /// Body参数
        /// </summary>
        public string? BodyParam { get; set; }
        /// <summary>
        /// 返回对象
        /// </summary>
        public string? Response { get;set; }
        public PathModel(JToken source, string url)
        {
            Url = url;
            foreach (JToken item in source)
            {
                if (item is not JProperty property) continue;
                HttpType = property.Name switch
                {
                    "get" => HttpMethodType.Get,
                    "post" => HttpMethodType.Post,
                    "put" => HttpMethodType.Put,
                    "patch" => HttpMethodType.Patch,
                    "delete" => HttpMethodType.Delete,
                    _ => throw new ApplicationException("未识别的Http方式"),
                };
                Handler(property.Value);
            }
        }
        private void Handler(JToken source)
        {
            foreach (JToken item in source)
            {
                if (item is not JProperty property) continue;
                switch (property.Name)
                {
                    case "summary":
                        Description = property.Value.ToString();
                        break;
                    case "parameters":
                        QueryParams = new List<QueryParamModel>();
                        foreach (JToken propertyItem in property.Value)
                        {
                            if (propertyItem is not JObject jobject) continue;
                            QueryParams.Add(new QueryParamModel(jobject));
                        }
                        break;
                    case "requestBody":
                        JToken? bodyParam = property.Value["content"]?["application/json"]?["schema"]?["$ref"];
                        if (bodyParam == null) break;
                        BodyParam = bodyParam.ToString().HandlerRef();
                        break;
                    case "responses":
                        JToken? responseParam = property.Value["200"]?["content"]?["application/json"]?["schema"]?["$ref"];
                        if (responseParam == null) break;
                        Response = responseParam.ToString().HandlerRef();
                        break;
                }
            }
        }
    }
}
