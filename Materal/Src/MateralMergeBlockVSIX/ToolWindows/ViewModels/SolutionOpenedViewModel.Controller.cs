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
        /// 创建控制器代码
        /// </summary>
        [GeneratorCodeMethod]
        private async Task GeneratorControllerCodeAsync()
        {
            foreach (DomainModel domain in Context.Domains)
            {
                await GeneratorIControllerCodeAsync(domain);
                await GeneratorControllersCodeAsync(domain);
            }
        }
        /// <summary>
        /// 创建控制器代码接口
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorIControllerCodeAsync(DomainModel domain)
        {
            if (domain.HasAttribute<NotControllerAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorIControllerCodeAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Controllers");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}控制器");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<EmptyServiceAttribute, EmptyControllerAttribute>())
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Controller : IMergeBlockController");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Controller : IMergeBlockController<Add{domain.Name}RequestModel, Edit{domain.Name}RequestModel, Query{domain.Name}RequestModel, {domain.Name}DTO, {domain.Name}ListDTO>");
            }
            codeContent.AppendLine($"    {{");
            if (domain.IsIndexDomain && !domain.HasAttribute<EmptyIndexAttribute>())
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"requestModel\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpPut]");
                codeContent.AppendLine($"        Task<ResultModel> ExchangeIndexAsync(ExchangeIndexRequestModel requestModel);");
            }
            if (domain.IsTreeDomain && !domain.HasAttribute<EmptyTreeAttribute>())
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"requestModel\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpPut]");
                codeContent.AppendLine($"        Task<ResultModel> ExchangeParentAsync(ExchangeParentRequestModel requestModel);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 查询树列表");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"requestModel\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpPost]");
                codeContent.AppendLine($"        Task<ResultModel<List<{domain.Name}TreeListDTO>>> GetTreeListAsync(Query{domain.Name}TreeListRequestModel requestModel);");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "Controllers", $"I{domain.Name}Controller.cs");
        }
        /// <summary>
        /// 创建控制器代码实现
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorControllersCodeAsync(DomainModel domain)
        {
            if (domain.HasAttribute<NotControllerAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorControllersCodeAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Application.Controllers");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}控制器");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<EmptyServiceAttribute, EmptyControllerAttribute>())
            {
                if (domain.HasAttribute<NotServiceAttribute>())
                {
                    codeContent.AppendLine($"    public partial class {domain.Name}Controller : {_moduleName}Controller, I{domain.Name}Controller");
                }
                else
                {
                    codeContent.AppendLine($"    public partial class {domain.Name}Controller : {_moduleName}Controller<I{domain.Name}Service>, I{domain.Name}Controller");
                }
            }
            else
            {
                codeContent.AppendLine($"    public partial class {domain.Name}Controller : {_moduleName}Controller<Add{domain.Name}RequestModel, Edit{domain.Name}RequestModel, Query{domain.Name}RequestModel, Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO, I{domain.Name}Service>, I{domain.Name}Controller");
            }
            codeContent.AppendLine($"    {{");
            if (domain.IsIndexDomain && !domain.HasAttribute<EmptyIndexAttribute>())
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"requestModel\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpPut]");
                codeContent.AppendLine($"        public async Task<ResultModel> ExchangeIndexAsync(ExchangeIndexRequestModel requestModel)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            ExchangeIndexModel model = Mapper.Map<ExchangeIndexModel>(requestModel) ?? throw new {_projectName}Exception(\"映射失败\");");
                codeContent.AppendLine($"            await DefaultService.ExchangeIndexAsync(model);");
                codeContent.AppendLine($"            return ResultModel.Success(\"交换位序成功\");");
                codeContent.AppendLine($"        }}");
            }
            if (domain.IsTreeDomain && !domain.HasAttribute<EmptyTreeAttribute>())
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"requestModel\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpPut]");
                codeContent.AppendLine($"        public async Task<ResultModel> ExchangeParentAsync(ExchangeParentRequestModel requestModel)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            ExchangeParentModel model = Mapper.Map<ExchangeParentModel>(requestModel) ?? throw new {_projectName}Exception(\"映射失败\");");
                codeContent.AppendLine($"            await DefaultService.ExchangeParentAsync(model);");
                codeContent.AppendLine($"            return ResultModel.Success(\"更改父级成功\");");
                codeContent.AppendLine($"        }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 查询树列表");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"requestModel\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpPost]");
                codeContent.AppendLine($"        public async Task<ResultModel<List<{domain.Name}TreeListDTO>>> GetTreeListAsync(Query{domain.Name}TreeListRequestModel requestModel)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            Query{domain.Name}TreeListModel model = Mapper.Map<Query{domain.Name}TreeListModel>(requestModel) ?? throw new {_projectName}Exception(\"映射失败\");");
                codeContent.AppendLine($"            List<{domain.Name}TreeListDTO> result = await DefaultService.GetTreeListAsync(model);");
                codeContent.AppendLine($"            return ResultModel<List<{domain.Name}TreeListDTO>>.Success(result, \"查询成功\");");
                codeContent.AppendLine($"        }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleApplication, "Controllers", $"{domain.Name}Controller.cs");
        }
    }
}
