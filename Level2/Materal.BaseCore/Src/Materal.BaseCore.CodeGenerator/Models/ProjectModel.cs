using Materal.BaseCore.CodeGenerator.Extensions;
using System.Text;

namespace Materal.BaseCore.CodeGenerator.Models
{
    public class ProjectModel
    {
        public ProjectModel()
        {
        }

        /// <summary>
        /// 磁盘文件夹路径
        /// </summary>
        public string DiskDirectoryPath { get; set; } = string.Empty;
        /// <summary>
        /// 项目根路径
        /// </summary>
        public string RootPath { get; set; } = string.Empty;
        /// <summary>
        /// 生成根路径
        /// </summary>
        public string GeneratorRootPath { get; set; } = string.Empty;
        /// <summary>
        /// 项目名称
        /// MBC.Demo.Service->Demo
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;
        /// <summary>
        /// 前缀
        /// MBC.Demo.Service->MBC
        /// </summary>
        public string PrefixName { get; set; } = string.Empty;
        /// <summary>
        /// 命名空间
        /// MBC.Demo.Service->MBC.Demo.Service
        /// </summary>
        public string Namespace { get; set; } = string.Empty;
        /// <summary>
        /// 数据库上下文名称
        /// </summary>
        public string DBContextName { get; set; } = string.Empty;
        /// <summary>
        /// 清空生成文件
        /// </summary>
        public void ClearMCGFiles()
        {
            if (Directory.Exists(GeneratorRootPath))
            {
                Directory.Delete(GeneratorRootPath, true);
            }
        }
        /// <summary>
        /// 创建枚举控制器
        /// </summary>
        /// <param name="enums"></param>
        public void CreateEnumsControllers(List<EnumModel> enums)
        {
            const string controllerName = "EnumsController";
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Materal.BaseCore.WebAPI.Controllers;");
            codeContent.AppendLine($"using Materal.Model;");
            codeContent.AppendLine($"using Materal.EnumHelper;");
            codeContent.AppendLine($"using Microsoft.AspNetCore.Authorization;");
            codeContent.AppendLine($"using Microsoft.AspNetCore.Mvc;");
            string[] enumNamespaces = enums.Select(m => m.Namespace).Distinct().ToArray();
            foreach (var item in enumNamespaces)
            {
                codeContent.AppendLine($"using {item};");
            }
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {PrefixName}.{ProjectName}.WebAPI.Controllers");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// 枚举控制器");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    [AllowAnonymous]");
            codeContent.AppendLine($"    public partial class {controllerName} : MateralCoreWebAPIControllerBase");
            codeContent.AppendLine($"    {{");
            foreach (var @enum in enums)
            {
                if (!@enum.GeneratorCode) continue;
                codeContent.AppendLine($"        /// <summary>");
                string? annotation = @enum.Annotation;
                if (string.IsNullOrWhiteSpace(annotation) || annotation == null)
                {
                    annotation = @enum.Name;
                }
                else if (!annotation.EndsWith("枚举"))
                {
                    annotation += "枚举";
                }
                codeContent.AppendLine($"        /// 获取所有{annotation}");
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
            string filePath = Path.Combine(GeneratorRootPath, "Controllers");
            codeContent.SaveFile(filePath, $"{controllerName}.g.cs");
        }
        /// <summary>
        /// 创建WebAPI文件
        /// </summary>
        /// <param name="domains"></param>
        public void CreateWebAPIFiles(List<DomainModel> domains)
        {
            ClearMCGFiles();
            foreach (DomainModel domain in domains)
            {
                domain.Init();
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
            ClearMCGFiles();
            foreach (DomainModel domain in domains)
            {
                domain.Init();
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
            ClearMCGFiles();
            foreach (DomainModel domain in domains)
            {
                domain.Init();
                domain.CreateServiceImplFile(this);
            }
        }
        /// <summary>
        /// 创建EF仓储实现文件
        /// </summary>
        /// <param name="domains"></param>
        public void CreateEFRepositoryFiles(List<DomainModel> domains)
        {
            ClearMCGFiles();
            CreateDBContextFile(domains);
            foreach (DomainModel domain in domains)
            {
                domain.Init();
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
            ClearMCGFiles();
            foreach (DomainModel domain in domains)
            {
                domain.Init();
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
            ClearMCGFiles();
            foreach (DomainModel domain in domains)
            {
                domain.Init();
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
            ClearMCGFiles();
            foreach (DomainModel domain in domains)
            {
                domain.Init();
                domain.CreateIRepositoryFile(this);
            }
        }
        /// <summary>
        /// 创建HttpClient文件
        /// </summary>
        /// <param name="controllers"></param>
        public void CreateHttpClientFiles(List<ControllerModel> controllers)
        {
            ClearMCGFiles();
            foreach (ControllerModel controller in controllers)
            {
                controller.CreateHttpClientFile(this);
            }
        }
        /// <summary>
        /// 创建DBContext
        /// </summary>
        public void CreateDBContextFile(List<DomainModel> domains)
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine("using Microsoft.EntityFrameworkCore;");
            codeContent.AppendLine("using System.Reflection;");
            string[] domainNamespaces = domains.Select(m => m.Namespace).Distinct().ToArray();
            foreach (var item in domainNamespaces)
            {
                codeContent.AppendLine($"using {item};");
            }
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
    }
}
