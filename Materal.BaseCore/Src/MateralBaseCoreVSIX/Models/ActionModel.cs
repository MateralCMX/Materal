using System;
using System.Collections.Generic;
using System.Linq;

namespace MateralBaseCoreVSIX.Models
{
    public class ActionModel
    {
        /// <summary>
        /// 注释
        /// </summary>
        public List<string> Annotations { get; } = new List<string>();
        /// <summary>
        /// Http类型
        /// </summary>
        public string HttpMethod { get; }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName { get; }
        /// <summary>
        /// 返回类型
        /// </summary>
        public string ResultType { get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Body参数
        /// </summary>
        public string BodyParams { get; } = null;
        /// <summary>
        /// Query参数
        /// </summary>
        public List<string> QueryParams { get; } = new List<string>();
        public ActionModel(string[] codes, int startIndex)
        {
            string actionCode = codes[startIndex--].Trim();
            int bodyIndex = actionCode.IndexOf("=>");
            if(bodyIndex >= 0)
            {
                actionCode = actionCode.Substring(0, bodyIndex).Trim();
            }
            string methodType = null;
            int bracketStartIndex = actionCode.IndexOf("([");
            if(bracketStartIndex < 0)
            {
                bracketStartIndex = actionCode.LastIndexOf("(");
            }
            #region 解析方法
            {
                string[] frontCodes = actionCode.Substring(0, bracketStartIndex).Split(' ');
                string code = frontCodes[frontCodes.Length - 2].Trim();
                if (code.StartsWith("Task<PageResultModel<") || code.StartsWith("PageResultModel<"))
                {
                    string type = GetType(code);
                    ResultType = $"Task<(List<{type}> data, PageModel pageInfo)>";
                    MethodName = $"GetPageResultModelBy";
                    methodType = type;
                }
                else if (code.StartsWith("Task<ResultModel<") || code.StartsWith("ResultModel<"))
                {
                    string type = GetType(code);
                    ResultType = $"Task<{type}>";
                    MethodName = $"GetResultModelBy";
                    methodType = type;
                }
                else if (code.StartsWith("Task<ResultModel") || code.StartsWith("ResultModel"))
                {
                    ResultType = "Task";
                    MethodName = $"GetResultModelBy";
                    methodType = null;
                }
                Name = frontCodes[frontCodes.Length - 1].Trim();
                if (Name.EndsWith("Async"))
                {
                    Name = Name.Substring(0, Name.Length - 5);
                }
            }
            #endregion
            #region 解析参数
            {
                string backCode = actionCode.Substring(bracketStartIndex + 1);
                backCode = backCode.Substring(0, backCode.Length - 1);
                if (!string.IsNullOrWhiteSpace(backCode))
                {
                    string[] backCodes = backCode.Split(',');
                    string[] queryTypeList = new string[]
                    {
                        "Guid","string","DateTime","int","decimal","float","double","uint"
                    };
                    foreach (string code in backCodes)
                    {
                        if (string.IsNullOrWhiteSpace(code)) continue;
                        string[] temps = code.Split(' ');
                        if (queryTypeList.Contains(temps[temps.Length - 2]))
                        {
                            QueryParams.Add(code);
                        }
                        else
                        {
                            BodyParams = code;
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
            string type = code.Substring(angleBracketStartIndex + 1);
            if (type.EndsWith(">>"))
            {
                type = type.Substring(0, type.Length - 2);
            }
            else
            {
                type = type.Substring(0, type.Length - 1);
            }
            return type;
        }
    }
}
