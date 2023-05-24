using Materal.BaseCore.CodeGenerator.Extensions;
using System.Text;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class ServiceModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        public List<InterfaceMethodModel> InterfaceMethodModels { get; set; } = new();
        public readonly List<string> Usings = new();
        public ServiceModel() { }
        public ServiceModel(string[] codes, int classLineIndex)
        {
            Name = GetServiceName(codes[classLineIndex]);
            Append(codes);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="codes"></param>
        public void Append(string[] codes)
        {
            for (int i = 0; i < codes.Length; i++)
            {
                string actionCode = codes[i].Trim();
                if (actionCode.StartsWith("using"))
                {
                    if (actionCode == "using Materal.BaseCore.PresentationModel;" || 
                        actionCode == "using Materal.BaseCore.Services.Models;" ||
                        actionCode == "using Materal.Utils.Model;" ||
                        actionCode == "using Microsoft.AspNetCore.Mvc;" || 
                        actionCode == "using System.ComponentModel.DataAnnotations;" ||
                        Usings.Contains(actionCode)) continue;
                    Usings.Add(actionCode);
                    continue;
                }
                if (!actionCode.EndsWith(");")) continue;
                InterfaceMethodModels.Add(new InterfaceMethodModel(codes, i));
            }
        }
        /// <summary>
        /// 获得服务名称
        /// </summary>
        /// <param name="classCode"></param>
        /// <returns></returns>
        public static string? GetServiceName(string classCode)
        {
            string[] temps = classCode.Trim().Split(' ');
            for (int i = 0; i < temps.Length; i++)
            {
                if (temps[i] == "interface")
                {
                    string name = temps[i + 1];
                    name = name[1..^7];
                    return name;
                }
            }
            return null;
        }
        /// <summary>
        /// 创建WebAPI控制器文件
        /// </summary>
        /// <param name="project"></param>
        public void CreateWebAPIControllerFile(ProjectModel project)
        {
            string filePath = Path.Combine("Controllers", $"{Name}Controller.Mapper.g.cs");
            project.ClearMCGFile(filePath);
            List<InterfaceMethodModel> models = InterfaceMethodModels.Where(m => m.GeneratorCode).ToList();
            if (models.Count <= 0) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using Materal.BaseCore.PresentationModel;");
            codeContent.AppendLine($"using Materal.BaseCore.Services.Models;");
            codeContent.AppendLine($"using Materal.Utils.Model;");
            codeContent.AppendLine($"using Microsoft.AspNetCore.Mvc;");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            foreach (string @using in Usings)
            {
                codeContent.AppendLine(@using);
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.WebAPI.Controllers");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    public partial class {Name}Controller");
            codeContent.AppendLine($"    {{");
            foreach (InterfaceMethodModel model in models)
            {
                if(model.Annotations.Count > 1)
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        {model.Annotations[1]}");
                    codeContent.AppendLine($"        /// </summary>");
                }
                foreach (ParamModel item in model.Params)
                {
                    codeContent.AppendLine($"        /// <param name=\"{item.ControllerName}\"></param>");
                }
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [Http{model.HttpMethod}]");
                if (model.IsTask)
                {
                    codeContent.Append($"        public async Task<{model.ResultModelType}> {model.Name}(");
                }
                else
                {
                    codeContent.Append($"        public {model.ResultModelType} {model.Name}(");
                }
                List<string> inputArgNames = new();
                List<string> modelNames = new();
                StringBuilder mapperCode = new();
                List<string> inputArgs = new();
                for (int i = 0; i < model.Params.Count; i++)
                {
                    ParamModel item = model.Params[i];
                    string type = string.IsNullOrWhiteSpace(item.RequestModelType) ? item.Type : item.RequestModelType;
                    string name = string.IsNullOrWhiteSpace(item.RequestModelName) ? item.Name : item.RequestModelName;
                    string prefix = string.Empty;
                    if(type != item.RequestModelType)
                    {
                        prefix = type.EndsWith("?") ? "" : $"[Required(ErrorMessage = \"{name}不能为空\")] ";
                        modelNames.Add(name);
                    }
                    else
                    {
                        string modelName = $"model{i}";
                        modelNames.Add(modelName);
                        mapperCode.AppendLine($"            var {modelName} = Mapper.Map<{item.Type}>({name});");
                    }
                    inputArgs.Add($"{prefix}{type} {name}");
                    inputArgNames.Add(name);
                }
                codeContent.AppendLine($"{string.Join(",", inputArgs)})");
                codeContent.AppendLine($"        {{");
                string mapperCodeStr = mapperCode.ToString();
                if (!string.IsNullOrWhiteSpace(mapperCodeStr))
                {
                    codeContent.Append(mapperCodeStr);
                }
                if (model.HasResultDataModel)
                {
                    if (model.IsTask)
                    {
                        codeContent.AppendLine($"            var result = await DefaultService.{model.Name}({string.Join(",", modelNames)});");
                    }
                    else
                    {
                        codeContent.AppendLine($"            var result = DefaultService.{model.Name}({string.Join(",", modelNames)});");
                    }
                    codeContent.AppendLine($"            return {model.ResultModelType}.Success(result);");
                }
                else
                {
                    if (model.IsTask)
                    {
                        codeContent.AppendLine($"            await DefaultService.{model.Name}({string.Join(",", modelNames)});");
                    }
                    else
                    {
                        codeContent.AppendLine($"            DefaultService.{model.Name}({string.Join(",", modelNames)});");
                    }
                    codeContent.AppendLine($"            return {model.ResultModelType}.Success();");
                }
                codeContent.AppendLine($"        }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string code = codeContent.ToString();
            codeContent.SaveFile(project.GeneratorRootPath, filePath);
        }
    }
}
