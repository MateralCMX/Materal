#nullable enable
using Materal.MergeBlock.GeneratorCode;
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedControlViewModel : ObservableObject
    {
        /// <summary>
        /// 创建操作模型
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorOperationalRequestModel(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorAddRequestModel(domain);
                GeneratorEditRequestModel(domain);
                GeneratorQueryRequestModel(domain, domains);
                GeneratorTreeQueryRequestModel(domain);
            }
        }
        /// <summary>
        /// 创建添加模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorAddRequestModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotServiceAttribute, NotControllerAttribute, ViewAttribute, NotAddAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}添加请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Add{domain.Name}RequestModel : IAddRequestModel");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in domain.Properties)
            {
                if (property.HasAttribute<NotAddAttribute>()) continue;
                GeneratorOperationalModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "RequestModel", domain.Name, $"Add{domain.Name}RequestModel.cs");
        }
        /// <summary>
        /// 创建修改模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorEditRequestModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotServiceAttribute, NotControllerAttribute, ViewAttribute, NotEditAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}修改请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Edit{domain.Name}RequestModel : IEditRequestModel");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Required(ErrorMessage = \"唯一标识为空\")]");
            codeContent.AppendLine($"        public Guid ID {{ get; set; }}");
            foreach (PropertyModel property in domain.Properties)
            {
                if (property.HasAttribute<NotEditAttribute>()) continue;
                GeneratorOperationalModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "RequestModel", domain.Name, $"Edit{domain.Name}RequestModel.cs");
        }
        /// <summary>
        /// 创建查询模型
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        private void GeneratorQueryRequestModel(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotServiceAttribute, NotControllerAttribute, ViewAttribute, NotQueryAttribute>()) return;
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}查询请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Query{domain.Name}RequestModel : PageRequestModel, IQueryRequestModel");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in targetDomain.Properties)
            {
                if (property.HasAttribute<NotQueryAttribute>()) continue;
                if (!property.HasAttribute<BetweenAttribute>())
                {
                    if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
                    {
                        codeContent.AppendLine($"        /// <summary>");
                        codeContent.AppendLine($"        /// {property.Annotation}");
                        codeContent.AppendLine($"        /// </summary>");
                    }
                    codeContent.AppendLine($"        public {property.NullPredefinedType} {property.Name} {{ get; set; }}");
                }
                else
                {
                    if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
                    {
                        codeContent.AppendLine($"        /// <summary>");
                        codeContent.AppendLine($"        /// 最小{property.Annotation}");
                        codeContent.AppendLine($"        /// </summary>");
                    }
                    codeContent.AppendLine($"        [GreaterThanOrEqual(\"{property.Name}\")]");
                    codeContent.AppendLine($"        public {property.NullPredefinedType} Min{property.Name} {{ get; set; }}");
                    if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
                    {
                        codeContent.AppendLine($"        /// <summary>");
                        codeContent.AppendLine($"        /// 最大{property.Annotation}");
                        codeContent.AppendLine($"        /// </summary>");
                    }
                    codeContent.AppendLine($"        [LessThanOrEqual(\"{property.Name}\")]");
                    codeContent.AppendLine($"        public {property.NullPredefinedType} Max{property.Name} {{ get; set; }}");
                }
            }
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识组");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public List<Guid>? IDs {{ get; set; }}");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 最小创建时间");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public DateTime? MinCreateTime {{ get; set; }}");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 最大创建时间");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public DateTime? MaxCreateTime {{ get; set; }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "RequestModel", domain.Name, $"Query{domain.Name}RequestModel.cs");
        }
        /// <summary>
        /// 创建树查询模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorTreeQueryRequestModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotServiceAttribute, NotControllerAttribute, ViewAttribute, NotQueryAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}树查询请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Query{domain.Name}TreeListRequestModel : FilterModel");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 父级唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public Guid? ParentID {{ get; set; }}");
            PropertyModel? treePropertyModel = domain.GetTreeGroupProperty();
            if (treePropertyModel is not null)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {treePropertyModel.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        [Equal]");
                codeContent.AppendLine($"        public {treePropertyModel.NullPredefinedType} {treePropertyModel.Name} {{ get; set; }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "RequestModel", domain.Name, $"Query{domain.Name}TreeListRequestModel.cs");
        }
        /// <summary>
        /// 创建控制器代码
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorControllerCode(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorIControllerCode(domain);
                GeneratorControllersCode(domain);
            }
        }
        /// <summary>
        /// 创建控制器代码接口
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorIControllerCode(DomainModel domain)
        {
            if (domain.HasAttribute<NotServiceAttribute, NotControllerAttribute, ViewAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Controllers");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}控制器");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<EmptyServiceAttribute, EmptyControllerAttribute>())
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Controller : IMergeBlockControllerBase");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Controller : IMergeBlockControllerBase<Add{domain.Name}RequestModel, Edit{domain.Name}RequestModel, Query{domain.Name}RequestModel, Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO, I{domain.Name}Service>");
            }
            codeContent.AppendLine($"    {{");

            if (domain.IsIndexDomain)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"requestModel\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpPut]");
                codeContent.AppendLine($"        Task<ResultModel> ExchangeIndexAsync(ExchangeIndexRequestModel requestModel);");
            }
            if (domain.IsTreeDomain)
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
            codeContent.SaveAs(_moduleAbstractions, "Controllers", $"I{domain.Name}Controller.cs");
        }
        /// <summary>
        /// 创建控制器代码实现
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorControllersCode(DomainModel domain)
        {
            if (domain.HasAttribute<NotServiceAttribute, NotControllerAttribute, ViewAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Application.Controllers");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}控制器");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<EmptyServiceAttribute>())
            {
                codeContent.AppendLine($"    public partial class {domain.Name}Controller : MergeBlockControllerBase, I{domain.Name}Controller");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {domain.Name}Controller : MergeBlockControllerBase<Add{domain.Name}RequestModel, Edit{domain.Name}RequestModel, Query{domain.Name}RequestModel, Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO, I{domain.Name}Service>, I{domain.Name}Controller");
            }
            codeContent.AppendLine($"    {{");
            if (domain.IsIndexDomain)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"requestModel\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpPut]");
                codeContent.AppendLine($"        public async Task<ResultModel> ExchangeIndexAsync(ExchangeIndexRequestModel requestModel)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            ExchangeIndexModel model = Mapper.Map<ExchangeIndexModel>(requestModel);");
                codeContent.AppendLine($"            await DefaultService.ExchangeIndexAsync(model);");
                codeContent.AppendLine($"            return ResultModel.Success(\"交换位序成功\");");
                codeContent.AppendLine($"        }}");
            }
            if (domain.IsTreeDomain)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"requestModel\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpPut]");
                codeContent.AppendLine($"        public async Task<ResultModel> ExchangeParentAsync(ExchangeParentRequestModel requestModel)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            ExchangeParentModel model = Mapper.Map<ExchangeParentModel>(requestModel);");
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
                codeContent.AppendLine($"            Query{domain.Name}TreeListModel model = Mapper.Map<Query{domain.Name}TreeListModel>(requestModel);");
                codeContent.AppendLine($"            List<{domain.Name}TreeListDTO> result = await DefaultService.GetTreeListAsync(model);");
                codeContent.AppendLine($"            return ResultModel<List<{domain.Name}TreeListDTO>>.Success(result, \"查询成功\");");
                codeContent.AppendLine($"        }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleApplication, "Controllers", $"{domain.Name}Controller.cs");
        }
    }
}
