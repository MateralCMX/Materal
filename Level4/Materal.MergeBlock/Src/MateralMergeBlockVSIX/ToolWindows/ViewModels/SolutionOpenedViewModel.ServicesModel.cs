#nullable enable
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedViewModel : ObservableObject
    {
        /// <summary>
        /// 创建操作模型
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorOperationalModel(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorAddModel(domain);
                GeneratorEditModel(domain);
                GeneratorQueryModel(domain, domains);
                GeneratorTreeQueryModel(domain);
            }
        }
        /// <summary>
        /// 创建添加模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorAddModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotAddAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorAddModel)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}添加模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Add{domain.Name}Model : IAddServiceModel");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in domain.Properties)
            {
                if (property.HasAttribute<NotAddAttribute>()) continue;
                GeneratorOperationalModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Services", "Models", domain.Name, $"Add{domain.Name}Model.cs");
        }
        /// <summary>
        /// 创建修改模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorEditModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotEditAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorEditModel)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}修改模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Edit{domain.Name}Model : IEditServiceModel");
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
            codeContent.SaveAs(_moduleAbstractions, "Services", "Models", domain.Name, $"Edit{domain.Name}Model.cs");
        }
        /// <summary>
        /// 创建查询模型
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        private void GeneratorQueryModel(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotQueryAttribute>()) return;
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorQueryModel)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}查询模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Query{domain.Name}Model : PageRequestModel, IQueryServiceModel");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in targetDomain.Properties)
            {
                if (domain.HasAttribute<NotQueryAttribute>() || !property.HasQueryAttribute) continue;
                if (property.HasAttribute<BetweenAttribute>())
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
                else
                {
                    if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
                    {
                        codeContent.AppendLine($"        /// <summary>");
                        codeContent.AppendLine($"        /// {property.Annotation}");
                        codeContent.AppendLine($"        /// </summary>");
                    }
                    string? queryAttributesCode = property.GetQueryAttributesCode();
                    if (queryAttributesCode is not null && !string.IsNullOrWhiteSpace(queryAttributesCode))
                    {
                        codeContent.AppendLine($"        {queryAttributesCode}");
                    }
                    codeContent.AppendLine($"        public {property.NullPredefinedType} {property.Name} {{ get; set; }}");
                }
            }
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识组");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Contains(\"ID\")]");
            codeContent.AppendLine($"        public List<Guid>? IDs {{ get; set; }}");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 最小创建时间");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [GreaterThanOrEqual(\"CreateTime\")]");
            codeContent.AppendLine($"        public DateTime? MinCreateTime {{ get; set; }}");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 最大创建时间");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [LessThanOrEqual(\"CreateTime\")]");
            codeContent.AppendLine($"        public DateTime? MaxCreateTime {{ get; set; }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Services", "Models", domain.Name, $"Query{domain.Name}Model.cs");
        }
        /// <summary>
        /// 创建树查询模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorTreeQueryModel(DomainModel domain)
        {
            if (!domain.IsTreeDomain || domain.HasAttribute<NotQueryAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorTreeQueryModel)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}树查询模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Query{domain.Name}TreeListModel : FilterModel");
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
            codeContent.SaveAs(_moduleAbstractions, "Services", "Models", domain.Name, $"Query{domain.Name}TreeListModel.cs");
        }
    }
}
