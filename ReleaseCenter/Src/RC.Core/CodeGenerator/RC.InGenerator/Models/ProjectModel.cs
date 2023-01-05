using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RC.InGenerator.Models
{
    /// <summary>
    /// 项目类型
    /// </summary>
    public class ProjectModel
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; private set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; private set; }
        #region 文件名
        /// <summary>
        /// 数据库上下文名称
        /// </summary>
        public string DBContextName => $"{ProjectName}DBContext";
        /// <summary>
        /// 枚举控制器名称
        /// </summary>
        public const string EnumsControllerName = "EnumsController";
        #endregion
        #region 命名空间
        /// <summary>
        /// 枚举命名空间
        /// </summary>
        public string EnumNamespace { get; private set; }
        /// <summary>
        /// 领域命名空间
        /// </summary>
        public string DomainNamespace { get; private set; }
        /// <summary>
        /// 服务命名空间
        /// </summary>
        public string ServiceNamespace { get; private set; }
        /// <summary>
        /// 服务实现命名空间
        /// </summary>
        public string ServiceImplNamespace { get; private set; }
        /// <summary>
        /// WebAPI命名空间
        /// </summary>
        public string WebAPINamespace { get; private set; }
        /// <summary>
        /// 仓储命名空间
        /// </summary>
        public string IRepositoryNamespace { get; private set; }
        /// <summary>
        /// 仓储实现命名空间
        /// </summary>
        public string RepositoryImplNamespace { get; private set; }
        /// <summary>
        /// 数据传输模型命名空间
        /// </summary>
        public string DataTransmitModelNamespace { get; private set; }
        /// <summary>
        /// 展示模型命名空间
        /// </summary>
        public string PresentationModelNamespace { get; private set; }
        #endregion
        /// <summary>
        /// 领域模型组
        /// </summary>
        public List<DomainModel> Domains { get; private set; } = new();
        public List<EnumModel> Enums { get; private set; } = new();
        public SourceProductionContext Context { get; }
        public ProjectModel(SourceProductionContext context, NamespaceDeclarationSyntax namespaceDeclaration)
        {
            Context = context;
            Namespace = namespaceDeclaration.Name.ToString();
            int index = Namespace.LastIndexOf(".Domain");
            if(index < 0)
            {
                index = Namespace.LastIndexOf(".Enums");
            }
            Namespace = Namespace.Substring(0, index);
            ServiceNamespace = Namespace + ".Services";
            ServiceImplNamespace = Namespace + ".ServiceImpl";
            WebAPINamespace = Namespace + ".WebAPI";
            IRepositoryNamespace = Namespace + ".Repositories";
            RepositoryImplNamespace = Namespace + ".RepositoryImpl";
            DataTransmitModelNamespace = Namespace + ".DataTransmitModel";
            PresentationModelNamespace = Namespace + ".PresentationModel";
            DomainNamespace = Namespace + ".Domain";
            EnumNamespace = Namespace + ".Enums";
            index = Namespace.IndexOf(".");
            if (index >= 0)
            {
                ProjectName = Namespace.Substring(index + 1);
            }
            else
            {
                ProjectName = Namespace;
            }
        }
        /// <summary>
        /// 添加一个领域模型
        /// </summary>
        /// <param name="context"></param>
        /// <param name="classDeclaration"></param>
        public void AddDomain(ClassDeclarationSyntax domainClassDeclaration)
        {
            Domains.Add(new DomainModel(Context, domainClassDeclaration, this));
        }
        /// <summary>
        /// 添加一个枚举
        /// </summary>
        /// <param name="context"></param>
        /// <param name="classDeclaration"></param>
        public void AddEnum(EnumDeclarationSyntax enumDeclaration)
        {
            Enums.Add(new EnumModel(enumDeclaration));
        }
        /// <summary>
        /// 创建DBContext
        /// </summary>
        public void CreateDBContext()
        {
            string[] domainNamespaces = Domains.Select(m => m.Namespace).Distinct().ToArray();
            StringBuilder codeContent = new();
            foreach (var item in domainNamespaces)
            {
                codeContent.AppendLine($"using {item};");
            }
            codeContent.AppendLine("using Microsoft.EntityFrameworkCore;");
            codeContent.AppendLine("using System.Reflection;");
            codeContent.AppendLine("");
            codeContent.AppendLine($"namespace {RepositoryImplNamespace}");
            codeContent.AppendLine("{");
            codeContent.AppendLine("    /// <summary>");
            codeContent.AppendLine($"    /// {ProjectName}数据库上下文");
            codeContent.AppendLine("    /// </summary>");
            codeContent.AppendLine($"    public sealed partial class {DBContextName} : DbContext");
            codeContent.AppendLine("    {");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 构造方法");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public {DBContextName}(DbContextOptions<{DBContextName}> options) : base(options) {{ }}");
            foreach (DomainModel domain in Domains)
            {
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {domain.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public DbSet<{domain.Name}> {domain.Name} {{ get; set; }}");
            }
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 配置模型");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine("        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());");
            codeContent.AppendLine("    }");
            codeContent.AppendLine("}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{DBContextName}.g.cs", sourceText);
        }
        /// <summary>
        /// 创建枚举控制器
        /// </summary>
        public void CreateEnumsController()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.EnumHelper;");
            codeContent.AppendLine($"using Materal.Model;");
            codeContent.AppendLine($"using Microsoft.AspNetCore.Authorization;");
            codeContent.AppendLine($"using Microsoft.AspNetCore.Mvc;");
            codeContent.AppendLine($"using RC.Core.WebAPI.Controllers;");
            codeContent.AppendLine($"using {EnumNamespace};");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {WebAPINamespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 枚举控制器");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    [AllowAnonymous]");
            codeContent.AppendLine($"    public class {EnumsControllerName} : RCWebAPIControllerBase");
            codeContent.AppendLine($"    {{");
            foreach (EnumModel @enum in Enums)
            {
                if (!@enum.GeneratorWebAPI) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 获取所有{@enum.Annotation}枚举");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        /// <returns></returns>");
                codeContent.AppendLine($"        [HttpGet]");
                codeContent.AppendLine($"        public ResultModel<List<KeyValueModel<{@enum.Name}>>> GetAll{@enum.Name}()");
                codeContent.AppendLine($"        {{");
                codeContent.AppendLine($"            List<KeyValueModel<{@enum.Name}>> result = KeyValueModel<{@enum.Name}>.GetAllCode();");
                codeContent.AppendLine($"            return ResultModel<List<KeyValueModel<{@enum.Name}>>>.Success(result, \"获取成功\");");
                codeContent.AppendLine($"        }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            SourceText sourceText = SourceText.From(codeContent.ToString(), Encoding.UTF8);
            Context.AddSource($"{EnumsControllerName}.g.cs", sourceText);
        }
    }
}
