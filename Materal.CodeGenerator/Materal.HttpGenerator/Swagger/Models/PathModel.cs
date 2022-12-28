using Materal.Common;
using Materal.NetworkHelper;
using Newtonsoft.Json.Linq;

namespace Materal.HttpGenerator.Swagger.Models
{
    public class PathModel
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName { get; }
        /// <summary>
        /// Http
        /// </summary>
        public HttpMethodType HttpMethod { get; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; }
        /// <summary>
        /// 查询参数
        /// </summary>
        public List<QueryParamModel>? QueryParams { get; }
        /// <summary>
        /// Body参数
        /// </summary>
        public string? BodyParam { get; }
        /// <summary>
        /// 返回对象
        /// </summary>
        public string? Response { get; }
        public PathModel(JToken source, string url)
        {
            #region 解析Url
            {
                Url = url;
                string[] temp = url.Split("/");
                if (temp.Length < 2) throw new MateralException("解析Url失败");
                ControllerName = temp[temp.Length - 2];
                ActionName= temp[temp.Length - 1];
            }
            #endregion
            foreach (JToken item in source)
            {
                if (item is not JProperty property) continue;
                #region 解析HttpMethod
                HttpMethod = property.Name switch
                {
                    "get" => HttpMethodType.Get,
                    "post" => HttpMethodType.Post,
                    "put" => HttpMethodType.Put,
                    "patch" => HttpMethodType.Patch,
                    "delete" => HttpMethodType.Delete,
                    _ => throw new ApplicationException("未识别的Http方式"),
                };
                #endregion
                #region 解析其他属性
                {
                    foreach (JToken propertyValue in property.Value)
                    {
                        if (propertyValue is not JProperty subProperty) continue;
                        switch (subProperty.Name)
                        {
                            case "summary"://解析描述
                                Description = subProperty.Value.ToString();
                                break;
                            case "parameters"://解析查询参数
                                QueryParams = new List<QueryParamModel>();
                                foreach (JToken subPropertyItem in subProperty.Value)
                                {
                                    if (subPropertyItem is not JObject querySource) continue;
                                    QueryParams.Add(new QueryParamModel(querySource));
                                }
                                break;
                            case "requestBody"://解析Body参数
                                JToken? bodyParam = subProperty.Value["content"]?["application/json"]?["schema"]?["$ref"];
                                if (bodyParam == null) break;
                                BodyParam = bodyParam.ToString().HandlerRef();
                                break;
                            case "responses"://解析返回
                                JToken? responseParam = subProperty.Value["200"]?["content"]?["application/json"]?["schema"]?["$ref"];
                                if (responseParam == null) break;
                                Response = responseParam.ToString().HandlerRef();
                                break;
                        }
                    }
                }
                #endregion
                break;
            }
        }
    }
}
