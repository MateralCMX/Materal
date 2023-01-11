using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MateralBaseCoreVSIX.Models
{
    public class ProjectModel
    {
        /// <summary>
        /// 磁盘文件夹路径
        /// </summary>
        public string DiskDirectoryPath { get; }
        /// <summary>
        /// 项目根路径
        /// </summary>
        public string RootPath { get; }
        /// <summary>
        /// 生成根路径
        /// </summary>
        public string GeneratorRootPath { get; }
        /// <summary>
        /// 项目名称
        /// MBC.Demo.Service->Demo
        /// </summary>
        public string ProjectName { get; }
        /// <summary>
        /// 前缀
        /// MBC.Demo.Service->MBC
        /// </summary>
        public string PrefixName { get; }
        /// <summary>
        /// 命名空间
        /// MBC.Demo.Service->MBC.Demo.Service
        /// </summary>
        public string Namespace { get; }
        /// <summary>
        /// 数据库上下文名称
        /// </summary>
        public string DBContextName { get; }
        public ProjectModel(Project project)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            RootPath = Path.GetDirectoryName(project.FullName);
            int biasIndex = RootPath.LastIndexOf('\\');
            DiskDirectoryPath = RootPath.Substring(0, biasIndex);
            GeneratorRootPath = Path.Combine(RootPath, "MCG");
            Namespace = project.Name;
            string[] names = project.Name.Split('.');
            if (names.Length >= 2)
            {
                ProjectName = names[1];
                PrefixName = names[0];
            }
            else
            {
                ProjectName = Namespace;
                PrefixName = ProjectName;
            }

            DBContextName = $"{ProjectName}DBContext";
        }
        /// <summary>
        /// 创建WebAPI文件
        /// </summary>
        /// <param name="domains"></param>
        public void CreateWebAPIFiles(List<DomainModel> domains)
        {
            ClearMCG();
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
            ClearMCG();
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
            ClearMCG();
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
            ClearMCG();
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
            ClearMCG();
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
            ClearMCG();
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
            ClearMCG();
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
            codeContent.AppendLine($"namespace {Namespace}");
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
            codeContent.SaveFile(GeneratorRootPath, $"{DBContextName}.g.cs");
        }
        /// <summary>
        /// 清空MCG文件
        /// </summary>
        private void ClearMCG()
        {
            if (Directory.Exists(GeneratorRootPath))
            {
                Directory.Delete(GeneratorRootPath, true);
            }
        }
    }
}
