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
        /// 创建请求模型
        /// </summary>
        [GeneratorCodeMethod]
        private async Task GeneratorRequestModelAsync()
        {
            foreach (DomainModel domain in Context.Domains)
            {
                await GeneratorAddRequestModelAsync(domain);
                await GeneratorEditRequestModelAsync(domain);
                await GeneratorQueryRequestModelAsync(domain, Context.Domains);
                await GeneratorTreeQueryRequestModelAsync(domain);
            }
        }
        /// <summary>
        /// 创建添加请求模型
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorAddRequestModelAsync(DomainModel domain)
        {
            if (domain.HasAttribute<NotAddAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorAddRequestModelAsync)}");
            codeContent.AppendLine($" */");
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
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "RequestModel", domain.Name, $"Add{domain.Name}RequestModel.cs");
        }
        /// <summary>
        /// 创建修改请求模型
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorEditRequestModelAsync(DomainModel domain)
        {
            if (domain.HasAttribute<NotEditAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorEditRequestModelAsync)}");
            codeContent.AppendLine($" */");
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
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "RequestModel", domain.Name, $"Edit{domain.Name}RequestModel.cs");
        }
        /// <summary>
        /// 创建查询请求模型
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        private async Task GeneratorQueryRequestModelAsync(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotQueryAttribute>()) return;
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorQueryRequestModelAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}查询请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Query{domain.Name}RequestModel : PageRequestModel, IQueryRequestModel");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in targetDomain.Properties)
            {
                if (!property.HasQueryAttribute || domain.HasAttribute<NotQueryAttribute>()) continue;
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
                    codeContent.AppendLine($"        public {property.NullPredefinedType} Min{property.Name} {{ get; set; }}");
                    if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
                    {
                        codeContent.AppendLine($"        /// <summary>");
                        codeContent.AppendLine($"        /// 最大{property.Annotation}");
                        codeContent.AppendLine($"        /// </summary>");
                    }
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
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "RequestModel", domain.Name, $"Query{domain.Name}RequestModel.cs");
        }
        /// <summary>
        /// 创建树查询请求模型
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorTreeQueryRequestModelAsync(DomainModel domain)
        {
            if (!(domain.IsTreeDomain && !domain.HasAttribute<EmptyTreeAttribute>()) || domain.HasAttribute<NotQueryAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorTreeQueryRequestModelAsync)}");
            codeContent.AppendLine($" */");
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
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "RequestModel", domain.Name, $"Query{domain.Name}TreeListRequestModel.cs");
        }
    }
}
