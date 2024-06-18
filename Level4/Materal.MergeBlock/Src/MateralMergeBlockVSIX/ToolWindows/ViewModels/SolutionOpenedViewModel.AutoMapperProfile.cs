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
        /// 创建AutoMapper规则
        /// </summary>
        [GeneratorCodeMethod]
        private async Task GeneratorAutoMapperProfileAsync()
        {
            foreach (DomainModel domain in Context.Domains)
            {
                await GeneratorAutoMapperProfileAsync(domain, Context.Domains);
            }
        }
        /// <summary>
        /// 创建AutoMapper规则
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        /// <param name="plugs"></param>
        private async Task GeneratorAutoMapperProfileAsync(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotAutoMapperAttribute>()) return;
            bool isGenerator = false;
            bool hasDTO = false;
            bool hasRequestModel = false;
            bool hasServicesModel = false;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorAutoMapperProfileAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Application.AutoMapperProfile");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}AutoMapper映射配置基类");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {domain.Name}ProfileBase : Profile");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 初始化");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        protected virtual void Init()");
            codeContent.AppendLine($"        {{");
            DomainModel targetDomain = domain.GetQueryDomain(domains);
            if (!domain.HasAttribute<NotAddAttribute>())
            {
                isGenerator = true;
                if (targetDomain != domain)
                {
                    codeContent.AppendLine($"            CreateMap<Add{domain.Name}Model, {targetDomain.Name}>();");
                }
                codeContent.AppendLine($"            CreateMap<Add{domain.Name}Model, {domain.Name}>();");
                hasServicesModel = true;
                if (!domain.HasAttribute<NotControllerAttribute>())
                {
                    codeContent.AppendLine($"            CreateMap<Add{domain.Name}RequestModel, Add{domain.Name}Model>();");
                    hasRequestModel = true;
                }
            }
            if (!domain.HasAttribute<NotEditAttribute>())
            {
                isGenerator = true;
                if (targetDomain != domain)
                {
                    codeContent.AppendLine($"            CreateMap<Edit{domain.Name}Model, {targetDomain.Name}>();");
                }
                codeContent.AppendLine($"            CreateMap<Edit{domain.Name}Model, {domain.Name}>();");
                hasServicesModel = true;
                if (!domain.HasAttribute<NotControllerAttribute>())
                {
                    codeContent.AppendLine($"            CreateMap<Edit{domain.Name}RequestModel, Edit{domain.Name}Model>();");
                    hasRequestModel = true;
                }
            }
            if (!domain.HasAttribute<NotQueryAttribute>() && !domain.HasAttribute<NotControllerAttribute>())
            {
                isGenerator = true;
                codeContent.AppendLine($"            CreateMap<Query{domain.Name}RequestModel, Query{domain.Name}Model>();");
                hasRequestModel = true;
                hasServicesModel = true;
            }
            if (targetDomain != domain)
            {
                isGenerator = true;
                codeContent.AppendLine($"            CreateMap<{targetDomain.Name}, {domain.Name}ListDTO>();");
                codeContent.AppendLine($"            CreateMap<{targetDomain.Name}, {domain.Name}DTO>();");
                hasDTO = true;
            }
            if (!domain.HasAttribute<NotListDTOAttribute>())
            {
                isGenerator = true;
                codeContent.AppendLine($"            CreateMap<{domain.Name}, {domain.Name}ListDTO>();");
                hasDTO = true;
            }
            if (!domain.HasAttribute<NotDTOAttribute>())
            {
                isGenerator = true;
                codeContent.AppendLine($"            CreateMap<{domain.Name}, {domain.Name}DTO>();");
                hasDTO = true;
            }
            if (domain.IsTreeDomain && !domain.HasAttribute<EmptyTreeAttribute>())
            {
                isGenerator = true;
                if (targetDomain != domain)
                {
                    codeContent.AppendLine($"            CreateMap<{targetDomain.Name}, {domain.Name}TreeListDTO>();");
                }
                codeContent.AppendLine($"            CreateMap<{domain.Name}, {domain.Name}TreeListDTO>();");
                if (!domain.HasAttribute<NotControllerAttribute>())
                {
                    codeContent.AppendLine($"            CreateMap<Query{domain.Name}TreeListRequestModel, Query{domain.Name}TreeListModel>();");
                }
            }
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}AutoMapper映射配置");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {domain.Name}Profile : {domain.Name}ProfileBase");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 构造方法");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public {domain.Name}Profile()");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            Init();");
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            if (!isGenerator) return;
            if (hasDTO || hasRequestModel || hasServicesModel)
            {
                codeContent.Insert(0, $"\r\n");
                if (hasDTO)
                {
                    codeContent.Insert(0, $"using {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name};\r\n");
                }
                if (hasRequestModel)
                {
                    codeContent.Insert(0, $"using {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name};\r\n");
                }
                if (hasServicesModel)
                {
                    codeContent.Insert(0, $"using {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name};\r\n");
                }
            }
            await codeContent.SaveAsAsync(Context, _moduleApplication, "AutoMapperProfile", $"{domain.Name}Profile.cs");
        }
    }
}
