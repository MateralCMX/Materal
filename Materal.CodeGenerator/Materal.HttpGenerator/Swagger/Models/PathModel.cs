using Materal.Common;
using Materal.NetworkHelper;
using Newtonsoft.Json.Linq;
using System.Text;

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
        /// <summary>
        /// 返回类型
        /// </summary>
        public string? ResultType { get; private set; }
        /// <summary>
        /// 参数代码
        /// </summary>
        public string? ParamsCode { get; private set; }
        /// <summary>
        /// 执行方法
        /// </summary>
        public string? ExcuteFuncName { get; private set; }
        /// <summary>
        /// 发送参数代码
        /// </summary>
        public string? SendParamsCode { get; private set; }
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
        public void Init(IReadOnlyCollection<SchemaModel>? schemas)
        {
            if (string.IsNullOrWhiteSpace(Response)) return;
            #region 处理返回类型与执行方法
            if (Response.EndsWith("PageResultModel"))
            {
                ResultType = ConvertKeyword(Response[0..^"PageResultModel".Length]);
                ExcuteFuncName = $"await GetPageResultModelBy{HttpMethod}Async<{ResultType}>";
                ResultType = $"async Task<(List<{ResultType}> data, PageModel pageInfo)>";
            }
            else if (Response.EndsWith("ListResultModel"))
            {
                ResultType = ConvertKeyword(Response[0..^"ListResultModel".Length]);
                ResultType = $"List<{ResultType}>";
                ExcuteFuncName = $"await GetResultModelBy{HttpMethod}Async<{ResultType}>";
                ResultType = $"async Task<{ResultType}>";
            }
            else if (Response == "ResultModel")
            {
                ResultType = "async Task";
                ExcuteFuncName = $"await GetResultModelBy{HttpMethod}Async";
            }
            else if (Response.EndsWith("ResultModel"))
            {
                ResultType = ConvertKeyword(Response[0..^"ResultModel".Length]);
                ExcuteFuncName = $"await GetResultModelBy{HttpMethod}Async<{ResultType}>";
                ResultType = $"async Task<{ResultType}>";
            }
            else
            {
                ResultType = $"async Task<{Response}>";
                ExcuteFuncName = $"await Send{HttpMethod}Async<{ResultType}>";
            }
            #endregion
            #region 处理参数代码
            List<string> paramsCodes = new();
            List<string> sendParamsCodes = new()
            {
                $"\"{ControllerName}/{ActionName}\""
            };
            #region Body参数
            if (!string.IsNullOrWhiteSpace(BodyParam))
            {
                //SchemaModel? targetSchema = schemas?.FirstOrDefault(m => m.Name == $"{BodyParam}");
                //if (targetSchema != null)
                //{
                //    paramsCodes.Add($"{BodyParam} requestModel");
                //}
                //else
                //{
                //    paramsCodes.Add($"{BodyParam} requestModel");
                //}
                paramsCodes.Add($"{BodyParam} requestModel");
                sendParamsCodes.Add("requestModel");
            }
            #endregion
            #region Query参数
            if (QueryParams != null)
            {
                List<string> queryArgs = new();
                foreach (QueryParamModel queryParam in QueryParams)
                {
                    paramsCodes.Add($"{queryParam.CSharpType} {queryParam.Name}");
                    if (queryParam.CSharpType.EndsWith("?"))
                    {
                        if (queryParam.CSharpType == "string?")
                        {
                            queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name} ?? string.Empty");
                        }
                        else
                        {
                            queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name}?.ToString() ?? string.Empty");
                        }
                    }
                    else
                    {
                        if (queryParam.CSharpType == "string")
                        {
                            queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name}");
                        }
                        else
                        {
                            queryArgs.Add($"[nameof({queryParam.Name})]={queryParam.Name}.ToString()");
                        }
                    }
                }
                if (queryArgs.Count > 0)
                {
                    sendParamsCodes.Add("new Dictionary<string, string>{" + string.Join(", ", queryArgs) + "}");
                }
            }
            #endregion
            ParamsCode = string.Join(", ", paramsCodes);
            SendParamsCode = string.Join(", ", sendParamsCodes);
            #endregion
        }
        /// <summary>
        /// 获得代码
        /// </summary>
        /// <returns></returns>
        public string? GetCode()
        {
            if (string.IsNullOrWhiteSpace(ActionName) || ControllerName == ActionName) return null;
            if (string.IsNullOrWhiteSpace(ResultType) || string.IsNullOrWhiteSpace(ActionName) || string.IsNullOrWhiteSpace(ExcuteFuncName) || string.IsNullOrWhiteSpace(SendParamsCode)) return null;
            StringBuilder codeContent = new();
            if (Description != null)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {Description}");
                codeContent.AppendLine($"        /// </summary>");
            }
            codeContent.AppendLine($"        public {ResultType} {ActionName}Async({ParamsCode}) => {ExcuteFuncName}({SendParamsCode});");
            string code = codeContent.ToString();
            return code;
        }
        #region 私有方法
        /// <summary>
        /// 转换关键字
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        private string ConvertKeyword(string inputStr)
        {
            inputStr = inputStr.Replace("String", "string");
            return inputStr;
        }
        #endregion
    }
}
