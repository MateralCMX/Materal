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
        /// 创建数据传输模型
        /// </summary>
        [GeneratorCodeMethod]
        private async Task GeneratorDTOModelAsync()
        {
            foreach (DomainModel domain in Context.Domains)
            {
                await GeneratorListDTOModelAsync(domain, Context.Domains);
                await GeneratorDTOModelAsync(domain, Context.Domains);
                await GeneratorTreeListDTOModelAsync(domain);
            }
        }
        /// <summary>
        /// 创建列表数据传输模型
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        private async Task GeneratorListDTOModelAsync(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotListDTOAttribute>()) return;
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorListDTOModelAsync)}");
            codeContent.AppendLine($" */");
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
            foreach (PropertyModel property in targetDomain.Properties)
            {
                if (property.HasAttribute<NotDTOAttribute>() || property.HasAttribute<NotListDTOAttribute>()) continue;
                GeneratorDTOModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "DTO", domain.Name, $"{domain.Name}ListDTO.cs");
        }
        /// <summary>
        /// 创建数据传输模型
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        private async Task GeneratorDTOModelAsync(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotDTOAttribute>()) return;
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorDTOModelAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {domain.Name}DTO : {domain.Name}ListDTO, IDTO");
            codeContent.AppendLine($"    {{");
            foreach (PropertyModel property in targetDomain.Properties)
            {
                if (property.HasAttribute<NotDTOAttribute>() || !property.HasAttribute<NotListDTOAttribute>()) continue;
                GeneratorDTOModelProperty(codeContent, property);
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "DTO", domain.Name, $"{domain.Name}DTO.cs");
        }
        /// <summary>
        /// 创建树列表数据传输模型
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorTreeListDTOModelAsync(DomainModel domain)
        {
            if (!(domain.IsTreeDomain && !domain.HasAttribute<EmptyTreeAttribute>()) || domain.HasAttribute<NotListDTOAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorTreeListDTOModelAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}树列表数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {domain.Name}TreeListDTO : {domain.Name}ListDTO, ITreeDTO<{domain.Name}TreeListDTO>");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 子级");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public List<{domain.Name}TreeListDTO> Children {{ get; set; }} = [];");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "DTO", domain.Name, $"{domain.Name}TreeListDTO.cs");
        }
        /// <summary>
        /// 创建数据传输模型属性
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
                codeContent.Insert(codeContent.Length - 2, $" = {property.Initializer};");
            }
            if (property.HasAttribute<DTOTextAttribute>())
            {
                if (property.Annotation is not null && !string.IsNullOrWhiteSpace(property.Annotation))
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// {property.Annotation}文本");
                    codeContent.AppendLine($"        /// </summary>");
                }
                if (property.CanNull)
                {
                    codeContent.AppendLine($"        public string? {property.Name}Text => {property.Name}?.GetDescriptionOrNull();");
                }
                else
                {
                    codeContent.AppendLine($"        public string {property.Name}Text => {property.Name}.GetDescription();");
                }
            }
        }
    }
}
