#nullable enable
using Materal.MergeBlock.GeneratorCode;
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedControlViewModel : ObservableObject
    {
        /// <summary>
        /// 创建AutoMapper规则
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorAutoMapperProfile(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorAutoMapperProfile(domain, domains);
            }
        }
        /// <summary>
        /// 创建AutoMapper规则
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="domains"></param>
        private void GeneratorAutoMapperProfile(DomainModel domain, List<DomainModel> domains)
        {
            if (domain.HasAttribute<NotServiceAttribute, ViewAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.DTO.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.RequestModel.{domain.Name};");
            codeContent.AppendLine($"using {_projectName}.{_moduleName}.Abstractions.Services.Models.{domain.Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.AutoMapperProfile");
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
            DomainModel targetDomain =  domain.GetQueryDomain(domains);
            if (!domain.HasAttribute<NotAddAttribute>())
            {
                if(targetDomain != domain)
                {
                    codeContent.AppendLine($"            CreateMap<Add{domain.Name}Model, {targetDomain.Name}>();");
                }
                codeContent.AppendLine($"            CreateMap<Add{domain.Name}Model, {domain.Name}>();");
                if (!domain.HasAttribute<NotControllerAttribute>())
                {
                    codeContent.AppendLine($"            CreateMap<Add{domain.Name}RequestModel, Add{domain.Name}Model>();");
                }
            }
            if (!domain.HasAttribute<NotEditAttribute>())
            {
                if (targetDomain != domain)
                {
                    codeContent.AppendLine($"            CreateMap<Edit{domain.Name}Model, {targetDomain.Name}>();");
                }
                codeContent.AppendLine($"            CreateMap<Edit{domain.Name}Model, {domain.Name}>();");
                if (!domain.HasAttribute<NotControllerAttribute>())
                {
                    codeContent.AppendLine($"            CreateMap<Edit{domain.Name}RequestModel, Edit{domain.Name}Model>();");
                }
            }
            if (!domain.HasAttribute<NotQueryAttribute>() && !domain.HasAttribute<NotControllerAttribute>())
            {
                codeContent.AppendLine($"            CreateMap<Query{domain.Name}RequestModel, Query{domain.Name}Model>();");
            }
            if (targetDomain != domain)
            {
                codeContent.AppendLine($"            CreateMap<{targetDomain.Name}, {domain.Name}ListDTO>();");
                codeContent.AppendLine($"            CreateMap<{targetDomain.Name}, {domain.Name}DTO>();");
            }
            codeContent.AppendLine($"            CreateMap<{domain.Name}, {domain.Name}ListDTO>();");
            codeContent.AppendLine($"            CreateMap<{domain.Name}, {domain.Name}DTO>();");
            if (domain.IsTreeDomain)
            {
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
            codeContent.SaveAs(_moduleApplication, "AutoMapperProfile", $"{domain.Name}Profile.cs");
        }
    }
}
