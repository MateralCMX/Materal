using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using RC.InGenerator.Attribtues;
using RC.InGenerator.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RC.InGenerator.Models
{
    public class DomainModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; private set; } = string.Empty;
        /// <summary>
        /// 其他引用项
        /// </summary>
        public List<string> OtherUsing { get; private set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; private set; }
        /// <summary>
        /// 属性
        /// </summary>
        public List<DomainPropertyModel> Properties { get; private set; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; private set; }
        /// <summary>
        /// 使用缓存
        /// </summary>
        public bool UseCache { get; private set; }
        /// <summary>
        /// 生成默认服务
        /// </summary>
        public bool GeneratorDefaultService { get; private set; }
        /// <summary>
        /// 生成查询目标服务
        /// </summary>
        public bool GeneratorQueryTargetService => !string.IsNullOrWhiteSpace(ViewDomainName);
        /// <summary>
        /// 生成WebAPI
        /// </summary>
        public bool GeneratorWebAPI { get; private set; }
        /// <summary>
        /// 生成待服务的WebAPI
        /// </summary>
        public bool GeneratorServiceWebAPI { get; private set; }
        /// <summary>
        /// 生成服务
        /// </summary>
        public bool GeneratorService { get; private set; }
        /// <summary>
        /// 生成查询模型
        /// </summary>
        public bool GeneratorQueryModel { get; private set; }
        /// <summary>
        /// 扩展查询生成
        /// </summary>
        public bool ExtendQueryGenerator { get; private set; }
        public SourceProductionContext Context { get; }
        public ProjectModel Project { get; }
        #region 文件名称
        /// <summary>
        /// 视图Domain名称
        /// </summary>
        public string? ViewDomainName { get; private set; }
        /// <summary>
        /// 视图仓储接口名称
        /// </summary>
        public string? IViewRepositoryName => $"I{ViewDomainName}Repository";
        /// <summary>
        /// 仓储接口名称
        /// </summary>
        public string IRepositoryName => $"I{Name}Repository";
        /// <summary>
        /// 仓储实现名称
        /// </summary>
        public string RepositoryImplName => $"{Name}RepositoryImpl";
        /// <summary>
        /// 实体配置名称
        /// </summary>
        public string EntityConfigName => $"{Name}Config";
        /// <summary>
        /// 添加模型名称
        /// </summary>
        public string AddModelName => $"Add{Name}Model";
        /// <summary>
        /// 修改模型名称
        /// </summary>
        public string EditModelName => $"Edit{Name}Model";
        /// <summary>
        /// 查询模型名称
        /// </summary>
        public string QueryModelName => $"Query{Name}Model";
        /// <summary>
        /// 数据传输模型名称
        /// </summary>
        public string DTOName => $"{Name}DTO";
        /// <summary>
        /// 列表数据传输模型名称
        /// </summary>
        public string ListDTOName => $"{Name}ListDTO";
        /// <summary>
        /// 服务名称
        /// </summary>
        public string IServiceName => $"I{Name}Service";
        /// <summary>
        /// 服务实现名称
        /// </summary>
        public string ServiceImplName => $"{Name}ServiceImpl";
        /// <summary>
        /// AutoMapper配置文件名称
        /// </summary>
        public string AutoMapperProfileName => $"{Name}Profile";
        /// <summary>
        /// 添加请求模型名称
        /// </summary>
        public string AddRequestModelName => $"Add{Name}RequestModel";
        /// <summary>
        /// 修改请求模型名称
        /// </summary>
        public string EditRequestModelName => $"Edit{Name}RequestModel";
        /// <summary>
        /// 查询请求模型名称
        /// </summary>
        public string QueryRequestModelName => $"Query{Name}RequestModel";
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName => $"{Name}Controller";
        #endregion
        public DomainModel(SourceProductionContext context, ClassDeclarationSyntax classDeclaration, ProjectModel project)
        {
            Context = context;
            Project = project;
            Name = classDeclaration.Identifier.ValueText;
            Annotation = classDeclaration.GetAnnotation();
            if (classDeclaration.Parent is NamespaceDeclarationSyntax namespaceDeclaration)
            {
                Namespace = namespaceDeclaration.Name.ToString();
            }
            OtherUsing = GetOtherUsing(classDeclaration);
            Attributes = classDeclaration.AttributeLists.GetAttributeSyntaxes();
            UseCache = Attributes.HasAttribute<CacheAttribute>();
            GeneratorDefaultService = !Attributes.HasAttribute<NotDefaultServiceGeneratorAttribute>();
            ExtendQueryGenerator = !Attributes.HasAttribute<NotExtendQueryGeneratorAttribute>();
            AttributeModel? viewAttributeModel = Attributes.GetAttribute<QueryTragetGeneratorAttribute>();
            if(viewAttributeModel != null && viewAttributeModel.AttributeArguments.Count > 0 && string.IsNullOrWhiteSpace(viewAttributeModel.AttributeArguments[0].Target))
            {
                ViewDomainName = viewAttributeModel.AttributeArguments[0].Value;
            }
            GeneratorService = !Attributes.HasAttribute<NotServiceGeneratorAttribute>();
            GeneratorQueryModel = !Attributes.HasAttribute<NotServiceAndQueryGeneratorAttribute>();
            if (!GeneratorQueryModel)
            {
                GeneratorService = false;
            }
            GeneratorWebAPI = !Attributes.HasAttribute<NotWebAPIGeneratorAttribute>();
            GeneratorServiceWebAPI = !Attributes.HasAttribute<NotWebAPIServiceGeneratorAttribute>();
            Properties = new List<DomainPropertyModel>();
            foreach (MemberDeclarationSyntax item in classDeclaration.Members)
            {
                if (item is not PropertyDeclarationSyntax propertyDeclaration) continue;
                Properties.Add(new DomainPropertyModel(propertyDeclaration));
            }
        }
        /// <summary>
        /// 初始化引用
        /// </summary>
        /// <param name="classDeclaration"></param>
        private static List<string> GetOtherUsing(ClassDeclarationSyntax classDeclaration)
        {
            List<string> result = new();
            SyntaxNode? tempNode = classDeclaration.Parent;
            while (tempNode != null && tempNode.Parent != null)
            {
                tempNode = tempNode.Parent;
            }
            if (tempNode is CompilationUnitSyntax compilation)
            {
                var blackList = new[]
                {
                    "Materal.Model",
                    "System.ComponentModel.DataAnnotations",
                    "RC.Core.Domain",
                    "RC.Core.Models",
                };
                if (compilation.Usings != null && compilation.Usings.Count > 0)
                {
                    foreach (var item in compilation.Usings)
                    {
                        string usingName = item.Name.ToString();
                        if (blackList.Contains(usingName)) continue;
                        result.Add(usingName);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        public void CreateFiles()
        {
            CreateIRepositoryFile();
            CreateRepositoryImplFile();
            CreateEntityConfigFile();
            if (GeneratorService || GeneratorQueryModel)
            {
                CreateAutoMapperProfile();
            }
            if (GeneratorQueryModel)
            {
                CreateQueryModelFile();
                CreateQueryRequestModelFile();
                CreateListDTOFile();
                CreateDTOFile();
            }
            if (!GeneratorService) return;
            CreateAddModelFile();
            CreateEditModelFile();
            CreateIServiceFile();
            CreateServiceImplFile();
            if (!GeneratorWebAPI) return;
            CreateAddRequestModelFile();
            CreateEditRequestModelFile();
            CreateWebAPIControllerFile();
        }
        /// <summary>
        /// 创建WebAPI控制器文件
        /// </summary>
        private void CreateWebAPIControllerFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using AutoMapper;");
            codeContent.AppendLine($"using RC.Core.WebAPI.Controllers;");
            codeContent.AppendLine($"using {Project.DataTransmitModelNamespace}.{Name};");
            codeContent.AppendLine($"using {Project.PresentationModelNamespace}.{Name};");
            codeContent.AppendLine($"using {Project.ServiceNamespace};");
            codeContent.AppendLine($"using {Project.ServiceNamespace}.Models.{Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.WebAPINamespace}.Controllers");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}控制器");
            codeContent.AppendLine($"    /// </summary>");
            if (GeneratorServiceWebAPI && GeneratorDefaultService)
            {
                codeContent.AppendLine($"    public partial class {ControllerName} : RCWebAPIServiceControllerBase<{AddRequestModelName}, {EditRequestModelName}, {QueryRequestModelName}, {AddModelName}, {EditModelName}, {QueryModelName}, {DTOName}, {ListDTOName}, {IServiceName}>");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {ControllerName} : RCWebAPIControllerBase");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{ControllerName}.g.cs", sourceText);
        }
        /// <summary>
        /// 创建查询请求文件
        /// </summary>
        private void CreateQueryRequestModelFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using Materal.Model;");
            codeContent.AppendLine($"using RC.Core.Models;");
            codeContent.AppendLine($"using RC.Core.PresentationModel;");
            DomainModel? targetDomain;
            if (GeneratorQueryTargetService)
            {
                targetDomain = Project.Domains.FirstOrDefault(m => m.Name == ViewDomainName) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            if (targetDomain.OtherUsing != null && targetDomain.OtherUsing.Count > 0)
            {
                foreach (string usingName in targetDomain.OtherUsing)
                {
                    codeContent.AppendLine($"using {usingName};");
                }
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.PresentationModelNamespace}.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}查询请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {QueryRequestModelName} : PageRequestModel, IQueryRequestModel, IRequestModel");
            codeContent.AppendLine($"    {{");
            FillQueryRequestModelProperties(targetDomain, codeContent);
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{QueryRequestModelName}.g.cs", sourceText);
        }
        /// <summary>
        /// 填充查询请求模型属性
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="codeContent"></param>
        private static void FillQueryRequestModelProperties(DomainModel domain, StringBuilder codeContent)
        {
            foreach (DomainPropertyModel property in domain.Properties)
            {
                if (property.UseQuery)
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// {property.Annotation}");
                    codeContent.AppendLine($"        /// </summary>");
                    codeContent.AppendLine($"        public {property.NullPredefinedType} {property.Name} {{ get; set; }}");
                }
                if (property.IsBetween)
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// 最大{property.Annotation}");
                    codeContent.AppendLine($"        /// </summary>");
                    codeContent.AppendLine($"        public {property.NullPredefinedType} Max{property.Name} {{ get; set; }}");
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// 最小{property.Annotation}");
                    codeContent.AppendLine($"        /// </summary>");
                    codeContent.AppendLine($"        public {property.NullPredefinedType} Min{property.Name} {{ get; set; }}");
                }
            }
            if (domain.ExtendQueryGenerator)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 唯一标识组");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public List<Guid>? IDs {{ get; set; }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 最大创建时间");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public DateTime? MaxCreateTime {{ get; set; }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 最小创建时间");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public DateTime? MinCreateTime {{ get; set; }}");
            }
        }
        /// <summary>
        /// 创建修改请求文件
        /// </summary>
        private void CreateEditRequestModelFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using RC.Core.Models;");
            codeContent.AppendLine($"using RC.Core.PresentationModel;");
            if (OtherUsing != null && OtherUsing.Count > 0)
            {
                foreach (string usingName in OtherUsing)
                {
                    codeContent.AppendLine($"using {usingName};");
                }
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.PresentationModelNamespace}.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}修改请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {EditRequestModelName} : IEditRequestModel, IRequestModel");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Required(ErrorMessage = \"唯一标识为空\")]");
            codeContent.AppendLine($"        public Guid ID {{ get; set; }}");
            foreach (DomainPropertyModel property in Properties)
            {
                if (!property.CanEdit) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                if (property.UseValidation)
                {
                    codeContent.AppendLine($"        {property.ValidationAttribute}");
                }
                codeContent.AppendLine($"        {property.Modifier} {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{EditRequestModelName}.g.cs", sourceText);
        }
        /// <summary>
        /// 创建添加请求文件
        /// </summary>
        private void CreateAddRequestModelFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using RC.Core.PresentationModel;");
            codeContent.AppendLine($"using RC.Core.Models;");
            if (OtherUsing != null && OtherUsing.Count > 0)
            {
                foreach (string usingName in OtherUsing)
                {
                    codeContent.AppendLine($"using {usingName};");
                }
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.PresentationModelNamespace} .{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}添加请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {AddRequestModelName} : IRequestModel");
            codeContent.AppendLine($"    {{");
            foreach (DomainPropertyModel property in Properties)
            {
                if (!property.CanAdd) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                if (property.UseValidation)
                {
                    codeContent.AppendLine($"        {property.ValidationAttribute}");
                }
                codeContent.AppendLine($"        {property.Modifier} {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{AddRequestModelName}.g.cs", sourceText);
        }
        /// <summary>
        /// 创建AutoMapper配置文件
        /// </summary>
        private void CreateAutoMapperProfile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using AutoMapper;");
            if((GeneratorService && GeneratorWebAPI) || GeneratorQueryModel)
            {
                codeContent.AppendLine($"using {Project.PresentationModelNamespace}.{Name};");
            }
            codeContent.AppendLine($"using {Project.ServiceNamespace}.Models.{Name};");
            if (GeneratorQueryModel)
            {
                codeContent.AppendLine($"using {Project.DataTransmitModelNamespace}.{Name};");
            }
            codeContent.AppendLine($"using {Project.DomainNamespace};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.Namespace}.AutoMapperProfile");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}AutoMapper映射配置基类");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {AutoMapperProfileName}Base : Profile");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 初始化");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        protected virtual void Init()");
            codeContent.AppendLine($"        {{");
            if (GeneratorService)
            {
                codeContent.AppendLine($"            CreateMap<{AddModelName}, {Name}>();");
                codeContent.AppendLine($"            CreateMap<{EditModelName}, {Name}>();");
                if (GeneratorWebAPI)
                {
                    codeContent.AppendLine($"            CreateMap<{AddRequestModelName}, {AddModelName}>();");
                    codeContent.AppendLine($"            CreateMap<{EditRequestModelName}, {EditModelName}>();");
                }
            }
            if (GeneratorQueryModel)
            {
                if (GeneratorQueryTargetService)
                {
                    codeContent.AppendLine($"            CreateMap<{ViewDomainName}, {ListDTOName}>();");
                    codeContent.AppendLine($"            CreateMap<{ViewDomainName}, {DTOName}>();");
                }
                codeContent.AppendLine($"            CreateMap<{Name}, {ListDTOName}>();");
                codeContent.AppendLine($"            CreateMap<{Name}, {DTOName}>();");
                codeContent.AppendLine($"            CreateMap<{QueryRequestModelName}, {QueryModelName}>();");
            }
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}AutoMapper映射配置");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {AutoMapperProfileName} : {AutoMapperProfileName}Base");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 构造方法");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public {AutoMapperProfileName}()");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            Init();");
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{AutoMapperProfileName}.g.cs", sourceText);
        }
        /// <summary>
        /// 创建服务文件
        /// </summary>
        private void CreateServiceImplFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using AutoMapper;");
            codeContent.AppendLine($"using Materal.TTA.Common;");
            codeContent.AppendLine($"using RC.Core.ServiceImpl;");
            codeContent.AppendLine($"using {Project.DataTransmitModelNamespace}.{Name};");
            codeContent.AppendLine($"using {Project.DomainNamespace};");
            codeContent.AppendLine($"using {Project.IRepositoryNamespace};");
            codeContent.AppendLine($"using {Project.ServiceNamespace};");
            codeContent.AppendLine($"using {Project.ServiceNamespace}.Models.{Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.ServiceImplNamespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 服务实现");
            codeContent.AppendLine($"    /// </summary>");
            if (GeneratorDefaultService)
            {
                if (GeneratorQueryTargetService)
                {
                    codeContent.AppendLine($"    public partial class {ServiceImplName} : BaseServiceImpl<{AddModelName}, {EditModelName}, {QueryModelName}, {DTOName}, {ListDTOName}, {IRepositoryName}, {IViewRepositoryName}, {Name}, {ViewDomainName}>, {IServiceName}");
                }
                else
                {
                    codeContent.AppendLine($"    public partial class {ServiceImplName} : BaseServiceImpl<{AddModelName}, {EditModelName}, {QueryModelName}, {DTOName}, {ListDTOName}, {IRepositoryName}, {Name}>, {IServiceName}");
                }
            }
            else
            {
                codeContent.AppendLine($"    public partial class {ServiceImplName} : {IServiceName}");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{ServiceImplName}.g.cs", sourceText);
        }
        /// <summary>
        /// 创建服务接口文件
        /// </summary>
        private void CreateIServiceFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using RC.Core.Services;");
            codeContent.AppendLine($"using {Project.DataTransmitModelNamespace}.{Name};");
            codeContent.AppendLine($"using {Project.ServiceNamespace}.Models.{Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.ServiceNamespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 服务");
            codeContent.AppendLine($"    /// </summary>");
            if (GeneratorDefaultService)
            {
                codeContent.AppendLine($"    public partial interface {IServiceName} : IBaseService<{AddModelName}, {EditModelName}, {QueryModelName}, {DTOName}, {ListDTOName}>");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface {IServiceName}");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{IServiceName}.g.cs", sourceText);
        }
        /// <summary>
        /// 创建查询模型文件
        /// </summary>
        private void CreateQueryModelFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using Materal.Model;");
            codeContent.AppendLine($"using RC.Core.Services;");
            DomainModel? targetDomain;
            if (GeneratorQueryTargetService)
            {
                targetDomain = Project.Domains.FirstOrDefault(m => m.Name == ViewDomainName) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            if (targetDomain.OtherUsing != null && targetDomain.OtherUsing.Count > 0)
            {
                foreach (string usingName in targetDomain.OtherUsing)
                {
                    codeContent.AppendLine($"using {usingName};");
                }
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.ServiceNamespace}.Models.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}查询模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {QueryModelName} : PageRequestModel, IQueryServiceModel");
            codeContent.AppendLine($"    {{");
            FillQueryModelProperties(targetDomain, codeContent);
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{QueryModelName}.g.cs", sourceText);
        }
        /// <summary>
        /// 填充查询请求模型属性
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="codeContent"></param>
        private static void FillQueryModelProperties(DomainModel domain, StringBuilder codeContent)
        {
            foreach (DomainPropertyModel property in domain.Properties)
            {
                if (property.UseQuery)
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// {property.Annotation}");
                    codeContent.AppendLine($"        /// </summary>");
                    codeContent.AppendLine($"        {property.QueryAttribute}");
                    codeContent.AppendLine($"        public {property.NullPredefinedType} {property.Name} {{ get; set; }}");
                }
                if (property.IsBetween)
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// 最大{property.Annotation}");
                    codeContent.AppendLine($"        /// </summary>");
                    codeContent.AppendLine($"        [LessThanOrEqual(\"{property.Name}\")]");
                    codeContent.AppendLine($"        public {property.NullPredefinedType} Max{property.Name} {{ get; set; }}");
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// 最小{property.Annotation}");
                    codeContent.AppendLine($"        /// </summary>");
                    codeContent.AppendLine($"        [GreaterThanOrEqual(\"{property.Name}\")]");
                    codeContent.AppendLine($"        public {property.NullPredefinedType} Min{property.Name} {{ get; set; }}");
                }
            }
            if (domain.ExtendQueryGenerator)
            {
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
            }
        }
        /// <summary>
        /// 创建修改模型文件
        /// </summary>
        private void CreateEditModelFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using RC.Core.Models;");
            codeContent.AppendLine($"using RC.Core.Services;");
            if (OtherUsing != null && OtherUsing.Count > 0)
            {
                foreach (string usingName in OtherUsing)
                {
                    codeContent.AppendLine($"using {usingName};");
                }
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.ServiceNamespace}.Models.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}修改模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {EditModelName} : IEditServiceModel");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Required(ErrorMessage = \"唯一标识为空\")]");
            codeContent.AppendLine($"        public Guid ID {{ get; set; }}");
            foreach (DomainPropertyModel property in Properties)
            {
                if (!property.CanEdit) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                if (property.UseValidation)
                {
                    codeContent.AppendLine($"        {property.ValidationAttribute}");
                }
                codeContent.AppendLine($"        {property.Modifier} {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{EditModelName}.g.cs", sourceText);
        }
        /// <summary>
        /// 创建添加模型文件
        /// </summary>
        private void CreateAddModelFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using RC.Core.Services;");
            codeContent.AppendLine($"using RC.Core.Models;");
            if (OtherUsing != null && OtherUsing.Count > 0)
            {
                foreach (string usingName in OtherUsing)
                {
                    codeContent.AppendLine($"using {usingName};");
                }
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.ServiceNamespace}.Models.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}添加模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {AddModelName} : IServiceModel");
            codeContent.AppendLine($"    {{");
            foreach (DomainPropertyModel property in Properties)
            {
                if (!property.CanAdd) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                if (property.UseValidation)
                {
                    codeContent.AppendLine($"        {property.ValidationAttribute}");
                }
                codeContent.AppendLine($"        {property.Modifier} {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{AddModelName}.g.cs", sourceText);
        }
        /// <summary>
        /// 创建DTO文件
        /// </summary>
        private void CreateDTOFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using RC.Core.DataTransmitModel;");
            codeContent.AppendLine($"using RC.Core.Models;");
            codeContent.AppendLine($"using System;");
            DomainModel? targetDomain;
            if (GeneratorQueryTargetService)
            {
                targetDomain = Project.Domains.FirstOrDefault(m => m.Name == ViewDomainName) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            if (targetDomain.OtherUsing != null && targetDomain.OtherUsing.Count > 0)
            {
                foreach (string usingName in targetDomain.OtherUsing)
                {
                    codeContent.AppendLine($"using {usingName};");
                }
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.DataTransmitModelNamespace}.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {DTOName} : {ListDTOName}, IDTO");
            codeContent.AppendLine($"    {{");
            FillDTOProperty(targetDomain, codeContent);
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{DTOName}.g.cs", sourceText);
        }
        /// <summary>
        /// 填充列表数据传输模型属性
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="codeContent"></param>
        private static void FillDTOProperty(DomainModel domain, StringBuilder codeContent)
        {
            foreach (DomainPropertyModel property in domain.Properties)
            {
                if (property.GeneratorListDTO || !property.GeneratorDTO) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                if (property.UseValidation)
                {
                    codeContent.AppendLine($"        {property.ValidationAttribute}");
                }
                codeContent.AppendLine($"        {property.Modifier} {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
        }
        /// <summary>
        /// 创建列表DTO文件
        /// </summary>
        private void CreateListDTOFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using RC.Core.DataTransmitModel;");
            codeContent.AppendLine($"using RC.Core.Models;");
            codeContent.AppendLine($"using System;");
            DomainModel? targetDomain;
            if (GeneratorQueryTargetService)
            {
                targetDomain = Project.Domains.FirstOrDefault(m => m.Name == ViewDomainName) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            if (targetDomain.OtherUsing != null && targetDomain.OtherUsing.Count > 0)
            {
                foreach (string usingName in targetDomain.OtherUsing)
                {
                    codeContent.AppendLine($"using {usingName};");
                }
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.DataTransmitModelNamespace}.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}列表数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {ListDTOName}: IListDTO");
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
            FillListDTOProperty(targetDomain, codeContent);
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{ListDTOName}.g.cs", sourceText);
        }
        /// <summary>
        /// 填充列表数据传输模型属性
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="codeContent"></param>
        private static void FillListDTOProperty(DomainModel domain, StringBuilder codeContent)
        {
            foreach (DomainPropertyModel property in domain.Properties)
            {
                if (!property.GeneratorListDTO) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                if (property.UseValidation)
                {
                    codeContent.AppendLine($"        {property.ValidationAttribute}");
                }
                codeContent.AppendLine($"        {property.Modifier} {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
        }
        /// <summary>
        /// 创建实体配置文件
        /// </summary>
        private void CreateEntityConfigFile()
        {
            #region EntityConfig
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore.Metadata.Builders;");
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore;");
            codeContent.AppendLine($"using RC.Core.SqlServer;");
            codeContent.AppendLine($"using {Project.Namespace}.Domain;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.RepositoryImplNamespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}实体配置基类");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public abstract class {EntityConfigName}Base : BaseEntityConfig<{Name}>");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 配置实体");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public override void Configure(EntityTypeBuilder<{Name}> builder)");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            builder = BaseConfigure(builder);");
            foreach (DomainPropertyModel property in Properties)
            {
                if (property.HasAttribute<NotEntityConfigGeneratorAttribute>()) continue;
                codeContent.AppendLine($"            builder.Property(e => e.{property.Name})");
                #region IsRequired
                if (property.CanNull)
                {
                    codeContent.Append($"                .IsRequired(false)");
                }
                else
                {
                    codeContent.Append($"                .IsRequired()");
                }
                #endregion
                #region HasMaxLength
                AttributeModel? stringLengthAttribute = property.GetAttribute("StringLengthAttribute");
                if (stringLengthAttribute != null)
                {
                    AttributeArgumentModel? maxLengthArgument = stringLengthAttribute.GetAttributeArgument();
                    if (maxLengthArgument != null)
                    {
                        codeContent.Append($"\r\n                .HasMaxLength({maxLengthArgument.Value})");
                    }
                }
                #endregion
                #region HasColumnType
                AttributeModel? columnTypeAttribute = property.GetAttribute<ColumnTypeAttribute>();
                if (columnTypeAttribute != null)
                {
                    AttributeArgumentModel? columnTypeArgument = columnTypeAttribute.GetAttributeArgument();
                    if (columnTypeArgument != null)
                    {
                        codeContent.Append($"\r\n                .HasColumnType({columnTypeArgument.Value})");
                    }
                }
                #endregion
                codeContent.AppendLine(";");
            }
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}实体配置类");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {EntityConfigName} : {EntityConfigName}Base");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{EntityConfigName}.g.cs", sourceText);
            #endregion
        }
        /// <summary>
        /// 创建仓储实现文件
        /// </summary>
        private void CreateRepositoryImplFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using {Namespace};");
            codeContent.AppendLine($"using {Project.IRepositoryNamespace};");
            codeContent.AppendLine($"using RC.Core.SqlServer;");
            if (UseCache)
            {
                codeContent.AppendLine($"using Materal.CacheHelper;");
            }
            codeContent.AppendLine($"using System;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.RepositoryImplNamespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}仓储实现");
            codeContent.AppendLine($"    /// </summary>");
            if (!UseCache)
            {
                codeContent.AppendLine($"    public partial class {RepositoryImplName}: RCEFRepositoryImpl<{Name}, Guid>, I{Name}Repository");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public {RepositoryImplName}({Project.DBContextName} dbContext) : base(dbContext) {{ }}");
                codeContent.AppendLine($"    }}");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {RepositoryImplName}: RCCacheRepositoryImpl<{Name}, Guid>, I{Name}Repository");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public {RepositoryImplName}({Project.DBContextName} dbContext, ICacheManager cacheManager) : base(dbContext, cacheManager) {{ }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 获得所有缓存名称");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        protected override string GetAllCacheName() => \"All{Name}\";");
                codeContent.AppendLine($"    }}");
            }
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{RepositoryImplName}.g.cs", sourceText);
        }
        /// <summary>
        /// 创建仓储接口文件
        /// </summary>
        private void CreateIRepositoryFile()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using {Namespace};");
            codeContent.AppendLine($"using Materal.TTA.EFRepository;");
            codeContent.AppendLine($"using System;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {Project.IRepositoryNamespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}仓储接口");
            codeContent.AppendLine($"    /// </summary>");
            if (!UseCache)
            {
                codeContent.AppendLine($"    public partial interface {IRepositoryName} : IEFRepository<{Name}, Guid> {{ }}");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface {IRepositoryName} : ICacheEFRepository<{Name}, Guid> {{ }}");
            }
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{IRepositoryName}.g.cs", sourceText);
        }
    }
}
