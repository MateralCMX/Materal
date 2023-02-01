//using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.CodeGenerator.Extensions;
using System.Text;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class DomainModel
    {
        /// <summary>
        /// 引用组
        /// </summary>
        public List<string> Usings { get; } = new();
        /// <summary>
        /// 其他引用组
        /// </summary>
        public List<string> OtherUsings { get; } = new();
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; } = new();
        /// <summary>
        /// 属性
        /// </summary>
        public List<DomainPropertyModel> Properties { get; } = new();
        private readonly bool _useCache;
        private readonly bool _generatorCode;
        private readonly bool _generatorQueryTargetService;
        private readonly bool _generatorService;
        private readonly bool _generatorQueryModel;
        private readonly bool _generatorWebAPI;
        private readonly bool _generatorServiceWebAPI;
        private readonly bool _generatorDefaultService;
        private readonly bool _extendQueryGenerator;
        #region 文件名称
        private readonly string _entityConfigName;
        private readonly string _iRepositoryName;
        private readonly string _repositoryImplName;
        private readonly string _listDTOName;
        private readonly string _dtoName;
        private readonly string _addModelName;
        private readonly string _editModelName;
        private readonly string _queryModelName;
        private readonly string _iServiceName;
        private readonly string _serviceImplName;
        private readonly string _autoMapperProfileName;
        private readonly string _addRequestModelName;
        private readonly string _editRequestModelName;
        private readonly string _queryRequestModelName;
        private readonly string _controllerName;
        private readonly string? _queryTargetName;
        private readonly string? _iQueryTargetRepositoryName;
        #endregion
        public DomainModel(string[] codes, int classLineIndex)
        {
            #region 解析Class
            {
                int startIndex = classLineIndex;
                #region 解析名称
                if (startIndex < 0 || startIndex >= codes.Length) throw new CodeGeneratorException("类行位序错误");
                const string classTag = " class ";
                Name = codes[startIndex];
                int classIndex = Name.IndexOf(classTag);
                if (classIndex <= 0) throw new CodeGeneratorException("模型不是类");
                Name = Name.Substring(classIndex + classTag.Length);
                int domainIndex = Name.IndexOf(" : BaseDomain, IDomain");
                if (domainIndex <= 0) throw new CodeGeneratorException("模型不是Domain");
                Name = Name.Substring(0, domainIndex);
                #endregion
                startIndex -= 1;
                #region 解析特性
                do
                {
                    if (startIndex < 0) break;
                    string attributeCode = codes[startIndex].Trim();
                    if (!attributeCode.StartsWith("[") || !attributeCode.EndsWith("]")) break;
                    startIndex -= 1;
                    List<string> attributeCodes = attributeCode.GetAttributeCodes();
                    Attributes.AddRange(attributeCodes.Select(attributeName => new AttributeModel(attributeName.Trim())));
                } while (true);
                _useCache = Attributes.HasAttribute<CacheAttribute>();
                AttributeModel queryTragetGeneratorAttribute = Attributes.GetAttribute<QueryTragetGeneratorAttribute>();
                if (queryTragetGeneratorAttribute != null)
                {
                    _generatorQueryTargetService = true;
                    AttributeArgumentModel target = queryTragetGeneratorAttribute.AttributeArguments.First(m => string.IsNullOrWhiteSpace(m.Target));
                    _queryTargetName = target.Value;
                    _iQueryTargetRepositoryName = $"I{_queryTargetName}Repository";
                }
                else
                {
                    _generatorQueryTargetService = false;
                    _queryTargetName = null;
                    _iQueryTargetRepositoryName = null;
                }
                _generatorCode = !Attributes.HasAttribute<NotGeneratorAttribute>();
                _generatorQueryTargetService = Attributes.HasAttribute<QueryTragetGeneratorAttribute>();
                _generatorService = !Attributes.HasAttribute<NotServiceGeneratorAttribute>();
                _generatorDefaultService = !Attributes.HasAttribute<NotDefaultServiceGeneratorAttribute>();
                _generatorQueryModel = !Attributes.HasAttribute<NotServiceAndQueryGeneratorAttribute>();
                if (!_generatorQueryModel)
                {
                    _generatorService = false;
                    _generatorDefaultService = false;
                }
                _generatorWebAPI = !Attributes.HasAttribute<NotWebAPIGeneratorAttribute>();
                _generatorServiceWebAPI = !Attributes.HasAttribute<NotWebAPIServiceGeneratorAttribute>();
                _extendQueryGenerator = !Attributes.HasAttribute<NotExtendQueryGeneratorAttribute>();
                #endregion
                #region 解析注释
                Annotation = codes.GetAnnotation(ref startIndex);
                #endregion
                #region 解析命名空间
                string nameSpaceCode = codes[startIndex].Trim();
                while (!nameSpaceCode.StartsWith("namespace ") && startIndex >= 0)
                {
                    nameSpaceCode = codes[--startIndex].Trim();
                }
                Namespace = nameSpaceCode.Substring("namespace ".Length);
                #endregion
                #region 解析引用
                for (int i = 0; i < startIndex; i++)
                {
                    string usingCode = codes[i].Trim();
                    if (usingCode.StartsWith("using "))
                    {
                        Usings.Add(usingCode);
                    }
                }
                #endregion
            }
            #endregion
            #region 解析属性
            {
                for (int i = classLineIndex; i < codes.Length; i++)
                {
                    string propertyCode = codes[i].Trim();
                    if (!propertyCode.StartsWith("public ")) continue;
                    int getSetIndex = propertyCode.IndexOf("{ get; set; }");
                    if (getSetIndex <= 0) continue;
                    Properties.Add(new DomainPropertyModel(codes, i));
                }
            }
            #endregion
            #region 文件名称
            _entityConfigName = $"{Name}Config";
            _iRepositoryName = $"I{Name}Repository";
            _repositoryImplName = $"{Name}RepositoryImpl";
            _listDTOName = $"{Name}ListDTO";
            _dtoName = $"{Name}DTO";
            _addModelName = $"Add{Name}Model";
            _editModelName = $"Edit{Name}Model";
            _queryModelName = $"Query{Name}Model";
            _iServiceName = $"I{Name}Service";
            _serviceImplName = $"{Name}ServiceImpl";
            _autoMapperProfileName = $"{Name}Profile";
            _addRequestModelName = $"Add{Name}RequestModel";
            _editRequestModelName = $"Edit{Name}RequestModel";
            _queryRequestModelName = $"Query{Name}RequestModel";
            _controllerName = $"{Name}Controller";
            #endregion
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            string[] blackList = new[]
            {
                "using Materal.Model;",
                "using System.ComponentModel.DataAnnotations;",
                $"using Materal.BaseCore.Domain;",
                $"using Materal.BaseCore.CodeGenerator;",
                $"using Materal.BaseCore.Common.Utils.IndexHelper;",
                $"using Materal.BaseCore.Common.Utils.TreeHelper;",
            };
            OtherUsings.Clear();
            foreach (string usingCode in Usings)
            {
                if (blackList.Contains(usingCode.Trim())) continue;
                OtherUsings.Add(usingCode.Trim());
            }
        }
        #region WebAPI
        /// <summary>
        /// 创建WebAPI控制器文件
        /// </summary>
        public void CreateWebAPIControllerFile(ProjectModel project)
        {
            if (!_generatorCode || !_generatorService || !_generatorWebAPI) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.BaseCore.WebAPI.Controllers;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.DataTransmitModel.{Name};");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.PresentationModel.{Name};");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Services;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Services.Models.{Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.WebAPI.Controllers");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}控制器");
            codeContent.AppendLine($"    /// </summary>");
            if (_generatorServiceWebAPI && _generatorDefaultService)
            {
                codeContent.AppendLine($"    public partial class {_controllerName} : MateralCoreWebAPIServiceControllerBase<{_addRequestModelName}, {_editRequestModelName}, {_queryRequestModelName}, {_addModelName}, {_editModelName}, {_queryModelName}, {_dtoName}, {_listDTOName}, {_iServiceName}>");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {_controllerName} : MateralCoreWebAPIControllerBase");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "Controllers");
            codeContent.SaveFile(filePath, $"{_controllerName}.g.cs");
        }
        /// <summary>
        /// 创建AutoMapper配置文件
        /// </summary>
        public void CreateAutoMapperProfileFile(ProjectModel project)
        {
            if (!_generatorCode) return;
            if (!_generatorService && !_generatorQueryModel) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using AutoMapper;");
            if (_generatorService && _generatorWebAPI || _generatorQueryModel)
            {
                codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.PresentationModel.{Name};");
            }
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Services.Models.{Name};");
            if (_generatorQueryModel)
            {
                codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.DataTransmitModel.{Name};");
            }
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Domain;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.WebAPI.AutoMapperProfile");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}AutoMapper映射配置基类");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_autoMapperProfileName}Base : Profile");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 初始化");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        protected virtual void Init()");
            codeContent.AppendLine($"        {{");
            if (_generatorService)
            {
                codeContent.AppendLine($"            CreateMap<{_addModelName}, {Name}>();");
                codeContent.AppendLine($"            CreateMap<{_editModelName}, {Name}>();");
                if (_generatorWebAPI)
                {
                    codeContent.AppendLine($"            CreateMap<{_addRequestModelName}, {_addModelName}>();");
                    codeContent.AppendLine($"            CreateMap<{_editRequestModelName}, {_editModelName}>();");
                }
            }
            if (_generatorQueryModel)
            {
                if (_generatorQueryTargetService)
                {
                    codeContent.AppendLine($"            CreateMap<{_queryTargetName}, {_listDTOName}>();");
                    codeContent.AppendLine($"            CreateMap<{_queryTargetName}, {_dtoName}>();");
                }
                codeContent.AppendLine($"            CreateMap<{Name}, {_listDTOName}>();");
                codeContent.AppendLine($"            CreateMap<{Name}, {_dtoName}>();");
                codeContent.AppendLine($"            CreateMap<{_queryRequestModelName}, {_queryModelName}>();");
            }
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}AutoMapper映射配置");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_autoMapperProfileName} : {_autoMapperProfileName}Base");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 构造方法");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public {_autoMapperProfileName}()");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            Init();");
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "AutoMapperProfile");
            codeContent.SaveFile(filePath, $"{_autoMapperProfileName}.g.cs");
        }
        #endregion
        #region Services
        /// <summary>
        /// 创建服务接口文件
        /// </summary>
        public void CreateIServiceFile(ProjectModel project)
        {
            if (!_generatorCode || !_generatorService) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.BaseCore.Services;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.DataTransmitModel.{Name};");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Services.Models.{Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.Services");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 服务");
            codeContent.AppendLine($"    /// </summary>");
            if (_generatorDefaultService)
            {
                codeContent.AppendLine($"    public partial interface {_iServiceName} : IBaseService<{_addModelName}, {_editModelName}, {_queryModelName}, {_dtoName}, {_listDTOName}>");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface {_iServiceName}");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(project.GeneratorRootPath, $"{_iServiceName}.g.cs");
        }
        /// <summary>
        /// 创建查询模型文件
        /// </summary>
        public void CreateQueryModelFile(ProjectModel project, List<DomainModel> domains)
        {
            if (!_generatorCode || !_generatorQueryModel) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine("#nullable enable");
            codeContent.AppendLine($"using Materal.Model;");
            codeContent.AppendLine($"using Materal.BaseCore.Services;");
            DomainModel targetDomain;
            if (_generatorQueryTargetService)
            {
                targetDomain = domains.FirstOrDefault((m) => m.Name == _queryTargetName) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            AppendOtherUsings(codeContent, targetDomain);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.Services.Models.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}查询模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_queryModelName} : PageRequestModel, IQueryServiceModel");
            codeContent.AppendLine($"    {{");
            FillQueryModelProperties(targetDomain, codeContent);
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "Models", Name);
            codeContent.SaveFile(filePath, $"{_queryModelName}.g.cs");
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
                if (property.HasQueryAttribute)
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// {property.Annotation}");
                    codeContent.AppendLine($"        /// </summary>");
                    AppendQueryAttributeCode(codeContent, property);
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
            if (domain._extendQueryGenerator)
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
        public void CreateEditModelFile(ProjectModel project)
        {
            if (!_generatorCode || !_generatorService) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine("#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using Materal.BaseCore.Services;");
            AppendOtherUsings(codeContent, this);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.Services.Models.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}修改模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_editModelName} : IEditServiceModel");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Required(ErrorMessage = \"唯一标识为空\")]");
            codeContent.AppendLine($"        public Guid ID {{ get; set; }}");
            foreach (DomainPropertyModel property in Properties)
            {
                if (!property.GeneratorEditModel) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                AppendValidationAttributeCode(codeContent, property);
                codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "Models", Name);
            codeContent.SaveFile(filePath, $"{_editModelName}.g.cs");
        }
        /// <summary>
        /// 创建添加模型文件
        /// </summary>
        public void CreateAddModelFile(ProjectModel project)
        {
            if (!_generatorCode || !_generatorService) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine("#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using Materal.BaseCore.Services;");
            AppendOtherUsings(codeContent, this);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.Services.Models.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}添加模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_addModelName} : IAddServiceModel");
            codeContent.AppendLine($"    {{");
            foreach (DomainPropertyModel property in Properties)
            {
                if (!property.GeneratorAddModel) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                AppendValidationAttributeCode(codeContent, property);
                codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "Models", Name);
            codeContent.SaveFile(filePath, $"{_addModelName}.g.cs");
        }
        #endregion
        #region ServiceImpl
        /// <summary>
        /// 创建服务文件
        /// </summary>
        public void CreateServiceImplFile(ProjectModel project)
        {
            if (!_generatorCode || !_generatorService) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.BaseCore.ServiceImpl;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.DataTransmitModel.{Name};");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Domain;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Domain.Repositories;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Services;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Services.Models.{Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.ServiceImpl");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 服务实现");
            codeContent.AppendLine($"    /// </summary>");
            if (_generatorDefaultService)
            {
                if (_generatorQueryTargetService)
                {
                    codeContent.AppendLine($"    public partial class {_serviceImplName} : BaseServiceImpl<{_addModelName}, {_editModelName}, {_queryModelName}, {_dtoName}, {_listDTOName}, {_iRepositoryName}, {_iQueryTargetRepositoryName}, {Name}, {_queryTargetName}>, {_iServiceName}");
                }
                else
                {
                    codeContent.AppendLine($"    public partial class {_serviceImplName} : BaseServiceImpl<{_addModelName}, {_editModelName}, {_queryModelName}, {_dtoName}, {_listDTOName}, {_iRepositoryName}, {Name}>, {_iServiceName}");
                }
            }
            else
            {
                codeContent.AppendLine($"    public partial class {_serviceImplName} : {_iServiceName}");
            }
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(project.GeneratorRootPath, $"{_serviceImplName}.g.cs");
        }
        #endregion
        #region EFRepository
        /// <summary>
        /// 创建仓储实现文件
        /// </summary>
        public void CreateRepositoryImplFile(ProjectModel project)
        {
            if (!_generatorCode) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using {project.PrefixName}.Core.EFRepository;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Domain;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Domain.Repositories;");
            if (_useCache)
            {
                codeContent.AppendLine($"using Materal.CacheHelper;");
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.EFRepository.RepositoryImpl");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}仓储实现");
            codeContent.AppendLine($"    /// </summary>");
            if (_useCache)
            {
                codeContent.AppendLine($"    public partial class {_repositoryImplName}: {project.PrefixName}CacheRepositoryImpl<{Name}, Guid>, I{Name}Repository");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public {_repositoryImplName}({project.DBContextName} dbContext, ICacheManager cacheManager) : base(dbContext, cacheManager) {{ }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 获得所有缓存名称");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        protected override string GetAllCacheName() => \"All{Name}\";");
                codeContent.AppendLine($"    }}");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {_repositoryImplName}: {project.PrefixName}EFRepositoryImpl<{Name}, Guid>, I{Name}Repository");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public {_repositoryImplName}({project.DBContextName} dbContext) : base(dbContext) {{ }}");
                codeContent.AppendLine($"    }}");
            }
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "RepositoryImpl");
            codeContent.SaveFile(filePath, $"{_repositoryImplName}.g.cs");
        }
        /// <summary>
        /// 创建实体配置文件
        /// </summary>
        public void CreateEntityConfigFile(ProjectModel project)
        {
            if (!_generatorCode) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore;");
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore.Metadata.Builders;");
            codeContent.AppendLine($"using Materal.BaseCore.EFRepository;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Domain;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.EFRepository.EntityConfigs");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}实体配置基类");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public abstract class {_entityConfigName}Base : BaseEntityConfig<{Name}>");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 配置实体");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public override void Configure(EntityTypeBuilder<{Name}> builder)");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            builder = BaseConfigure(builder);");
            foreach (DomainPropertyModel property in Properties)
            {
                if (!property.GeneratorEntityConfig) continue;
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
                AttributeModel stringLengthAttribute = property.GetAttribute("StringLengthAttribute");
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
                AttributeModel columnTypeAttribute = property.GetAttribute<ColumnTypeAttribute>();
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
            codeContent.AppendLine($"    public partial class {_entityConfigName} : {_entityConfigName}Base");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "EntityConfigs");
            codeContent.SaveFile(filePath, $"{_entityConfigName}.g.cs");
        }
        #endregion
        #region Domain
        /// <summary>
        /// 创建仓储接口文件
        /// </summary>
        public void CreateIRepositoryFile(ProjectModel project)
        {
            if (!_generatorCode) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.TTA.EFRepository;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.Domain.Repositories");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}仓储接口");
            codeContent.AppendLine($"    /// </summary>");
            if (!_useCache)
            {
                codeContent.AppendLine($"    public partial interface {_iRepositoryName} : IEFRepository<{Name}, Guid> {{ }}");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface {_iRepositoryName} : ICacheEFRepository<{Name}, Guid> {{ }}");
            }
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "Repositories");
            codeContent.SaveFile(filePath, $"{_iRepositoryName}.g.cs");
        }
        #endregion
        #region DataTransmitModel
        /// <summary>
        /// 创建DTO文件
        /// </summary>
        public void CreateDTOFile(ProjectModel project, List<DomainModel> domains)
        {
            if (!_generatorCode || !_generatorQueryModel) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using Materal.BaseCore.DataTransmitModel;");
            DomainModel targetDomain;
            if (_generatorQueryTargetService)
            {
                targetDomain = domains.FirstOrDefault((m) => m.Name == _queryTargetName) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            AppendOtherUsings(codeContent, targetDomain);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.DataTransmitModel.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_dtoName} : {_listDTOName}, IDTO");
            codeContent.AppendLine($"    {{");
            FillDTOProperty(targetDomain, codeContent);
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, Name);
            codeContent.SaveFile(filePath, $"{_dtoName}.g.cs");
        }
        /// <summary>
        /// 填充数据传输模型属性
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="codeContent"></param>
        private static void FillDTOProperty(DomainModel domain, StringBuilder codeContent)
        {
            foreach (DomainPropertyModel property in domain.Properties)
            {
                if (property.GeneratorListDTO) continue;
                if (!property.GeneratorDTO) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                AppendValidationAttributeCode(codeContent, property);
                codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
        }
        /// <summary>
        /// 创建列表DTO文件
        /// </summary>
        public void CreateListDTOFile(ProjectModel project, List<DomainModel> domains)
        {
            if (!_generatorCode || !_generatorQueryModel) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using Materal.BaseCore.DataTransmitModel;");
            DomainModel targetDomain;
            if (_generatorQueryTargetService)
            {
                targetDomain = domains.FirstOrDefault((m) => m.Name == _queryTargetName) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            AppendOtherUsings(codeContent, targetDomain);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.DataTransmitModel.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}列表数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_listDTOName}: IListDTO");
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
            string filePath = Path.Combine(project.GeneratorRootPath, Name);
            codeContent.SaveFile(filePath, $"{_listDTOName}.g.cs");
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
                AppendValidationAttributeCode(codeContent, property);
                codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
        }
        #endregion
        #region PresentationModel
        /// <summary>
        /// 创建查询请求文件
        /// </summary>
        public void CreateQueryRequestModelFile(ProjectModel project, List<DomainModel> domains)
        {
            if (!_generatorCode || !_generatorQueryModel) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine("#nullable enable");
            codeContent.AppendLine($"using Materal.Model;");
            codeContent.AppendLine($"using Materal.BaseCore.PresentationModel;");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            DomainModel targetDomain;
            if (_generatorQueryTargetService)
            {
                targetDomain = domains.FirstOrDefault(m => m.Name == _queryTargetName) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            AppendOtherUsings(codeContent, targetDomain);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.PresentationModel.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}查询请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_queryRequestModelName} : PageRequestModel, IQueryRequestModel");
            codeContent.AppendLine($"    {{");
            FillQueryRequestModelProperties(targetDomain, codeContent);
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, Name);
            codeContent.SaveFile(filePath, $"{_queryRequestModelName}.g.cs");
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
                if (property.HasQueryAttribute)
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
            if (domain._extendQueryGenerator)
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
        public void CreateEditRequestModelFile(ProjectModel project)
        {
            if (!_generatorCode || !_generatorService || !_generatorWebAPI) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine("#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using Materal.BaseCore.PresentationModel;");
            AppendOtherUsings(codeContent, this);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.PresentationModel.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}修改请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_editRequestModelName} : IEditRequestModel");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        [Required(ErrorMessage = \"唯一标识为空\")]");
            codeContent.AppendLine($"        public Guid ID {{ get; set; }}");
            foreach (DomainPropertyModel property in Properties)
            {
                if (!property.GeneratorEditModel) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                AppendValidationAttributeCode(codeContent, property);
                codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, Name);
            codeContent.SaveFile(filePath, $"{_editRequestModelName}.g.cs");
        }
        /// <summary>
        /// 创建添加请求文件
        /// </summary>
        public void CreateAddRequestModelFile(ProjectModel project)
        {
            if (!_generatorCode || !_generatorService || !_generatorWebAPI) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine("#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using Materal.BaseCore.PresentationModel;");
            AppendOtherUsings(codeContent, this);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.PresentationModel.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}添加请求模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_addRequestModelName} : IAddRequestModel");
            codeContent.AppendLine($"    {{");
            foreach (DomainPropertyModel property in Properties)
            {
                if (!property.GeneratorAddModel) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                AppendValidationAttributeCode(codeContent, property);
                codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, Name);
            codeContent.SaveFile(filePath, $"{_addRequestModelName}.g.cs");
        }
        #endregion
        /// <summary>
        /// 添加其他引用
        /// </summary>
        /// <param name="codeContent"></param>
        /// <param name="targetDomain"></param>
        private static void AppendOtherUsings(StringBuilder codeContent, DomainModel targetDomain)
        {
            if (targetDomain.OtherUsings != null && targetDomain.OtherUsings.Count > 0)
            {
                foreach (string usingCode in targetDomain.OtherUsings)
                {
                    codeContent.AppendLine(usingCode);
                }
            }
        }
        /// <summary>
        /// 添加验证特性代码
        /// </summary>
        /// <param name="codeContent"></param>
        /// <param name="property"></param>
        private static void AppendValidationAttributeCode(StringBuilder codeContent, DomainPropertyModel property)
        {
            if (property.HasValidationAttribute)
            {
                codeContent.Append($"        [");
                List<string> attributesString = new();
                foreach (AttributeModel attribute in property.ValidationAttributes)
                {
                    attributesString.Add(attribute.ToString());
                }
                codeContent.Append(string.Join(", ", attributesString));
                codeContent.AppendLine($"]");
            }
        }
        /// <summary>
        /// 添加查询特性代码
        /// </summary>
        /// <param name="codeContent"></param>
        /// <param name="property"></param>
        private static void AppendQueryAttributeCode(StringBuilder codeContent, DomainPropertyModel property)
        {
            if (property.HasQueryAttribute)
            {
                codeContent.Append($"        [");
                List<string> attributesString = new();
                foreach (AttributeModel attribute in property.QueryAttributes)
                {
                    attributesString.Add(attribute.ToString());
                }
                codeContent.Append(string.Join(", ", attributesString));
                codeContent.AppendLine($"]");
            }
        }
    }
}
