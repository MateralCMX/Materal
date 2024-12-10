#nullable enable
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using System.Collections.Generic;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedViewModel
    {
        /// <summary>
        /// 创建控制器代码
        /// </summary>
        [GeneratorCodeMethod]
        private async Task GeneratorControllerMapperCodeAsync()
        {
            foreach (IServiceModel service in Context.Services)
            {
                await GeneratorIControllerMapperCodeAsync(service);
                await GeneratorControllerMapperCodeAsync(service);
            }
        }
        /// <summary>
        /// 创建控制器代码接口
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorIControllerMapperCodeAsync(IServiceModel service)
        {
            if (!service.HasMapperMethod) return;
            StringBuilder codeContent = new();
            bool isUsing = false;
            foreach (string usingCode in service.Usings)
            {
                string trueUsingCode = usingCode;
                if (trueUsingCode.Contains($"{_projectName}.{_moduleName}.Abstractions.Services.Models"))
                {
                    trueUsingCode = trueUsingCode.Replace("Services.Models", "RequestModel");
                }
                codeContent.AppendLine($"using {trueUsingCode};");
                isUsing = true;
            }
            if (isUsing)
            {
                codeContent.AppendLine($"");
            }
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Controllers");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {service.Annotation}控制器");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial interface I{service.DomainName}Controller : IMergeBlockController");
            codeContent.AppendLine($"    {{");
            foreach (MethodModel method in service.Methods)
            {
                AttributeModel? attribute = method.Attributes.GetAttribute<MapperControllerAttribute>();
                string? httpMethod = attribute?.GetAttributeArgument()?.Value;
                if (attribute is null || httpMethod is null) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {method.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                foreach (MethodArgumentModel argument in method.Arguments)
                {
                    codeContent.AppendLine($"        /// <param name=\"{argument.RequestName}\"></param>");
                }
                if (method.ReturnType != "void")
                {
                    codeContent.AppendLine($"        /// <returns></returns>");
                }
                codeContent.Append($"        [");
                switch (httpMethod)
                {
                    case "MapperType.Get":
                        codeContent.Append($"HttpGet");
                        break;
                    case "MapperType.Post":
                        codeContent.Append($"HttpPost");
                        break;
                    case "MapperType.Put":
                        codeContent.Append($"HttpPut");
                        break;
                    case "MapperType.Delete":
                        codeContent.Append($"HttpDelete");
                        break;
                    case "MapperType.Patch":
                        codeContent.Append($"HttpPatch");
                        break;
                }
                string? isAllowAnonymous = attribute.GetAttributeArgument(nameof(MapperControllerAttribute.IsAllowAnonymous))?.Value;
                if (isAllowAnonymous is not null && isAllowAnonymous.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    codeContent.Append($", AllowAnonymous");
                }
                codeContent.AppendLine($"]");
                List<string> methodArguments = [];
                for (int i = 0; i < method.Arguments.Count; i++)
                {
                    MethodArgumentModel methodArgument = method.Arguments[i];
                    methodArguments.Add($"{methodArgument.RequestPredefinedType} {methodArgument.RequestName}");
                }
                if (method.IsTaskReturnType)
                {
                    if (method.NotTaskReturnType == "void")
                    {
                        codeContent.AppendLine($"        Task<ResultModel> {method.Name}({string.Join(", ", methodArguments)});");
                    }
                    else if (IsPageReturnType(method.NotTaskReturnType, out PageReturnTypeModel pageReturn))
                    {
                        codeContent.AppendLine($"        Task<CollectionResultModel<{pageReturn.PageResultListType}>> {method.Name}({string.Join(", ", methodArguments)});");
                    }
                    else
                    {
                        codeContent.AppendLine($"        Task<ResultModel<{method.NotTaskReturnType}>> {method.Name}({string.Join(", ", methodArguments)});");
                    }
                }
                else
                {
                    if (method.NotTaskReturnType == "void")
                    {
                        codeContent.AppendLine($"        ResultModel {method.Name}({string.Join(", ", methodArguments)});");
                    }
                    else if (IsPageReturnType(method.NotTaskReturnType, out PageReturnTypeModel pageReturn))
                    {
                        codeContent.AppendLine($"        CollectionResultModel<{pageReturn.PageResultListType}> {method.Name}({string.Join(", ", methodArguments)});");
                    }
                    else
                    {
                        codeContent.AppendLine($"        ResultModel<{method.NotTaskReturnType}> {method.Name}({string.Join(", ", methodArguments)});");
                    }
                }
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "Controllers", $"I{service.DomainName}Controller.Mapper.cs");
        }
        /// <summary>
        /// 创建控制器代码接口
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorControllerMapperCodeAsync(IServiceModel service)
        {
            if (!service.HasMapperMethod) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorControllerMapperCodeAsync)}");
            codeContent.AppendLine($" */");
            bool isUsing = false;
            foreach (string usingCode in service.Usings)
            {
                string trueUsingCode = usingCode;
                if (trueUsingCode.Contains($"{_projectName}.{_moduleName}.Abstractions.Services.Models"))
                {
                    codeContent.AppendLine($"using {usingCode};");
                    trueUsingCode = trueUsingCode.Replace("Services.Models", "RequestModel");
                }
                codeContent.AppendLine($"using {trueUsingCode};");
                isUsing = true;
            }
            if (isUsing)
            {
                codeContent.AppendLine($"");
            }
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Application.Controllers");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {service.Annotation}控制器");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {service.DomainName}Controller");
            codeContent.AppendLine($"    {{");
            foreach (MethodModel method in service.Methods)
            {
                AttributeModel? attribute = method.Attributes.GetAttribute<MapperControllerAttribute>();
                string? httpMethod = attribute?.GetAttributeArgument()?.Value;
                if (attribute is null || httpMethod is null) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {method.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                foreach (MethodArgumentModel argument in method.Arguments)
                {
                    codeContent.AppendLine($"        /// <param name=\"{argument.RequestName}\"></param>");
                }
                if (method.ReturnType != "void")
                {
                    codeContent.AppendLine($"        /// <returns></returns>");
                }
                codeContent.Append($"        [");
                switch (httpMethod)
                {
                    case "MapperType.Get":
                        codeContent.Append($"HttpGet");
                        break;
                    case "MapperType.Post":
                        codeContent.Append($"HttpPost");
                        break;
                    case "MapperType.Put":
                        codeContent.Append($"HttpPut");
                        break;
                    case "MapperType.Delete":
                        codeContent.Append($"HttpDelete");
                        break;
                    case "MapperType.Patch":
                        codeContent.Append($"HttpPatch");
                        break;
                }
                string? isAllowAnonymous = attribute.GetAttributeArgument(nameof(MapperControllerAttribute.IsAllowAnonymous))?.Value;
                if (isAllowAnonymous is not null && isAllowAnonymous.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    codeContent.Append($", AllowAnonymous");
                }
                codeContent.AppendLine($"]");
                List<string> methodArguments = [];
                List<string> mapperArguments = [];
                List<string> mapperCodes = [];
                List<string> useArguments = [];
                for (int i = 0; i < method.Arguments.Count; i++)
                {
                    MethodArgumentModel methodArgument = method.Arguments[i];
                    methodArguments.Add($"{methodArgument.RequestPredefinedType} {methodArgument.RequestName}");
                    if (methodArgument.RequestName != methodArgument.Name)
                    {
                        mapperCodes.Add($"            {methodArgument.PredefinedType} {methodArgument.Name} = Mapper.Map<{methodArgument.PredefinedType}>({methodArgument.RequestName}) ?? throw new {_projectName}Exception(\"映射失败\");");
                        mapperArguments.Add(methodArgument.Name);
                    }
                    useArguments.Add(methodArgument.Name);
                }
                if (method.IsTaskReturnType)
                {
                    if (method.NotTaskReturnType == "void")
                    {
                        codeContent.AppendLine($"        public async Task<ResultModel> {method.Name}({string.Join(", ", methodArguments)})");
                    }
                    else if (IsPageReturnType(method.NotTaskReturnType, out PageReturnTypeModel pageReturn))
                    {
                        codeContent.AppendLine($"        public async Task<CollectionResultModel<{pageReturn.PageResultListType}>> {method.Name}({string.Join(", ", methodArguments)})");
                    }
                    else
                    {
                        codeContent.AppendLine($"        public async Task<ResultModel<{method.NotTaskReturnType}>> {method.Name}({string.Join(", ", methodArguments)})");
                    }
                }
                else
                {
                    if (method.NotTaskReturnType == "void")
                    {
                        codeContent.AppendLine($"        public ResultModel {method.Name}({string.Join(", ", methodArguments)})");
                    }
                    else if (IsPageReturnType(method.NotTaskReturnType, out PageReturnTypeModel pageReturn))
                    {
                        codeContent.AppendLine($"        public CollectionResultModel<{pageReturn.PageResultListType}> {method.Name}({string.Join(", ", methodArguments)})");
                    }
                    else
                    {
                        codeContent.AppendLine($"        public ResultModel<{method.NotTaskReturnType}> {method.Name}({string.Join(", ", methodArguments)})");
                    }
                }
                codeContent.AppendLine($"        {{");
                foreach (string mapperCode in mapperCodes)
                {
                    codeContent.AppendLine(mapperCode);
                }
                foreach (string mapperArgument in mapperArguments)
                {
                    codeContent.AppendLine($"            BindLoginUserID({mapperArgument});");
                }
                codeContent.Append($"            ");
                if (method.NotTaskReturnType != "void")
                {
                    if (IsPageReturnType(method.NotTaskReturnType, out PageReturnTypeModel pageReturn))
                    {
                        codeContent.Append($"({pageReturn.LeftType} {pageReturn.LeftName}, {pageReturn.RightType} {pageReturn.RightName}) = ");
                    }
                    else
                    {
                        codeContent.Append($"{method.NotTaskReturnType} result = ");
                    }
                }
                if (method.IsTaskReturnType)
                {
                    codeContent.Append($"await ");
                }
                codeContent.AppendLine($"DefaultService.{method.Name}({string.Join(", ", useArguments)});");
                if (method.NotTaskReturnType == "void")
                {
                    codeContent.AppendLine($"            return ResultModel.Success(\"{method.Annotation}成功\");");
                }
                else if (IsPageReturnType(method.NotTaskReturnType, out PageReturnTypeModel pageReturn))
                {
                    codeContent.AppendLine($"            return CollectionResultModel<{pageReturn.PageResultListType}>.Success({pageReturn.LeftName}, {pageReturn.RightName}, \"{method.Annotation}成功\");");
                }
                else
                {
                    codeContent.AppendLine($"            return ResultModel<{method.NotTaskReturnType}>.Success(result, \"{method.Annotation}成功\");");
                }
                codeContent.AppendLine($"        }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleApplication, "Controllers", $"{service.DomainName}Controller.Mapper.cs");
        }
        /// <summary>
        /// 是分页返回类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private bool IsPageReturnType(string notTaskReturnType, out PageReturnTypeModel pageReturn)
        {
            try
            {
                string code = notTaskReturnType;
                pageReturn = new();
                if (!code.StartsWith("(List<") || !code.EndsWith(")")) return false;
                code = code[1..^1];
                string[] temps = code.Split(',');
                if (temps.Length != 2) return false;
                string left = temps[0].Trim();
                string right = temps[1].Trim();
                pageReturn.LeftType = left[..left.IndexOf(' ')].Trim();
                pageReturn.LeftName = left[left.IndexOf(' ')..].Trim();
                pageReturn.RightType = right[..right.IndexOf(' ')].Trim();
                pageReturn.RightName = right[right.IndexOf(' ')..].Trim();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"解析代码失败:\r\n{notTaskReturnType}", ex);
            }
        }
        /// <summary>
        /// 分页返回类型
        /// </summary>
        private class PageReturnTypeModel
        {
            /// <summary>
            /// 左侧类型
            /// </summary>
            public string LeftType { get; set; } = string.Empty;
            /// <summary>
            /// 分页返回列表类型
            /// </summary>
            public string PageResultListType => LeftType[5..^1];
            /// <summary>
            /// 左侧名称
            /// </summary>
            public string LeftName { get; set; } = string.Empty;
            /// <summary>
            /// 右侧类型
            /// </summary>
            public string RightType { get; set; } = string.Empty;
            /// <summary>
            /// 右侧名称
            /// </summary>
            public string RightName { get; set; } = string.Empty;
        }
    }
}
