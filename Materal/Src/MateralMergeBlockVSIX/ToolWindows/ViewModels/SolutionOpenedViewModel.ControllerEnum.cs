#nullable enable
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedViewModel : ObservableObject
    {
        /// <summary>
        /// 生成枚举控制器
        /// </summary>
        [GeneratorCodeAfterMethod]
        private async Task GeneratorEnumControllerAsync()
        {
            if (Context.Enums.Count <= 0) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorEnumControllerAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"using Microsoft.AspNetCore.Authorization;");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.Enums;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.HttpClient");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 枚举控制器");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    [AllowAnonymous]");
            codeContent.AppendLine($"    public partial class EnumsController : {_moduleName}Controller");
            codeContent.AppendLine($"    {{");
            foreach (EnumModel @enum in Context.Enums)
            {
                if (@enum.HasAttribute<NotControllerAttribute>()) return;
                string annotation = $"        /// 获取所有{@enum.Annotation}";
                if (!annotation.EndsWith("枚举"))
                {
                    annotation += "枚举";
                }
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine(annotation);
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpGet]");
                codeContent.AppendLine($"        public ResultModel<List<KeyValueModel<{@enum.Name}>>> GetAll{@enum.Name}()");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            List<KeyValueModel<{@enum.Name}>> result = KeyValueModel<{@enum.Name}>.GetAllCode();");
                codeContent.AppendLine($"            return ResultModel<List<KeyValueModel<{@enum.Name}>>>.Success(result, \"获取成功\");");
                codeContent.AppendLine($"        }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleApplication, "Controllers", $"EnumsController.cs");
        }
    }
}
