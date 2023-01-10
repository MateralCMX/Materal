using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MateralVSHelper.CodeGenerator
{
    public class ProjectModel
    {
        /// <summary>
        /// 根路径
        /// </summary>
        public string RootPath { get; }
        private readonly string _projectName;
        /// <summary>
        /// 前缀
        /// </summary>
        public string PrefixName { get; }
        #region 命名空间
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace { get; }
        /// <summary>
        /// Domain命名空间
        /// </summary>
        public string DomainNamespace { get; }
        /// <summary>
        /// 仓储命名空间
        /// </summary>
        public string IRepositoryNamespace { get; }
        /// <summary>
        /// 仓储实现命名空间
        /// </summary>
        public string RepositoryImplNamespace { get; }
        /// <summary>
        /// 数据传输模型命名空间
        /// </summary>
        public string DataTransmitModelNamespace { get; }
        /// <summary>
        /// 表现模型命名空间
        /// </summary>
        public string PresentationModelNamespace { get; }
        /// <summary>
        /// 服务命名空间
        /// </summary>
        public string ServiceNamespace { get; }
        /// <summary>
        /// 服务实现命名空间
        /// </summary>
        public string ServiceImplNamespace { get; }
        /// <summary>
        /// WebAPI命名空间
        /// </summary>
        public string WebAPINamespace { get; }
        #endregion
        #region 文件名
        /// <summary>
        /// 数据库上下文名称
        /// </summary>
        public string DBContextName { get; }
        #endregion
        public ProjectModel(Project project, string rootPath)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            RootPath = Path.Combine(rootPath, project.Name, "MCG");
            Namespace = project.Name;
            int dotIndex = Namespace.LastIndexOf('.');
            if(dotIndex > 0)
            {
                _projectName = Namespace.Substring(dotIndex + 1);
                PrefixName = Namespace.Substring(0, dotIndex);
            }
            else
            {
                _projectName = Namespace;
                PrefixName = _projectName;
            }
            DomainNamespace = $"{Namespace}.Domain";
            IRepositoryNamespace = $"{Namespace}.Repositories";
            RepositoryImplNamespace = $"{Namespace}.EFRepository";
            DataTransmitModelNamespace = $"{Namespace}.DataTransmitModel";
            PresentationModelNamespace = $"{Namespace}.PresentationModel";
            ServiceNamespace = $"{Namespace}.Services";
            ServiceImplNamespace = $"{Namespace}.ServiceImpl"; 
            WebAPINamespace = $"{Namespace}.WebAPI";

            DBContextName = $"{_projectName}DBContext";
        }
        /// <summary>
        /// 创建WebAPI文件
        /// </summary>
        /// <param name="domains"></param>
        public void CreateWebAPIFiles(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                domain.Init(this);
                domain.CreateWebAPIControllerFile(this);
                domain.CreateAutoMapperProfileFile(this);
            }
        }
        /// <summary>
        /// 创建服务文件
        /// </summary>
        /// <param name="domains"></param>
        public void CreateServicesFiles(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                domain.Init(this);
                domain.CreateIServiceFile(this);
                domain.CreateAddModelFile(this);
                domain.CreateEditModelFile(this);
                domain.CreateQueryModelFile(this, domains);
            }
        }
        /// <summary>
        /// 创建服务实现文件
        /// </summary>
        /// <param name="domains"></param>
        public void CreateServiceImplFiles(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                domain.Init(this);
                domain.CreateServiceImplFile(this);
            }
        }
        /// <summary>
        /// 创建EF仓储实现文件
        /// </summary>
        /// <param name="domains"></param>
        public void CreateEFRepositoryFiles(List<DomainModel> domains)
        {
            CreateDBContextFile(domains);
            foreach (DomainModel domain in domains)
            {
                domain.Init(this);
                domain.CreateEntityConfigFile(this);
                domain.CreateRepositoryImplFile(this);
            }
        }
        /// <summary>
        /// 创建数据传输模型文件
        /// </summary>
        /// <param name="domains"></param>
        public void CreateDataTransmitModelFiles(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                domain.Init(this);
                domain.CreateListDTOFile(this, domains);
                domain.CreateDTOFile(this, domains);
            }
        }
        /// <summary>
        /// 创建数据传输模型文件
        /// </summary>
        /// <param name="domains"></param>
        public void CreatePresentationModelFiles(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                domain.Init(this);
                domain.CreateAddRequestModelFile(this);
                domain.CreateEditRequestModelFile(this);
                domain.CreateQueryRequestModelFile(this, domains);
            }
        }
        /// <summary>
        /// 创建Domain文件
        /// </summary>
        /// <param name="domains"></param>
        public void CreateDomainFiles(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                domain.Init(this);
                domain.CreateIRepositoryFile(this);
            }
        }
        /// <summary>
        /// 创建DBContext
        /// </summary>
        private void CreateDBContextFile(List<DomainModel> domains)
        {
            string[] domainNamespaces = domains.Select(m => m.Namespace).Distinct().ToArray();
            StringBuilder codeContent = new StringBuilder();
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
            codeContent.AppendLine($"    /// {_projectName}数据库上下文");
            codeContent.AppendLine("    /// </summary>");
            codeContent.AppendLine($"    public sealed partial class {DBContextName} : DbContext");
            codeContent.AppendLine("    {");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 构造方法");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public {DBContextName}(DbContextOptions<{DBContextName}> options) : base(options) {{ }}");
            foreach (DomainModel domain in domains)
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
            codeContent.SaveFile(RootPath, $"{DBContextName}.g.cs");
        }
    }
}
