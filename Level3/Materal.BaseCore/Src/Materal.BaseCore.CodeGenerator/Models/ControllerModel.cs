using Materal.BaseCore.CodeGenerator.Extensions;
using System.Text;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class ControllerModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        public List<string> Annotations { get; set; } = new();
        public bool IsServiceHttpClient { get; set; } = false;
        /// <summary>
        /// 生成代码
        /// </summary>
        public bool GeneratorCode { get; set;} = false;
        public string[] TModels { get; set; } = new string[5];
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = new();
        public List<ActionModel> ActionModels { get; set; } = new();
        /// <summary>
        /// 引用
        /// </summary>
        public List<string> Usings { get; set; } = new();
        public ControllerModel() { }
        public ControllerModel(string[] codes, int classLineIndex)
        {
            Name = GetControllerName(codes[classLineIndex]);
            Append(codes, classLineIndex);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="classLineIndex"></param>
        public void Append(string[] codes, int classLineIndex)
        {
            string classCode = codes[classLineIndex].Trim();
            if (classCode.EndsWith(">"))
            {
                int angleBracketStartIndex = classCode.IndexOf('<');
                if (angleBracketStartIndex < 0) throw new CodeGeneratorException("类型错误");
                string tModel = classCode[(angleBracketStartIndex + 1)..];
                tModel = tModel[..^1];
                string[] tModels = tModel.Split(',');
                if (tModels.Length == 9)
                {
                    IsServiceHttpClient = true;
                    TModels[0] = tModels[0].Trim();
                    TModels[1] = tModels[1].Trim();
                    TModels[2] = tModels[2].Trim();
                    TModels[3] = tModels[6].Trim();
                    TModels[4] = tModels[7].Trim();
                }
            }
            #region 解析Using
            for (int i = 0; i < codes.Length; i++)
            {
                if (codes[i].StartsWith("namespace")) break;
                if (!codes[i].StartsWith("using")) continue;
                Usings.Add(codes[i]);
            }
            Usings = Usings.Distinct().OrderBy(m => m).ToList();
            #endregion
            #region 解析特性
            {
                int startIndex = classLineIndex - 1;
                do
                {
                    if (startIndex < 0) break;
                    string attributeCode = codes[startIndex].Trim();
                    if (!attributeCode.StartsWith("[") || !attributeCode.EndsWith("]")) break;
                    startIndex -= 1;
                    List<string> attributeCodes = attributeCode.GetAttributeCodes();
                    Attributes.AddRange(attributeCodes.Select(attributeName => new AttributeModel(attributeName.Trim())));
                } while (true);
                GeneratorCode = !Attributes.HasAttribute<NotGeneratorAttribute>();
            }
            #endregion
            #region 解析注释
            {
                if (Annotations.Count == 0)
                {
                    int startIndex = classLineIndex;
                    string code = codes[startIndex--].Trim();
                    while (code.StartsWith("///"))
                    {
                        Annotations.Insert(0, code);
                        code = codes[startIndex--].Trim();
                    }
                }
            }
            #endregion
            #region 拼装自定义Action
            for (int i = classLineIndex; i < codes.Length; i++)
            {
                string actionCode = codes[i].Trim();
                if (actionCode.Contains("HttpGet") || actionCode.Contains("HttpPut") || actionCode.Contains("HttpPost") || actionCode.Contains("HttpDelete"))
                {
                    i += 1;
                    actionCode = codes[i].Trim();
                    int overrideIndex = actionCode.IndexOf(" override ");
                    if (overrideIndex >= 0) continue;
                    ActionModels.Add(new ActionModel(codes, i));
                }
            }
            #endregion
        }
        /// <summary>
        /// 获得控制器名称
        /// </summary>
        /// <param name="classCode"></param>
        /// <returns></returns>
        public static string? GetControllerName(string classCode)
        {
            string[] temps = classCode.Trim().Split(' ');
            for (int i = 0; i < temps.Length; i++)
            {
                if (temps[i] == "class")
                {
                    string name = temps[i + 1];
                    name = name[..^10];
                    return name;
                }
            }
            return null;
        }
        /// <summary>
        /// httpClient引用黑名单
        /// </summary>
        private readonly string[] _httpClientUsingStaticBlackList = new[]
        {
            "using Microsoft.AspNetCore",
            "using Materal.BaseCore.Services",
            "using Materal.BaseCore.CodeGenerator",
            "using Materal.BaseCore.WebAPI",
            "using Materal.Utils"
        };
        /// <summary>
        /// 创建HttpClient文件
        /// </summary>
        public void CreateHttpClientFile(ProjectModel project)
        {
            if (!GeneratorCode) return;
            if (!ActionModels.Any(m => m.GeneratorCode) && !IsServiceHttpClient) return;
            string[] _httpClientUsingBlackList = new[]
            {
                $"using {project.PrefixName}.Core",
                $"using {project.PrefixName}.{project.ProjectName}.Services"
            };
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using {project.PrefixName}.Core.HttpClient;");
            codeContent.AppendLine($"using Materal.Utils.Model;");
            foreach (string @using in Usings)
            {
                if (_httpClientUsingStaticBlackList.Any(m => @using.StartsWith(m))) continue;
                if (_httpClientUsingBlackList.Any(m => @using.StartsWith(m))) continue;
                codeContent.AppendLine(@using);
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.HttpClient");
            codeContent.AppendLine($"{{");
            foreach (string annotation in Annotations)
            {
                codeContent.AppendLine($"    {annotation}");
            }
            if (IsServiceHttpClient)
            {
                codeContent.AppendLine($"    public partial class {Name}HttpClient : HttpClientBase<{TModels[0]}, {TModels[1]}, {TModels[2]}, {TModels[3]}, {TModels[4]}>");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {Name}HttpClient : HttpClientBase");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        public {Name}HttpClient() : base(\"{project.PrefixName}.{project.ProjectName}\") {{ }}");
            foreach (ActionModel action in ActionModels)
            {
                if (!action.GeneratorCode) continue;
                foreach (string annotation in action.Annotations)
                {
                    codeContent.AppendLine($"        {annotation}");
                }
                codeContent.Append($"        public async {action.ResultType} {action.Name}Async(");
                List<string> args = new();
                string? bodySendArgs = null;
                if (!string.IsNullOrWhiteSpace(action.BodyParams) && action.BodyParams != null)
                {
                    args.Add(action.BodyParams);
                    bodySendArgs = action.BodyParams.Split(' ').Last();
                }
                List<string> querySendArgs = new();
                foreach (string arg in action.QueryParams)
                {
                    args.Add(arg);
                    string[] temps = arg.Split(' ');
                    string temp = temps.Last();
                    if (temps[^2] != "string")
                    {
                        querySendArgs.Add($"[nameof({temp})] = {temp}.ToString()");
                    }
                    else
                    {
                        querySendArgs.Add($"[nameof({temp})] = {temp}");
                    }
                }
                codeContent.Append($"{string.Join(", ", args)}) => await {action.MethodName}(\"{Name}/{action.Name}\"");
                if (querySendArgs.Count > 0)
                {
                    codeContent.Append($", new Dictionary<string, string> {{ ");
                    codeContent.Append(string.Join(", ", querySendArgs));
                    codeContent.Append($"}}");
                }
                if (!string.IsNullOrWhiteSpace(bodySendArgs))
                {
                    if (querySendArgs.Count <= 0)
                    {
                        codeContent.Append($", null");
                    }
                    codeContent.Append($", {bodySendArgs}");
                }
                codeContent.AppendLine($");");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(project.GeneratorRootPath, $"{Name}HttpClient.g.cs");
        }
    }
}
