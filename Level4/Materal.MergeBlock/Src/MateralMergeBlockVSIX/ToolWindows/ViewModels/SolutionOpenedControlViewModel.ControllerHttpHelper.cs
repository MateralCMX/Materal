#nullable enable
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedControlViewModel : ObservableObject
    {
        /// <summary>
        /// 生成控制器访问器
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeAfterMethod]
        private void GeneratorControllerAccessors(List<IControllerModel> controllers)
        {
            foreach (IControllerModel controller in controllers)
            {
                GeneratorControllerAccessor(controller);
            }
        }
        /// <summary>
        /// 生成控制器访问器
        /// </summary>
        /// <param name="controller"></param>
        private void GeneratorControllerAccessor(IControllerModel controller)
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.DTO.{controller.DomainName};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.RequestModel.{controller.DomainName};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.HttpClient");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {controller.Annotation}访问器");
            codeContent.AppendLine($"    /// </summary>");
            string? interfaceCode = controller.Interfaces.FirstOrDefault(m => m.StartsWith("IMergeBlockControllerBase<"));
            if (interfaceCode is not null)
            {
                codeContent.AppendLine($"    public partial class {controller.DomainName}ControllerAccessor(IServiceProvider serviceProvider) : BaseControllerAccessor<I{controller.DomainName}Controller, Add{controller.DomainName}RequestModel, Edit{controller.DomainName}RequestModel, Query{controller.DomainName}RequestModel, {controller.DomainName}DTO, {controller.DomainName}ListDTO>(serviceProvider), I{controller.DomainName}Controller");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {controller.DomainName}ControllerAccessor(IServiceProvider serviceProvider) : BaseControllerAccessor(serviceProvider), I{controller.DomainName}Controller");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 项目名称");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public override string ProjectName => \"{_projectName}\";");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 模块名称");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public override string ModuleName => \"{_moduleName}\";");
            foreach (MethodModel method in controller.Methods)
            {
                codeContent.AppendLine($"        /// <summary>");
                if (!string.IsNullOrWhiteSpace(method.Annotation))
                {
                    codeContent.AppendLine($"        /// {method.Annotation}");
                }
                else
                {
                    codeContent.AppendLine($"        /// {method.Name}");
                }
                codeContent.AppendLine($"        /// </summary>");
                List<string> arguments = [];
                List<string> dicArguments = [];
                List<string> objArguments = [];
                foreach (MethodArgumentModel argument in method.Arguments)
                {
                    codeContent.AppendLine($"        /// <param name=\"{argument.Name}\"></param>");
                    arguments.Add($"{argument.PredefinedType} {argument.Name}");
                    switch (argument.PredefinedType)
                    {
                        case "Guid":
                        case "int":
                        case "long":
                        case "decimal":
                        case "double":
                        case "float":
                        case "DateTime":
                        case "bool":
                            dicArguments.Add($"[nameof({argument.Name})] = {argument.Name}.ToString()");
                            break;
                        case "string":
                            dicArguments.Add($"[nameof({argument.Name})] = {argument.Name}");
                            break;
                        default:
                            objArguments.Add(argument.Name);
                            break;
                    }
                }
                if (method.IsTaskReturnType || method.NotTaskReturnType != "void")
                {
                    codeContent.AppendLine($"        /// <returns></returns>");
                }
                if (method.IsTaskReturnType)
                {
                    codeContent.AppendLine($"        public async Task<{method.NotTaskReturnType}> {method.Name}({string.Join(", ", arguments)})");
                }
                else
                {
                    codeContent.AppendLine($"        public {method.NotTaskReturnType} {method.Name}({string.Join(", ", arguments)})");
                }
                string dicCode = "[]";
                if (dicArguments.Count > 0)
                {
                    dicCode = $"new() {{{string.Join(", ", dicArguments)}}}";
                }
                string objCode = string.Empty;
                if (objArguments.Count > 0)
                {
                    objCode = string.Join(", ", objArguments);
                }
                if (method.IsTaskReturnType)
                {
                    if (string.IsNullOrWhiteSpace(objCode))
                    {
                        codeContent.AppendLine($"            => await HttpHelper.SendAsync<I{controller.DomainName}Controller, {method.NotTaskReturnType}>(ProjectName, ModuleName, nameof({method.Name}), {dicCode});");
                    }
                    else
                    {
                        codeContent.AppendLine($"            => await HttpHelper.SendAsync<I{controller.DomainName}Controller, {method.NotTaskReturnType}>(ProjectName, ModuleName, nameof({method.Name}), {dicCode}, {objCode});");
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(objCode))
                    {
                        codeContent.AppendLine($"            => HttpHelper.SendAsync<I{controller.DomainName}Controller, {method.NotTaskReturnType}>(ProjectName, ModuleName, nameof({method.Name}), {dicCode}).Result;");
                    }
                    else
                    {
                        codeContent.AppendLine($"            => HttpHelper.SendAsync<I{controller.DomainName}Controller, {method.NotTaskReturnType}>(ProjectName, ModuleName, nameof({method.Name}), {dicCode}, {objCode}).Result;");
                    }
                }
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "ControllerAccessors", $"{controller.DomainName}ControllerAccessor.cs");
        }
    }
}
