#nullable enable
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedControlViewModel : ObservableObject
    {
        /// <summary>
        /// 创建仓储代码
        /// </summary>
        /// <param name="domains"></param>
        private void GeneratorServicesCode(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorAddModel(domain);
                GeneratorIServicesCode(domain);
            }
        }
        private void GeneratorAddModel(DomainModel domain)
        {
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
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                string? verificationAttributesCode = property.GetVerificationAttributesCode();
                if(verificationAttributesCode is not null && string.IsNullOrWhiteSpace(verificationAttributesCode))
                {
                    codeContent.AppendLine(verificationAttributesCode);
                }
                codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }}");
                if(property.Initializer is not null && string.IsNullOrWhiteSpace(property.Initializer))
                {
                    codeContent.Insert(codeContent.Length - 2, $"  = {property.Initializer};");
                }
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Services", "Models", domain.Name, $"Add{domain.Name}Model.cs");
        }
        /// <summary>
        /// 创建服务代码
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
            codeContent.AppendLine($"    public partial interface I{domain.Name}Service : IBaseService<Add{domain.Name}Model, Edit{domain.Name}Model, Query{domain.Name}Model, {domain.Name}DTO, {domain.Name}ListDTO> {{ }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Services", $"I{domain.Name}Service.cs");
        }
    }
}
