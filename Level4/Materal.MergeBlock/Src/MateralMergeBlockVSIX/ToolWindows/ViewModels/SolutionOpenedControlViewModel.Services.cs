#nullable enable
using Materal.BaseCore.CodeGenerator;
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
        private void GeneratorOperationalModel(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorAddModel(domain);
                GeneratorEditModel(domain);
                GeneratorQueryModel(domain);
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
        private void GeneratorQueryModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotQueryAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}查询模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class Query{domain.Name}Model : PageRequestModel, IQueryServiceModel");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in domain.Properties)
            {
                if (property.HasAttribute<NotQueryAttribute>()) continue;
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
                if (property.Initializer is not null && !string.IsNullOrWhiteSpace(property.Initializer))
                {
                    codeContent.Insert(codeContent.Length - 2, $"  = {property.Initializer};");
                }
            }
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识组");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Contains(\"ID\")]");
            codeContent.AppendLine($"        public List<Guid>? IDs {{ get; set; }}");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 最大创建时间");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [LessThanOrEqual(\"CreateTime\")]");
            codeContent.AppendLine($"        public DateTime? MaxCreateTime {{ get; set; }}");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 最小创建时间");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [GreaterThanOrEqual(\"CreateTime\")]");
            codeContent.AppendLine($"        public DateTime? MinCreateTime {{ get; set; }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Services", "Models", domain.Name, $"Query{domain.Name}Model.cs");
        }
        /// <summary>
        /// 创建列表数据传输模型
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorDTOModel(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorListDTOModel(domain);
                GeneratorDTOModel(domain);
            }
        }
        /// <summary>
        /// 创建列表数据传输模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorListDTOModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotListDTOAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}列表数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {domain.Name}ListDTO : IListDTO");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Required(ErrorMessage = \"唯一标识为空\")]");
            codeContent.AppendLine($"        public Guid ID {{ get; set; }}");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 创建时间");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Required(ErrorMessage = \"创建时间为空\")]");
            codeContent.AppendLine($"        public DateTime CreateTime {{ get; set; }}");
            foreach (PropertyModel property in domain.Properties)
            {
                if (property.HasAttribute<NotListDTOAttribute>()) continue;
                GeneratorDTOModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "DTO", domain.Name, $"{domain.Name}ListDTO.cs");
        }
        /// <summary>
        /// 创建列表数据传输模型
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorDTOModel(DomainModel domain)
        {
            if (domain.HasAttribute<NotListDTOAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {domain.Name}DTO : {domain.Name}ListDTO, IDTO");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in domain.Properties)
            {
                if (property.HasAttribute<NotDTOAttribute>() || !property.HasAttribute<NotListDTOAttribute>()) continue;
                GeneratorDTOModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "DTO", domain.Name, $"{domain.Name}DTO.cs");
        }
        /// <summary>
        /// 创建DTO模型属性
        /// </summary>
        /// <param name="codeContent"></param>
        /// <param name="property"></param>
        private void GeneratorDTOModelProperty(StringBuilder codeContent, PropertyModel property)
        {
            if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
            }
            string? queryAttributesCode = property.GetVerificationAttributesCode();
            if (queryAttributesCode is not null && !string.IsNullOrWhiteSpace(queryAttributesCode))
            {
                codeContent.AppendLine($"        {queryAttributesCode}");
            }
            codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }}");
            if (property.Initializer is not null && !string.IsNullOrWhiteSpace(property.Initializer))
            {
                codeContent.Insert(codeContent.Length - 2, $"  = {property.Initializer};");
            }
        }
        /// <summary>
        /// 创建服务代码
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorServicesCode(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorIServicesCode(domain);
                GeneratorServiceImplsCode(domain);
            }
        }
        /// <summary>
        /// 创建服务代码接口
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorIServicesCode(DomainModel domain)
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.MergeBlock.Abstractions.Services;");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Services");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}服务");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<EmptyServiceAttribute>())
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Service : IBaseService");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Service : IBaseService<Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO>");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Services", $"I{domain.Name}Service.cs");
        }
        /// <summary>
        /// 创建服务代码实现
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorServiceImplsCode(DomainModel domain)
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.MergeBlock.Application.Services;");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Application.Services");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}服务");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<EmptyServiceAttribute>())
            {
                codeContent.AppendLine($"    public partial class {domain.Name}ServiceImpl(IServiceProvider serviceProvider) : BaseServiceImpl(serviceProvider), I{domain.Name}Service");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {domain.Name}ServiceImpl(IServiceProvider serviceProvider) : BaseServiceImpl<Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO, I{domain.Name}Repository, {domain.Name}>(serviceProvider), I{domain.Name}Service");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleApplication, "Services", $"{domain.Name}ServiceImpl.cs");
        }
    }
}
