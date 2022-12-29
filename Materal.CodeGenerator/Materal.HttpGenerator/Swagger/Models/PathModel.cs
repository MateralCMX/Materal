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
        /// 返回对象类型
        /// </summary>
        public string? ResponseType { get; private set; }
        /// <summary>
        /// 前缀返回对象类型
        /// </summary>
        public string? PrefixResponseType { get; private set; }
        /// <summary>
        /// 返回类型
        /// </summary>
        public string? ResultType { get; private set; }
        /// <summary>
        /// 前缀返回类型
        /// </summary>
        public string? PrefixResultType { get; private set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        public List<string> ParamTypes { get; } = new();
        /// <summary>
        /// 前缀参数类型
        /// </summary>
        public List<string> PrefixParamTypes { get; } = new();
        /// <summary>
        /// 参数代码
        /// </summary>
        public string? ParamsCode { get; private set; }
        /// <summary>
        /// 前缀参数代码
        /// </summary>
        public string? PrefixParamsCode { get; private set; }
        /// <summary>
        /// 执行方法
        /// </summary>
        public string? ExcuteFuncName { get; private set; }
        /// <summary>
        /// 前缀执行方法
        /// </summary>
        public string? PrefixExcuteFuncName { get; private set; }
        /// <summary>
        /// 发送参数代码
        /// </summary>
        public string? SendParamsCode { get; private set; }
        /// <summary>
        /// 前缀发送参数代码
        /// </summary>
        public string? PrefixSendParamsCode { get; private set; }
        public PathModel(JToken source, string url)
        {
            #region 解析Url
            {
                Url = url;
                string[] temp = url.Split("/");
                if (temp.Length < 2) throw new MateralException("解析Url失败");
                ControllerName = temp[^2];
                ActionName = temp[^1];
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
        public void Init(IReadOnlyCollection<SchemaModel>? schemas, string? prefixName)
        {
            if (string.IsNullOrWhiteSpace(Response)) return;
            #region 处理返回类型与执行方法
            if (Response.EndsWith("PageResultModel"))
            {
                ResponseType = ConvertKeyword(Response[0..^"PageResultModel".Length]);
                if (schemas != null && schemas.Any(m => m.PrefixName == prefixName + ResponseType))
                {
                    PrefixResponseType = prefixName + ResponseType;
                    ResultType = ResponseType;
                    PrefixResultType = prefixName + ResponseType;
                }
                else
                {
                    PrefixResponseType = ResponseType;
                    ResultType = ResponseType;
                    PrefixResultType = ResponseType;
                }
                ExcuteFuncName = $"await GetPageResultModelBy{HttpMethod}Async<{ResultType}>";
                PrefixExcuteFuncName = $"await GetPageResultModelBy{HttpMethod}Async<{PrefixResultType}>";
                ResultType = $"async Task<(List<{ResultType}> data, PageModel pageInfo)>";
                PrefixResultType = $"async Task<(List<{PrefixResultType}> data, PageModel pageInfo)>";
            }
            else if (Response.EndsWith("ListResultModel"))
            {
                ResponseType = ConvertKeyword(Response[0..^"ListResultModel".Length]);
                if (schemas != null && schemas.Any(m => m.PrefixName == prefixName + ResponseType))
                {
                    PrefixResponseType = prefixName + ResponseType;
                    ResultType = $"List<{ResponseType}>";
                    PrefixResultType = $"List<{prefixName}{ResponseType}>";
                }
                else
                {
                    PrefixResponseType = ResponseType;
                    ResultType = $"List<{ResponseType}>";
                    PrefixResultType = $"List<{ResponseType}>";
                }
                ExcuteFuncName = $"await GetResultModelBy{HttpMethod}Async<{ResultType}>";
                PrefixExcuteFuncName = $"await GetResultModelBy{HttpMethod}Async<{PrefixResultType}>";
                ResultType = $"async Task<{ResultType}>";
                PrefixResultType = $"async Task<{PrefixResultType}>";
            }
            else if (Response == "ResultModel")
            {
                ResultType = "async Task";
                PrefixResultType = ResultType;
                ExcuteFuncName = $"await GetResultModelBy{HttpMethod}Async";
                PrefixExcuteFuncName = ExcuteFuncName;
            }
            else if (Response.EndsWith("ResultModel"))
            {
                ResponseType = ConvertKeyword(Response[0..^"ResultModel".Length]);
                if (schemas != null && schemas.Any(m => m.PrefixName == prefixName + ResponseType))
                {
                    PrefixResponseType = prefixName + ResponseType;
                    ResultType = ResponseType;
                    PrefixResultType = prefixName + ResultType;
                }
                else
                {
                    PrefixResponseType = ResponseType;
                    ResultType = ResponseType;
                    PrefixResultType = ResponseType;
                }
                ExcuteFuncName = $"await GetResultModelBy{HttpMethod}Async<{ResultType}>";
                PrefixExcuteFuncName = $"await GetResultModelBy{HttpMethod}Async<{PrefixResultType}>";
                ResultType = $"async Task<{ResultType}>";
                PrefixResultType = $"async Task<{PrefixResultType}>";
            }
            else
            {
                ResponseType = $"async Task<{Response}>";
                if (schemas != null && schemas.Any(m => m.PrefixName == prefixName + Response))
                {
                    ResultType = $"async Task<{Response}>";
                    PrefixResultType = $"async Task<{prefixName}{Response}>";
                }
                else
                {
                    ResultType = ResponseType;
                    PrefixResultType = ResultType;
                }
                ExcuteFuncName = $"await Send{HttpMethod}Async<{ResultType}>";
                PrefixExcuteFuncName = $"await Send{HttpMethod}Async<{PrefixResultType}>";
            }
            #endregion
            #region 处理参数代码
            List<string> paramsCodes = new();
            List<string> prefixParamsCodes = new();
            List<string> sendParamsCodes = new()
            {
                $"\"{ControllerName}/{ActionName}\""
            };
            List<string> prefixSendParamsCodes = new()
            {
                $"\"{ControllerName}/{ActionName}\""
            };
            #region Body参数
            if (!string.IsNullOrWhiteSpace(BodyParam))
            {
                ParamTypes.Add(BodyParam);
                paramsCodes.Add($"{BodyParam} requestModel");
                string prefixParamName = $"{prefixName}{BodyParam}";
                if (schemas != null && schemas.Any(m => m.PrefixName == prefixParamName))
                {
                    PrefixParamTypes.Add(prefixParamName);
                    prefixParamsCodes.Add($"{prefixParamName} requestModel");
                }
                else
                {
                    PrefixParamTypes.Add(BodyParam);
                    prefixParamsCodes.Add($"{BodyParam} requestModel");
                }
                sendParamsCodes.Add("requestModel");
                prefixSendParamsCodes.Add("requestModel");
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
            PrefixParamsCode = string.Join(", ", prefixParamsCodes);
            SendParamsCode = string.Join(", ", sendParamsCodes);
            PrefixSendParamsCode = string.Join(", ", prefixSendParamsCodes);
            #endregion
        }
        /// <summary>
        /// 获得代码
        /// </summary>
        /// <returns></returns>
        public string? GetCode()
        {
            if (string.IsNullOrWhiteSpace(ActionName) || ControllerName == ActionName) return null;
            if (string.IsNullOrWhiteSpace(PrefixResultType) || string.IsNullOrWhiteSpace(PrefixExcuteFuncName) || string.IsNullOrWhiteSpace(PrefixSendParamsCode)) return null;
            StringBuilder codeContent = new();
            if (Description != null)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {Description}");
                codeContent.AppendLine($"        /// </summary>");
            }
            codeContent.AppendLine($"        public {PrefixResultType} {ActionName}Async({PrefixParamsCode}) => {PrefixExcuteFuncName}({PrefixSendParamsCode});");
            string code = codeContent.ToString();
            return code;
        }
        #region 私有方法
        /// <summary>
        /// 转换关键字
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        private static string ConvertKeyword(string inputStr)
        {
            inputStr = inputStr.Replace("String", "string");
            return inputStr;
        }
        #endregion
    }
}
