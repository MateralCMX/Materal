using EnvDTE;
using Materal.CodeGenerator;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;

namespace MateralVSHelper.CodeGenerator
{
    public class DomainModel
    {
        /// <summary>
        /// 引用组
        /// </summary>
        public List<string> Usings { get; } = new List<string>();
        /// <summary>
        /// 其他引用组
        /// </summary>
        public List<string> OtherUsings { get; } = new List<string>();
        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; }
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
        public List<AttributeModel> Attributes { get; } = new List<AttributeModel>();
        /// <summary>
        /// 属性
        /// </summary>
        public List<DomainPropertyModel> Properties { get; } = new List<DomainPropertyModel>();
        /// <summary>
        /// 使用缓存
        /// </summary>
        private bool _useCache => Attributes.HasAttribute<CacheAttribute>();
        /// <summary>
        /// 生成目标查询服务
        /// </summary>
        private bool _generatorQueryTargetService => Attributes.HasAttribute<QueryTragetGeneratorAttribute>();
        #region 文件名称
        private readonly string _entityConfigName;
        private readonly string _iRepositoryName;
        private readonly string _repositoryImplName;
        private readonly string _listDTOName;
        private readonly string _dtoName;
        private readonly string _addModelName;
        private readonly string _editModelName;
        #endregion
        public DomainModel(string[] codes, int classLineIndex)
        {
            #region 解析Class
            {
                int startIndex = classLineIndex;
                #region 解析名称
                if(startIndex < 0 || startIndex >= codes.Length) throw new VSHelperException("类行位序错误");
                const string classTag = " class ";
                Name = codes[startIndex];
                int classIndex = Name.IndexOf(classTag);
                if (classIndex <= 0) throw new VSHelperException("模型不是类");
                Name = Name.Substring(classIndex + classTag.Length);
                int domainIndex = Name.IndexOf(" : BaseDomain, IDomain");
                if (domainIndex <= 0) throw new VSHelperException("模型不是Domain");
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
            #endregion
        }
        /// <summary>
        /// 创建默认文件
        /// </summary>
        public void CreateDefaultFiles(ProjectModel project, List<DomainModel> domains)
        {
            if (Attributes.HasAttribute<NotGeneratorAttribute>()) return;
            #region 获取其他引用
            string[] blackList = new[]
            {
                "using Materal.Model;",
                "using System.ComponentModel.DataAnnotations;",
                $"using {project.PrefixName}.Core.Domain;",
                $"using {project.PrefixName}.Core.Models;",
                $"using {project.PrefixName}.Core.CodeGenerator;",
            };
            OtherUsings.Clear();
            foreach (string usingCode in Usings)
            {
                if (blackList.Contains(usingCode.Trim())) continue;
                OtherUsings.Add(usingCode.Trim());
            }
            #endregion
            CreateIRepositoryFile(project);
            CreateRepositoryImplFile(project);
            CreateEntityConfigFile(project);
            //if (GeneratorService || GeneratorQueryModel)
            //{
            //    CreateAutoMapperProfile(project);
            //}
            //if (GeneratorQueryModel)
            {
                //CreateQueryModelFile(project);
                //CreateQueryRequestModelFile(project);
                CreateListDTOFile(project, domains);
                CreateDTOFile(project, domains);
            }
            //if (!GeneratorService) return;
            CreateAddModelFile(project);
            CreateEditModelFile(project);
            //CreateIServiceFile(project);
            //CreateServiceImplFile(project);
            //if (!GeneratorWebAPI) return;
            //CreateAddRequestModelFile(project);
            //CreateEditRequestModelFile(project);
            //CreateWebAPIControllerFile(project);

            //CreateEntityConfigFile(project);
            //CreateIRepositoryFile(project);
            //CreateRepositoryImplFile(project);
            //CreateListDTOFile(project, domains);
            //CreateDTOFile(project, domains);
            //CreateAddModelFile(project);
            //CreateEditModelFile(project);
        }
        /// <summary>
        /// 创建修改模型文件
        /// </summary>
        private void CreateEditModelFile(ProjectModel project)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using RC.Core.Models;");
            codeContent.AppendLine($"using RC.Core.Services;");
            AppendOtherUsings(codeContent, this);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.ServiceNamespace}.Models.{Name}");
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
                AppendValidationCode(codeContent, property);
                codeContent.AppendLine($"        public {property.PredefinedType} {property.Name} {{ get; set; }} {property.Initializer}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(project.RootPath, $"{_editModelName}.g.cs");
        }
        /// <summary>
        /// 创建添加模型文件
        /// </summary>
        private void CreateAddModelFile(ProjectModel project)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using RC.Core.Services;");
            codeContent.AppendLine($"using RC.Core.Models;");
            AppendOtherUsings(codeContent, this);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.ServiceNamespace}.Models.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}添加模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_addModelName} : IServiceModel");
            codeContent.AppendLine($"    {{");
            foreach (DomainPropertyModel property in Properties)
            {
                if (!property.GeneratorAddModel) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {property.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                AppendValidationCode(codeContent, property);
                codeContent.AppendLine($"        public {property.Type} {property.Name} {{ get; set; }} {property.Initializer}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(project.RootPath, $"{_addModelName}.g.cs");
        }
        /// <summary>
        /// 创建DTO文件
        /// </summary>
        private void CreateDTOFile(ProjectModel project, List<DomainModel> domains)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using RC.Core.DataTransmitModel;");
            codeContent.AppendLine($"using RC.Core.Models;");
            codeContent.AppendLine($"using System;");
            DomainModel targetDomain;
            if (_generatorQueryTargetService)
            {
                AttributeModel attributeModel = Attributes.GetAttribute<QueryTragetGeneratorAttribute>();
                AttributeArgumentModel target = attributeModel.AttributeArguments.First(m => string.IsNullOrWhiteSpace(m.Target));
                targetDomain = domains.FirstOrDefault((DomainModel m) => m.Name == target.Value) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            AppendOtherUsings(codeContent, targetDomain);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.DataTransmitModelNamespace}.{Name}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}数据传输模型");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_dtoName} : {_listDTOName}, IDTO");
            codeContent.AppendLine($"    {{");
            FillDTOProperty(targetDomain, codeContent);
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(project.RootPath, $"{_dtoName}.g.cs");
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
                AppendValidationCode(codeContent, property);
                codeContent.AppendLine($"        public {property.Type} {property.Name} {{ get; set; }} {property.Initializer}");
            }
        }
        /// <summary>
        /// 创建列表DTO文件
        /// </summary>
        private void CreateListDTOFile(ProjectModel project, List<DomainModel> domains)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"using System.ComponentModel.DataAnnotations;");
            codeContent.AppendLine($"using {project.PrefixName}.Core.DataTransmitModel;");
            codeContent.AppendLine($"using {project.PrefixName}.Core.Models;");
            codeContent.AppendLine($"using System;");
            DomainModel targetDomain;
            if (_generatorQueryTargetService)
            {
                AttributeModel attributeModel = Attributes.GetAttribute<QueryTragetGeneratorAttribute>();
                AttributeArgumentModel target = attributeModel.AttributeArguments.First(m => string.IsNullOrWhiteSpace(m.Target));
                targetDomain = domains.FirstOrDefault((DomainModel m) => m.Name == target.Value) ?? this;
            }
            else
            {
                targetDomain = this;
            }
            AppendOtherUsings(codeContent, targetDomain);
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.DataTransmitModelNamespace}.{Name}");
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
            codeContent.SaveFile(project.RootPath, $"{_listDTOName}.g.cs");
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
                AppendValidationCode(codeContent, property);
                codeContent.AppendLine($"        public {property.Type} {property.Name} {{ get; set; }} {property.Initializer}");
            }
        }
        /// <summary>
        /// 创建仓储实现文件
        /// </summary>
        private void CreateRepositoryImplFile(ProjectModel project)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"using {Namespace};");
            codeContent.AppendLine($"using {project.IRepositoryNamespace};");
            codeContent.AppendLine($"using {project.PrefixName}.Core.EFRepository;");
            if (_useCache)
            {
                codeContent.AppendLine($"using Materal.CacheHelper;");
            }
            codeContent.AppendLine($"using System;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.RepositoryImplNamespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {Annotation}仓储实现");
            codeContent.AppendLine($"    /// </summary>");
            if (_useCache)
            {
                codeContent.AppendLine($"    public partial class {_repositoryImplName}: RCCacheRepositoryImpl<{Name}, Guid>, I{Name}Repository");
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
                codeContent.AppendLine($"    public partial class {_repositoryImplName}: RCEFRepositoryImpl<{Name}, Guid>, I{Name}Repository");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 构造方法");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public {_repositoryImplName}({project.DBContextName} dbContext) : base(dbContext) {{ }}");
                codeContent.AppendLine($"    }}");
            }
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(project.RootPath, $"{_repositoryImplName}.g.cs");
        }
        /// <summary>
        /// 创建仓储接口文件
        /// </summary>
        private void CreateIRepositoryFile(ProjectModel project)
        {
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"using {Namespace};");
            codeContent.AppendLine($"using Materal.TTA.EFRepository;");
            codeContent.AppendLine($"using System;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.IRepositoryNamespace}");
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
            codeContent.SaveFile(project.RootPath, $"{_iRepositoryName}.g.cs");
        }
        /// <summary>
        /// 创建实体配置文件
        /// </summary>
        private void CreateEntityConfigFile(ProjectModel project)
        {
            #region EntityConfig
            StringBuilder codeContent = new StringBuilder();
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore;");
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore.Metadata.Builders;");
            codeContent.AppendLine($"using {project.PrefixName}.Core.EFRepository;");
            codeContent.AppendLine($"using {Namespace};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {project.RepositoryImplNamespace}");
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
                AttributeModel stringLengthAttribute = property.GetAttribute<StringLengthAttribute>();
                if (stringLengthAttribute != null)
                {
                    AttributeArgumentModel maxLengthArgument = stringLengthAttribute.GetAttributeArgument();
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
                    AttributeArgumentModel columnTypeArgument = columnTypeAttribute.GetAttributeArgument();
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
            codeContent.SaveFile(project.RootPath, $"{_entityConfigName}.g.cs");
            #endregion
        }
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
        /// 添加验证代码
        /// </summary>
        /// <param name="codeContent"></param>
        /// <param name="property"></param>
        private static void AppendValidationCode(StringBuilder codeContent, DomainPropertyModel property)
        {
            if (property.HasValidationAttribute)
            {
                codeContent.Append($"        [");
                List<string> attributesString = new List<string>();
                foreach (AttributeModel attribute in property.ValidationAttributes)
                {
                    attributesString.Add(attribute.ToString());
                }
                codeContent.Append(string.Join(", ", attributesString));
                codeContent.AppendLine($"]");
            }
        }
    }
}
