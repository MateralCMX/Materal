using Materal.BaseCore.CodeGenerator.Extensions;
using System.Text;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class DomainModel
    {
        /// <summary>
        /// 引用组
        /// </summary>
        public List<string> Usings { get; set; } = new();
        /// <summary>
        /// 其他引用组
        /// </summary>
        public List<string> OtherUsings { get; set; } = new();
        /// <summary>
        /// 注释
        /// </summary>
        public string? Annotation { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 特性组
        /// </summary>
        public List<AttributeModel> Attributes { get; set; } = new();
        /// <summary>
        /// 属性
        /// </summary>
        public List<DomainPropertyModel> Properties { get; set; } = new();
        /// <summary>
        /// 使用缓存
        /// </summary>
        public bool UseCache { get; set; }
        /// <summary>
        /// 生成代码
        /// </summary>
        public bool GeneratorCode { get; set; }
        /// <summary>
        /// 生成目标查询服务
        /// </summary>
        public bool GeneratorQueryTargetService { get; set; }
        /// <summary>
        /// 生成服务
        /// </summary>
        public bool GeneratorService { get; set; }
        /// <summary>
        /// 生成查询模型
        /// </summary>
        public bool GeneratorQueryModel { get; set; }
        /// <summary>
        /// 生成WebAPI
        /// </summary>
        public bool GeneratorWebAPI { get; set; }
        /// <summary>
        /// 生成服务WebAPI
        /// </summary>
        public bool GeneratorServiceWebAPI { get; set; }
        /// <summary>
        /// 生成默认服务
        /// </summary>
        public bool GeneratorDefaultService { get; set; }
        /// <summary>
        /// 生成扩展查询
        /// </summary>
        public bool GeneratorExtendQuery { get; set; }
        /// <summary>
        /// 是表
        /// </summary>
        public bool IsTable { get; set; }
        /// <summary>
        /// 是IndexDomain
        /// </summary>
        public bool IsIndexDomain { get; set; }
        /// <summary>
        /// 是TreeDomain
        /// </summary>
        public bool IsTreeDomain { get; set; }
        #region 文件名称
        private readonly string _entityConfigName = string.Empty;
        private readonly string _iRepositoryName = string.Empty;
        private readonly string _repositoryImplName = string.Empty;
        private readonly string _listDTOName = string.Empty;
        private readonly string _treeListDTOName = string.Empty;
        private readonly string _dtoName = string.Empty;
        private readonly string _addModelName = string.Empty;
        private readonly string _editModelName = string.Empty;
        private readonly string _queryModelName = string.Empty;
        private readonly string _queryTreeListModelName = string.Empty;
        private readonly string _iServiceName = string.Empty;
        private readonly string _serviceImplName = string.Empty;
        private readonly string _autoMapperProfileName = string.Empty;
        private readonly string _addRequestModelName = string.Empty;
        private readonly string _editRequestModelName = string.Empty;
        private readonly string _queryRequestModelName = string.Empty;
        private readonly string _queryTreeListRequestModelName = string.Empty;
        private readonly string _controllerName = string.Empty;
        private readonly string? _queryTargetName;
        private readonly string? _iQueryTargetRepositoryName;
        #endregion
        public DomainModel() { }
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
                Name = Name[(classIndex + classTag.Length)..];
                int domainIndex = Name.IndexOf(" : BaseDomain, IDomain");
                if (domainIndex <= 0) throw new CodeGeneratorException("模型不是Domain");
                int indexDomainIndex = Name.IndexOf(", IIndexDomain");
                IsIndexDomain = indexDomainIndex > 0;
                int treeDomainIndex = Name.IndexOf(", ITreeDomain");
                IsTreeDomain = treeDomainIndex > 0;
                Name = Name[..domainIndex];
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
                UseCache = Attributes.HasAttribute<CacheAttribute>();
                AttributeModel queryTargetGeneratorAttribute = Attributes.GetAttribute<QueryTargetGeneratorAttribute>();
                if (queryTargetGeneratorAttribute != null)
                {
                    GeneratorQueryTargetService = true;
                    AttributeArgumentModel target = queryTargetGeneratorAttribute.AttributeArguments.First(m => string.IsNullOrWhiteSpace(m.Target));
                    _queryTargetName = target.Value[1..^1];
                    _iQueryTargetRepositoryName = $"I{_queryTargetName}Repository";
                }
                else
                {
                    GeneratorQueryTargetService = false;
                    _queryTargetName = null;
                    _iQueryTargetRepositoryName = null;
                }
                GeneratorCode = !Attributes.HasAttribute<NotGeneratorAttribute>();
                GeneratorService = !Attributes.HasAttribute<NotServiceGeneratorAttribute>();
                GeneratorDefaultService = !Attributes.HasAttribute<NotDefaultServiceGeneratorAttribute>();
                GeneratorQueryModel = !Attributes.HasAttribute<NotServiceAndQueryGeneratorAttribute>();
                if (!GeneratorQueryModel)
                {
                    GeneratorService = false;
                    GeneratorDefaultService = false;
                }
                GeneratorWebAPI = !Attributes.HasAttribute<NotWebAPIGeneratorAttribute>();
                GeneratorServiceWebAPI = !Attributes.HasAttribute<NotWebAPIServiceGeneratorAttribute>();
                GeneratorExtendQuery = !Attributes.HasAttribute<NotExtendQueryGeneratorAttribute>();
                IsTable = !Attributes.HasAttribute<NotTableAttribute>();
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
                Namespace = nameSpaceCode["namespace ".Length..];
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
            _treeListDTOName = $"{Name}TreeListDTO";
            _dtoName = $"{Name}DTO";
            _addModelName = $"Add{Name}Model";
            _editModelName = $"Edit{Name}Model";
            _queryModelName = $"Query{Name}Model";
            _queryTreeListModelName = $"Query{Name}TreeListModel";
            _iServiceName = $"I{Name}Service";
            _serviceImplName = $"{Name}ServiceImpl";
            _autoMapperProfileName = $"{Name}Profile";
            _addRequestModelName = $"Add{Name}RequestModel";
            _editRequestModelName = $"Edit{Name}RequestModel";
            _queryRequestModelName = $"Query{Name}RequestModel";
            _queryTreeListRequestModelName = $"Query{Name}TreeListRequestModel";
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
                "using Materal.Utils.Model;",
                "using System.ComponentModel.DataAnnotations;",
                "using Materal.BaseCore.Domain;",
                "using Materal.BaseCore.Common.Utils.IndexHelper;",
                "using Materal.BaseCore.Common.Utils.TreeHelper;",
            };
            OtherUsings.Clear();
            foreach (string usingCode in Usings)
            {
                if (usingCode.Contains("CodeGenerator")) continue;
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
            if (!GeneratorCode || !GeneratorService || !GeneratorWebAPI) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.BaseCore.PresentationModel;");
            codeContent.AppendLine($"using Materal.BaseCore.Services.Models;");
            codeContent.AppendLine($"using Materal.BaseCore.WebAPI.Controllers;");
            codeContent.AppendLine($"using Materal.Utils.Model;");
            codeContent.AppendLine($"using Microsoft.AspNetCore.Mvc;");
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
            if (GeneratorServiceWebAPI && GeneratorDefaultService)
            {
                codeContent.AppendLine($"    public partial class {_controllerName} : MateralCoreWebAPIServiceControllerBase<{_addRequestModelName}, {_editRequestModelName}, {_queryRequestModelName}, {_addModelName}, {_editModelName}, {_queryModelName}, {_dtoName}, {_listDTOName}, {_iServiceName}>");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"serviceProvider\"></param>");
                codeContent.AppendLine($"        public {_controllerName}(IServiceProvider serviceProvider) : base(serviceProvider) {{ }}");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {_controllerName} : MateralCoreWebAPIControllerBase");
                codeContent.AppendLine($"    {{");
            }
            if (IsIndexDomain)
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
            if (IsTreeDomain)
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
                codeContent.AppendLine($"        public async Task<ResultModel<List<{_treeListDTOName}>>> GetTreeListAsync({_queryTreeListRequestModelName} requestModel)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            {_queryTreeListModelName} model = Mapper.Map<{_queryTreeListModelName}>(requestModel);");
                codeContent.AppendLine($"            List<{_treeListDTOName}> result = await DefaultService.GetTreeListAsync(model);");
                codeContent.AppendLine($"            return ResultModel<List<{_treeListDTOName}>>.Success(result, \"查询成功\");");
                codeContent.AppendLine($"        }}");
            }
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
            if (!GeneratorCode) return;
            if (!GeneratorService && !GeneratorQueryModel) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using AutoMapper;");
            if (GeneratorService && GeneratorWebAPI || GeneratorQueryModel)
            {
                codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.PresentationModel.{Name};");
            }
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Services.Models.{Name};");
            if (GeneratorQueryModel)
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
            if (GeneratorService)
            {
                codeContent.AppendLine($"            CreateMap<{_addModelName}, {Name}>();");
                codeContent.AppendLine($"            CreateMap<{_editModelName}, {Name}>();");
                if (GeneratorWebAPI)
                {
                    codeContent.AppendLine($"            CreateMap<{_addRequestModelName}, {_addModelName}>();");
                    codeContent.AppendLine($"            CreateMap<{_editRequestModelName}, {_editModelName}>();");
                }
            }
            if (GeneratorQueryModel)
            {
                if (GeneratorQueryTargetService)
                {
                    if (IsTreeDomain)
                    {
                        codeContent.AppendLine($"            CreateMap<{_queryTargetName}, {_treeListDTOName}>();");
                    }
                    codeContent.AppendLine($"            CreateMap<{_queryTargetName}, {_listDTOName}>();");
                    codeContent.AppendLine($"            CreateMap<{_queryTargetName}, {_dtoName}>();");
                }
                if (IsTreeDomain)
                {
                    codeContent.AppendLine($"            CreateMap<{Name}, {_treeListDTOName}>();");
                }
                codeContent.AppendLine($"            CreateMap<{Name}, {_listDTOName}>();");
                codeContent.AppendLine($"            CreateMap<{Name}, {_dtoName}>();");
                codeContent.AppendLine($"            CreateMap<{_queryRequestModelName}, {_queryModelName}>();");
                if (IsTreeDomain)
                {
                    codeContent.AppendLine($"            CreateMap<{_queryTreeListRequestModelName}, {_queryTreeListModelName}>();");
                }
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
            if (!GeneratorCode || !GeneratorService) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.BaseCore.Services;");
            codeContent.AppendLine($"using Materal.BaseCore.Services.Models;");
            codeContent.AppendLine($"using Materal.Utils.Model;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.DataTransmitModel.{Name};");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Services.Models.{Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.Services");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}服务");
            codeContent.AppendLine($"    /// </summary>");
            if (GeneratorDefaultService)
            {
                codeContent.AppendLine($"    public partial interface {_iServiceName} : IBaseService<{_addModelName}, {_editModelName}, {_queryModelName}, {_dtoName}, {_listDTOName}>");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface {_iServiceName}");
            }
            codeContent.AppendLine($"    {{");
            if (IsIndexDomain)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [DataValidation]");
                codeContent.AppendLine($"        Task ExchangeIndexAsync(ExchangeIndexModel model);");
            }
            if (IsTreeDomain)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [DataValidation]");
                codeContent.AppendLine($"        Task ExchangeParentAsync(ExchangeParentModel model);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 查询树列表");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"queryModel\"></param>");
                codeContent.AppendLine($"        Task<List<{_treeListDTOName}>> GetTreeListAsync({_queryTreeListModelName} queryModel);");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(project.GeneratorRootPath, $"{_iServiceName}.g.cs");
        }
        /// <summary>
        /// 创建查询模型文件
        /// </summary>
        public void CreateQueryModelFile(ProjectModel project, List<DomainModel> domains)
        {
            if (!GeneratorCode || !GeneratorQueryModel) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine("#nullable enable");
            codeContent.AppendLine($"using Materal.Utils.Model;");
            codeContent.AppendLine($"using Materal.BaseCore.Services;");
            DomainModel targetDomain;
            if (GeneratorQueryTargetService)
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
        /// 创建查询树模型文件
        /// </summary>
        /// <param name="project"></param>
        /// <param name="domains"></param>
        public void CreateQueryTreeModelFile(ProjectModel project)
        {
            if (!GeneratorCode || !GeneratorQueryModel || !IsTreeDomain) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine("#nullable enable");
            codeContent.AppendLine($"using Materal.Utils.Model;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.Services.Models.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}查询模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_queryTreeListModelName} : FilterModel");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 父级唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public Guid? ParentID {{ get; set; }}");
            foreach (DomainPropertyModel property in Properties.Where(m => m.IsTreeGourpProperty))
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                AppendQueryAttributeCode(codeContent, property);
                codeContent.AppendLine($"        public {property.NullPredefinedType} {property.Name} {{ get; set; }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "Models", Name);
            codeContent.SaveFile(filePath, $"{_queryTreeListModelName}.g.cs");
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
                    if (property.IsContains && !property.IsStringContains)
                    {
                        codeContent.AppendLine($"        public List<{property.NotNullPredefinedType}>? {property.Name}Collections {{ get; set; }}");
                    }
                    else
                    {
                        codeContent.AppendLine($"        public {property.NullPredefinedType} {property.Name} {{ get; set; }}");
                    }
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
            if (domain.GeneratorExtendQuery)
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
            if (!GeneratorCode || !GeneratorService) return;
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
            if (!GeneratorCode || !GeneratorService) return;
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
            if (!GeneratorCode || !GeneratorService) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.BaseCore.Domain;");
            codeContent.AppendLine($"using Materal.BaseCore.ServiceImpl;");
            codeContent.AppendLine($"using Materal.BaseCore.Services.Models;");
            codeContent.AppendLine($"using Materal.TTA.Common;");
            codeContent.AppendLine($"using Materal.TTA.EFRepository;");
            codeContent.AppendLine($"using System.Linq.Expressions;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.DataTransmitModel.{Name};");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Domain;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Domain.Repositories;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Services;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Services.Models.{Name};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.ServiceImpl");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}服务实现");
            codeContent.AppendLine($"    /// </summary>");
            if (GeneratorDefaultService)
            {
                if (GeneratorQueryTargetService)
                {
                    codeContent.AppendLine($"    public partial class {_serviceImplName} : BaseServiceImpl<{_addModelName}, {_editModelName}, {_queryModelName}, {_dtoName}, {_listDTOName}, {_iRepositoryName}, {_iQueryTargetRepositoryName}, {Name}, {_queryTargetName}>, {_iServiceName}");
                }
                else
                {
                    codeContent.AppendLine($"    public partial class {_serviceImplName} : BaseServiceImpl<{_addModelName}, {_editModelName}, {_queryModelName}, {_dtoName}, {_listDTOName}, {_iRepositoryName}, {Name}>, {_iServiceName}");
                }
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"serviceProvider\"></param>");
                codeContent.AppendLine($"        public {_serviceImplName}(IServiceProvider serviceProvider) : base(serviceProvider) {{ }}");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {_serviceImplName} : {_iServiceName}");
                codeContent.AppendLine($"    {{");
            }
            if (IsIndexDomain)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                if (IsTreeDomain)
                {
                    codeContent.AppendLine($"        public async Task ExchangeIndexAsync(ExchangeIndexModel model)");
                    codeContent.AppendLine($"        {{");
                    codeContent.AppendLine($"            OnExchangeIndexBefore(model);");
                    codeContent.Append($"            await ServiceImplHelper.ExchangeIndexAndExchangeParentByGroupPropertiesAsync<{_iRepositoryName}, {Name}>(model, DefaultRepository, UnitOfWork, new string[] {{ ");
                    List<DomainPropertyModel> indexGourpProperties = Properties.Where(m => m.IsIndexGourpProperty).ToList();
                    List<string> indexGroupCode = new();
                    foreach (DomainPropertyModel indexGourpProperty in indexGourpProperties)
                    {
                        indexGroupCode.Add($"nameof({Name}.{indexGourpProperty.Name})");
                    }
                    codeContent.Append(string.Join(", ", indexGroupCode));
                    codeContent.Append($" }}, new string[] {{ ");
                    List<DomainPropertyModel> treeGourpProperties = Properties.Where(m => m.IsTreeGourpProperty).ToList();
                    List<string> treeGroupCode = new();
                    foreach (DomainPropertyModel treeGourpProperty in treeGourpProperties)
                    {
                        treeGroupCode.Add($"nameof({Name}.{treeGourpProperty.Name})");
                    }
                    codeContent.Append(string.Join(", ", treeGroupCode));
                    codeContent.Append($" }}");
                    codeContent.AppendLine($");");
                    codeContent.AppendLine($"            OnExchangeIndexAfter(model);");
                    codeContent.AppendLine($"        }}");
                }
                else
                {
                    codeContent.AppendLine($"        public async Task ExchangeIndexAsync(ExchangeIndexModel model)");
                    codeContent.AppendLine($"        {{");
                    codeContent.AppendLine($"            OnExchangeIndexBefore(model);");
                    codeContent.Append($"            await ServiceImplHelper.ExchangeIndexByGroupPropertiesAsync<{_iRepositoryName}, {Name}>(model, DefaultRepository, UnitOfWork");
                    List<DomainPropertyModel> treeGourpProperties = Properties.Where(m => m.IsIndexGourpProperty).ToList();
                    foreach (DomainPropertyModel treeGourpProperty in treeGourpProperties)
                    {
                        codeContent.Append($", nameof({Name}.{treeGourpProperty.Name})");
                    }
                    codeContent.AppendLine($");");
                    codeContent.AppendLine($"            OnExchangeIndexAfter(model);");
                    codeContent.AppendLine($"        }}");
                }
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序之前");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        partial void OnExchangeIndexBefore(ExchangeIndexModel model);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 交换位序之后");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        partial void OnExchangeIndexAfter(ExchangeIndexModel model);");
            }
            if (IsTreeDomain)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        public async Task ExchangeParentAsync(ExchangeParentModel model)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            OnExchangeParentBefore(model);");
                codeContent.Append($"            await ServiceImplHelper.ExchangeParentByGroupPropertiesAsync<{_iRepositoryName}, {Name}>(model, DefaultRepository, UnitOfWork");
                List<DomainPropertyModel> indexGourpProperties = Properties.Where(m => m.IsTreeGourpProperty).ToList();
                foreach (DomainPropertyModel indexGourpProperty in indexGourpProperties)
                {
                    codeContent.Append($", nameof({Name}.{indexGourpProperty.Name})");
                }
                codeContent.AppendLine($");");
                codeContent.AppendLine($"            OnExchangeParentAfter(model);");
                codeContent.AppendLine($"        }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级之前");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        partial void OnExchangeParentBefore(ExchangeParentModel model);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 更改父级之后");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"model\"></param>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        partial void OnExchangeParentAfter(ExchangeParentModel model);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 查询树列表");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"queryModel\"></param>");
                codeContent.AppendLine($"        public async Task<List<{_treeListDTOName}>> GetTreeListAsync({_queryTreeListModelName} queryModel)");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            #region 排序表达式");
                codeContent.AppendLine($"            Type domainType = typeof({Name});");
                codeContent.AppendLine($"            Expression<Func<{Name}, object>> sortExpression = m => m.CreateTime;");
                codeContent.AppendLine($"            SortOrderEnum sortOrder = SortOrderEnum.Descending;");
                codeContent.AppendLine($"            if (queryModel.SortPropertyName is not null && !string.IsNullOrWhiteSpace(queryModel.SortPropertyName) && domainType.GetProperty(queryModel.SortPropertyName) is not null)");
                codeContent.AppendLine($"            {{");
                codeContent.AppendLine($"                sortExpression = queryModel.GetSortExpression<{Name}>();");
                codeContent.AppendLine($"                sortOrder = queryModel.IsAsc ? SortOrderEnum.Ascending : SortOrderEnum.Descending;");
                codeContent.AppendLine($"            }}");
                codeContent.AppendLine($"            else if (domainType.IsAssignableTo<IIndexDomain>())");
                codeContent.AppendLine($"            {{");
                codeContent.AppendLine($"                ParameterExpression parameterExpression = Expression.Parameter(domainType, \"m\");");
                codeContent.AppendLine($"                MemberExpression memberExpression = Expression.Property(parameterExpression, nameof(IIndexDomain.Index));");
                codeContent.AppendLine($"                UnaryExpression unaryExpression = Expression.Convert(memberExpression, typeof(object));");
                codeContent.AppendLine($"                sortExpression = Expression.Lambda<Func<{Name}, object>>(unaryExpression, parameterExpression);");
                codeContent.AppendLine($"                sortOrder = SortOrderEnum.Ascending;");
                codeContent.AppendLine($"            }}");
                codeContent.AppendLine($"            #endregion");
                codeContent.AppendLine($"            #region 查询数据源");
                codeContent.AppendLine($"            List<{Name}> allInfo;");
                codeContent.AppendLine($"            if (DefaultRepository is ICacheEFRepository<{Name}, Guid> cacheRepository)");
                codeContent.AppendLine($"            {{");
                codeContent.AppendLine($"                allInfo = await cacheRepository.GetAllInfoFromCacheAsync();");
                codeContent.AppendLine($"                Func<{Name}, bool> searchDlegate = queryModel.GetSearchDelegate<{Name}>();");
                codeContent.AppendLine($"                Func<{Name}, object> sortDlegate = sortExpression.Compile();");
                codeContent.AppendLine($"                if (sortOrder == SortOrderEnum.Ascending)");
                codeContent.AppendLine($"                {{");
                codeContent.AppendLine($"                    allInfo = allInfo.Where(searchDlegate).OrderBy(sortDlegate).ToList();");
                codeContent.AppendLine($"                }}");
                codeContent.AppendLine($"                else");
                codeContent.AppendLine($"                {{");
                codeContent.AppendLine($"                    allInfo = allInfo.Where(searchDlegate).OrderByDescending(sortDlegate).ToList();");
                codeContent.AppendLine($"                }}");
                codeContent.AppendLine($"            }}");
                codeContent.AppendLine($"            else");
                codeContent.AppendLine($"            {{");
                codeContent.AppendLine($"                allInfo = await DefaultRepository.FindAsync(queryModel, sortExpression, sortOrder);");
                codeContent.AppendLine($"            }}");
                codeContent.AppendLine($"            #endregion");
                codeContent.AppendLine($"            OnToTreeBefore(allInfo, queryModel);");
                codeContent.AppendLine($"            List<{_treeListDTOName}> result = allInfo.ToTree<{Name}, {_treeListDTOName}>(queryModel.ParentID, (dto, domain) =>");
                codeContent.AppendLine($"            {{");
                codeContent.AppendLine($"                Mapper.Map(domain, dto);");
                codeContent.AppendLine($"                OnConvertToTreeDTO(dto, domain, queryModel);");
                codeContent.AppendLine($"            }});");
                codeContent.AppendLine($"            OnToTreeAfter(result, queryModel);");
                codeContent.AppendLine($"            return result;");
                codeContent.AppendLine($"        }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 转换树之前");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"allInfo\"></param>");
                codeContent.AppendLine($"        /// <param name=\"queryModel\"></param>");
                codeContent.AppendLine($"        partial void OnToTreeBefore(List<{Name}> allInfo, {_queryTreeListModelName} queryModel);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 转换树之后");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"dtos\"></param>");
                codeContent.AppendLine($"        /// <param name=\"queryModel\"></param>");
                codeContent.AppendLine($"        partial void OnToTreeAfter(List<{_treeListDTOName}> dtos, {_queryTreeListModelName} queryModel);");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 转换为树DTO");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <param name=\"dtos\"></param>");
                codeContent.AppendLine($"        /// <param name=\"domain\"></param>");
                codeContent.AppendLine($"        /// <param name=\"queryModel\"></param>");
                codeContent.AppendLine($"        partial void OnConvertToTreeDTO({_treeListDTOName} dto, {Name} domain, {_queryTreeListModelName} queryModel);");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(project.GeneratorRootPath, $"{_serviceImplName}.g.cs");
        }
        #endregion
        #region EFRepository
        /// <summary>
        /// 创建仓储实现文件
        /// </summary>
        public void CreateRepositoryImplFile(ProjectModel project, List<EnumModel> enums)
        {
            if (!GeneratorCode) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore;");
            codeContent.AppendLine($"using {project.PrefixName}.Core.EFRepository;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Domain;");
            codeContent.AppendLine($"using {project.PrefixName}.{project.ProjectName}.Domain.Repositories;");
            if (enums != null && enums.Count > 0)
            {
                codeContent.AppendLine($"using {enums[0].Namespace};");
            }
            if (UseCache)
            {
                codeContent.AppendLine($"using Materal.Utils.Cache;");
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.EFRepository.RepositoryImpl");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}仓储实现");
            codeContent.AppendLine($"    /// </summary>");
            if (UseCache)
            {
                codeContent.AppendLine($"    public partial class {_repositoryImplName}: {project.PrefixName}CacheRepositoryImpl<{Name}, Guid, {project.DBContextName}>, I{Name}Repository");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public {_repositoryImplName}({project.DBContextName} dbContext, ICacheHelper cacheManager) : base(dbContext, cacheManager) {{ }}");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 获得所有缓存名称");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        protected override string GetAllCacheName() => \"All{Name}\";");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {_repositoryImplName}: {project.PrefixName}EFRepositoryImpl<{Name}, Guid, {project.DBContextName}>, I{Name}Repository");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public {_repositoryImplName}({project.DBContextName} dbContext) : base(dbContext) {{ }}");
            }
            if (IsIndexDomain)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 获取最大位序");
                codeContent.AppendLine($"        /// </summary>");
                List<DomainPropertyModel> indexGourpProperties = Properties.Where(m => m.IsIndexGourpProperty).ToList();
                List<string> args = new();
                List<string> Lambdas = new();
                foreach (DomainPropertyModel indexGourpProperty in indexGourpProperties)
                {
                    string name = indexGourpProperty.Name.FirstLower();
                    codeContent.AppendLine($"        /// <param name=\"{name}\"></param>");
                    args.Add($"{indexGourpProperty.PredefinedType} {name}");
                    Lambdas.Add($"m.{indexGourpProperty.Name} == {name}");
                }
                string lambda = string.Join(" && ", Lambdas);
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        public async Task<int> GetMaxIndexAsync({string.Join(", ", args)})");
                codeContent.AppendLine($"        {{");
                if (indexGourpProperties.Count > 0)
                {
                    codeContent.AppendLine($"            if (!await DBSet.AnyAsync(m => {lambda})) return -1;");
                    codeContent.AppendLine($"            int result = await DBSet.Where(m => {lambda}).MaxAsync(m => m.Index);");
                }
                else
                {
                    codeContent.AppendLine($"            if (!await DBSet.AnyAsync()) return -1;");
                    codeContent.AppendLine($"            int result = await DBSet.MaxAsync(m => m.Index);");
                }
                codeContent.AppendLine($"            return result;");
                codeContent.AppendLine($"        }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "RepositoryImpl");
            codeContent.SaveFile(filePath, $"{_repositoryImplName}.g.cs");
        }
        /// <summary>
        /// 创建实体配置文件
        /// </summary>
        public void CreateEntityConfigFile(ProjectModel project)
        {
            if (!GeneratorCode || !IsTable) return;
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
            #region 有注释
            if (!string.IsNullOrWhiteSpace(Annotation))
            {
                codeContent.AppendLine($"            builder.ToTable(m => m.HasComment(\"{Annotation}\"));");
            }
            #endregion
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
                #region 有注释
                if (!string.IsNullOrWhiteSpace(property.Annotation))
                {
                    codeContent.Append($"\r\n                .HasComment(\"{property.Annotation}\")");
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
        public void CreateIRepositoryFile(ProjectModel project, List<EnumModel> enums)
        {
            if (!GeneratorCode) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.TTA.EFRepository;");
            codeContent.AppendLine($"using {project.PrefixName}.Core.Domain.Repositories;");
            if (enums != null && enums.Count > 0)
            {
                codeContent.AppendLine($"using {enums[0].Namespace};");
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.Domain.Repositories");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}仓储接口");
            codeContent.AppendLine($"    /// </summary>");
            if (!UseCache)
            {
                codeContent.AppendLine($"    public partial interface {_iRepositoryName} : I{project.PrefixName}Repository<{Name}, Guid>");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface {_iRepositoryName} : ICache{project.PrefixName}Repository<{Name}, Guid>");
            }
            codeContent.AppendLine($"    {{");
            if (IsIndexDomain)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 获取最大位序");
                codeContent.AppendLine($"        /// </summary>");
                List<DomainPropertyModel> indexGourpProperties = Properties.Where(m => m.IsIndexGourpProperty).ToList();
                List<string> args = new();
                foreach (DomainPropertyModel indexGourpProperty in indexGourpProperties)
                {
                    string name = indexGourpProperty.Name.FirstLower();
                    codeContent.AppendLine($"        /// <param name=\"{name}\"></param>");
                    args.Add($"{indexGourpProperty.PredefinedType} {name}");
                }
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        Task<int> GetMaxIndexAsync({string.Join(", ", args)});");
            }
            codeContent.AppendLine($"    }}");
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
            if (!GeneratorCode || !GeneratorQueryModel) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using Materal.BaseCore.DataTransmitModel;");
            DomainModel targetDomain;
            if (GeneratorQueryTargetService)
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
            if (!GeneratorCode || !GeneratorQueryModel) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"#nullable enable");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using Materal.BaseCore.DataTransmitModel;");
            DomainModel targetDomain;
            if (GeneratorQueryTargetService)
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
        /// <summary>
        /// 创建树形列表DTO文件
        /// </summary>
        /// <param name="project"></param>
        /// <param name="domains"></param>
        public void CreateTreeListDTOFile(ProjectModel project, List<DomainModel> domains)
        {
            if (!GeneratorCode || !GeneratorQueryModel || !IsTreeDomain) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.BaseCore.DataTransmitModel;");
            DomainModel targetDomain;
            if (GeneratorQueryTargetService)
            {
                targetDomain = domains.FirstOrDefault((m) => m.Name == _queryTargetName) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.DataTransmitModel.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}树列表数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_treeListDTOName}: {_listDTOName}, ITreeDTO<{_treeListDTOName}>");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 子级");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public List<{_treeListDTOName}> Children {{ get; set; }} = new();");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, Name);
            codeContent.SaveFile(filePath, $"{_treeListDTOName}.g.cs");
        }
        #endregion
        #region PresentationModel
        /// <summary>
        /// 创建查询请求文件
        /// </summary>
        public void CreateQueryRequestModelFile(ProjectModel project, List<DomainModel> domains)
        {
            if (!GeneratorCode || !GeneratorQueryModel) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine("#nullable enable");
            codeContent.AppendLine($"using Materal.Utils.Model;");
            codeContent.AppendLine($"using Materal.BaseCore.PresentationModel;");
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            DomainModel targetDomain;
            if (GeneratorQueryTargetService)
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
        /// 创建查询树请求模型文件
        /// </summary>
        /// <param name="project"></param>
        public void CreateQueryTreeRequestModelFile(ProjectModel project)
        {
            if (!GeneratorCode || !GeneratorQueryModel || !IsTreeDomain) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine("#nullable enable");
            codeContent.AppendLine($"using Materal.Utils.Model;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.PrefixName}.{project.ProjectName}.PresentationModel.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}查询模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_queryTreeListRequestModelName} : FilterModel");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 父级唯一标识");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public Guid? ParentID {{ get; set; }}");
            foreach (DomainPropertyModel property in Properties.Where(m => m.IsTreeGourpProperty))
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public {property.NullPredefinedType} {property.Name} {{ get; set; }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            string filePath = Path.Combine(project.GeneratorRootPath, "Models", Name);
            codeContent.SaveFile(filePath, $"{_queryTreeListRequestModelName}.g.cs");
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

                    if (property.IsContains && !property.IsStringContains)
                    {
                        codeContent.AppendLine($"        public List<{property.NotNullPredefinedType}>? {property.Name}Collections {{ get; set; }}");
                    }
                    else
                    {
                        codeContent.AppendLine($"        public {property.NullPredefinedType} {property.Name} {{ get; set; }}");
                    }
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
            if (domain.GeneratorExtendQuery)
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
            if (!GeneratorCode || !GeneratorService || !GeneratorWebAPI) return;
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
            if (!GeneratorCode || !GeneratorService || !GeneratorWebAPI) return;
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
            if (!property.HasQueryAttribute) return;
            if (property.IsContains && !property.IsStringContains)
            {
                codeContent.AppendLine($"        [Contains(\"{property.Name}\")]");
            }
            else
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
