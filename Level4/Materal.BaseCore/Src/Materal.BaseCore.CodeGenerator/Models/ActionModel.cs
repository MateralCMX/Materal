using Materal.BaseCore.CodeGenerator.Extensions;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class ActionModel
    {
        /// <summary>
        /// 生成代码
        /// </summary>
        public bool GeneratorCode { get; set; } = true;
        /// <summary>
        /// 注释
        /// </summary>
        public List<string> Annotations { get; set; } = new();
        /// <summary>
        /// Http类型
        /// </summary>
        public string HttpMethod { get; set; } = string.Empty;
        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; set; } = string.Empty;
        /// <summary>
        /// 返回类型
        /// </summary>
        public string ResultType { get; set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Body参数
        /// </summary>
        public string? BodyParams { get; set; } = null;
        /// <summary>
        /// Query参数
        /// </summary>
        public List<string> QueryParams { get; set; } = new List<string>();
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = new();

        public ActionModel()
        {
        }
        public ActionModel(string[] codes, int startIndex)
        {
            string actionCode = codes[startIndex--].Trim();
            int bodyIndex = actionCode.IndexOf("=>");
            if (bodyIndex >= 0)
            {
                actionCode = actionCode[..bodyIndex].Trim();
            }
            string? methodType;
            int bracketStartIndex = actionCode.IndexOf("([");
            if (bracketStartIndex < 0)
            {
                bracketStartIndex = actionCode.LastIndexOf("(");
            }
            #region 解析特性
            {
                int tempIndex = startIndex;
                do
                {
                    if (tempIndex < 0) break;
                    string attributeCode = codes[tempIndex].Trim();
                    if (!attributeCode.StartsWith("[") || !attributeCode.EndsWith("]")) break;
                    tempIndex -= 1;
                    List<string> attributeCodes = attributeCode.GetAttributeCodes();
                    Attributes.AddRange(attributeCodes.Select(attributeName => new AttributeModel(attributeName.Trim())));
                } while (true);
                GeneratorCode = !Attributes.HasAttribute<NotGeneratorAttribute>();
            }
            #endregion
            #region 解析方法
            {
                string[] tempFrontCodes = actionCode[..bracketStartIndex].Split(' ');
                List<string> frontCodes = tempFrontCodes.AssemblyFullCode(" ");
                string code = frontCodes[^2].Trim();
                if (code.StartsWith("Task<PageResultModel<") || code.StartsWith("PageResultModel<"))
                {
                    string type = GetType(code);
                    ResultType = $"Task<(List<{type}>? data, PageModel pageInfo)>";
                    MethodName = $"GetPageResultModelBy";
                    methodType = type;
                }
                else if (code.StartsWith("Task<ResultModel<") || code.StartsWith("ResultModel<"))
                {
                    string type = GetType(code);
                    ResultType = $"Task<{type}?>";
                    MethodName = $"GetResultModelBy";
                    methodType = type;
                }
                else if (code.StartsWith("Task<ResultModel") || code.StartsWith("ResultModel"))
                {
                    ResultType = "Task";
                    MethodName = $"GetResultModelBy";
                    methodType = null;
                }
                else
                {
                    ResultType = "Task<string>";
                    MethodName = $"GetResultBy";
                    methodType = null;
                }
                ResultType = ResultType.Replace("??", "?");
                Name = frontCodes[^1].Trim();
                if (Name.EndsWith("Async"))
                {
                    Name = Name[..^5];
                }
            }
            #endregion
            #region 解析参数
            {
                string backCode = actionCode[(bracketStartIndex + 1)..];
                backCode = backCode[..^1];
                if (!string.IsNullOrWhiteSpace(backCode))
                {
                    string[] backCodes = backCode.Split(',');
                    string[] queryTypeList = new string[]
                    {
                        "Guid","string","DateTime","int","decimal","float","double","uint"
                    };
                    string[] removeList = new string[]
                    {
                        "[FromBody]","[FromForm]","[FromQuery]"
                    };
                    string[] blackList = new string[]
                    {
                        "IFormFile"
                    };
                    foreach (string code in backCodes)
                    {
                        if (string.IsNullOrWhiteSpace(code)) continue;
                        string trueCode = code;
                        foreach (var item in removeList)
                        {
                            trueCode = trueCode.Replace(item, "");
                        }
                        string[] temps = trueCode.Split(' ');
                        string type = temps[^2];
                        int tempIndex = type.IndexOf("]");
                        if (tempIndex > 0)
                        {
                            type = type[(tempIndex + 1)..];
                        }
                        if (blackList.Contains(type))
                        {
                            GeneratorCode = false;
                        }
                        if (queryTypeList.Contains(type))
                        {
                            QueryParams.Add(trueCode);
                        }
                        else
                        {
                            BodyParams = trueCode;
                        }
                    }
                }
            }
            #endregion
            #region 解析HttpMethod
            actionCode = codes[startIndex--].Trim();
            if (actionCode.Contains("HttpGet"))
            {
                HttpMethod = "Get";
            }
            else if (actionCode.Contains("HttpPost"))
            {
                HttpMethod = "Post";
            }
            else if (actionCode.Contains("HttpPut"))
            {
                HttpMethod = "Put";
            }
            else if (actionCode.Contains("HttpDelete"))
            {
                HttpMethod = "Delete";
            }
            MethodName += $"{HttpMethod}Async";
            if (!string.IsNullOrWhiteSpace(methodType))
            {
                MethodName += $"<{methodType}>";
            }
            #endregion
            #region 解析注释
            actionCode = codes[startIndex--].Trim();
            while (actionCode.StartsWith("///"))
            {
                Annotations.Insert(0, actionCode);
                actionCode = codes[startIndex--].Trim();
            }
            #endregion
        }

        /// <summary>
        /// 获得类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetType(string code)
        {
            int angleBracketStartIndex = -1;
            if (code.StartsWith("Task<PageResultModel<") || code.StartsWith("PageResultModel<"))
            {
                angleBracketStartIndex = code.LastIndexOf("PageResultModel<") + 15;
            }
            else if (code.StartsWith("Task<ResultModel<") || code.StartsWith("ResultModel<"))
            {
                angleBracketStartIndex = code.LastIndexOf("ResultModel<") + 11;
            }
            if (angleBracketStartIndex < 0) return code;
            string type = code[(angleBracketStartIndex + 1)..];
            int frontCount = type.Count(m => m == '<');
            int backCount = type.Count(m => m == '>');
            while (frontCount != backCount && type.EndsWith(">"))
            {
                type = type[..^1];
                backCount = type.Count(m => m == '>');
            }
            return type;
        }
    }
}
