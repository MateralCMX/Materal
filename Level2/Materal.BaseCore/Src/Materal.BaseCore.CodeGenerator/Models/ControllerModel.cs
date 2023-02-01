using Materal.BaseCore.CodeGenerator.Extensions;
using System.Text;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class ControllerModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; }
        private readonly List<string> _annotations = new();
        private bool _isServiceHttpClient = false;
        private readonly string[] _tModels = new string[5];
        private readonly List<ActionModel> actionModels = new();
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
                string tModel = classCode.Substring(angleBracketStartIndex + 1);
                tModel = tModel.Substring(0, tModel.Length - 1);
                string[] tModels = tModel.Split(',');
                if (tModels.Length == 9)
                {
                    _isServiceHttpClient = true;
                    _tModels[0] = tModels[0].Trim();
                    _tModels[1] = tModels[1].Trim();
                    _tModels[2] = tModels[2].Trim();
                    _tModels[3] = tModels[6].Trim();
                    _tModels[4] = tModels[7].Trim();
                }
            }
            #region 解析注释
            {
                if (_annotations.Count == 0)
                {
                    int startIndex = classLineIndex;
                    string code = codes[startIndex--].Trim();
                    while (code.StartsWith("///"))
                    {
                        _annotations.Insert(0, code);
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
                    actionModels.Add(new ActionModel(codes, i));
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
                    name = name.Substring(0, name.Length - 10);
                    return name;
                }
            }
            return null;
        }
        /// <summary>
        /// 创建HttpClient文件
        /// </summary>
        public void CreateHttpClientFile(ProjectModel project)
        {
            if (!actionModels.Any(m => m.GeneratorCode) && !_isServiceHttpClient) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using {project.PrefixName}.Core.HttpClient;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.DataTransmitModel.{Name};");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.PresentationModel.{Name};");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using Materal.BaseCore.Common.Utils.TreeHelper;");
            codeContent.AppendLine($"using Materal.BaseCore.Common.Utils.IndexHelper;");
            codeContent.AppendLine($"using Materal.BaseCore.PresentationModel;");
            codeContent.AppendLine($"using Materal.Model;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.HttpClient");
            codeContent.AppendLine($"{{");
            foreach (string annotation in _annotations)
            {
                codeContent.AppendLine($"    {annotation}");
            }
            if (_isServiceHttpClient)
            {
                codeContent.AppendLine($"    public partial class {Name}HttpClient : HttpClientBase<{_tModels[0]}, {_tModels[1]}, {_tModels[2]}, {_tModels[3]}, {_tModels[4]}>");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {Name}HttpClient : HttpClientBase");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        public {Name}HttpClient() : base(\"{project.PrefixName}.{project.ProjectName}\") {{ }}");
            foreach (ActionModel action in actionModels)
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
                    if (temps[temps.Length - 2] != "string")
                    {
                        querySendArgs.Add($"[nameof({temp})] = {temp}.ToString()");
                    }
                    else
                    {
                        querySendArgs.Add($"[nameof({temp})] = {temp}");
                    }
                }
                codeContent.Append($"{string.Join(", ", args)}) => await {action.MethodName}(\"{Name}/{action.Name}\", ");
                if (string.IsNullOrWhiteSpace(bodySendArgs))
                {
                    codeContent.Append($"null, ");
                }
                else
                {
                    codeContent.Append($"{bodySendArgs}, ");
                }
                if (querySendArgs.Count > 0)
                {
                    codeContent.Append($"new Dictionary<string, string> {{ ");
                    codeContent.Append(string.Join(", ", querySendArgs));
                    codeContent.Append($"}}");
                }
                else
                {
                    codeContent.Append($"null");
                }
                codeContent.AppendLine($");");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(project.GeneratorRootPath, $"{Name}HttpClient.g.cs");
        }
    }
}
