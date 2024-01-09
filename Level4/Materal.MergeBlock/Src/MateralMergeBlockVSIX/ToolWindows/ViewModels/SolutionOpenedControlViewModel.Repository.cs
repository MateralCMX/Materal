#nullable enable
using Materal.BaseCore.CodeGenerator;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedControlViewModel : ObservableObject
    {
        /// <summary>
        /// 创建实体配置代码
        /// </summary>
        [GeneratorCodeMethod]
        private void GeneratorEntityConfigsCode(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorEntityConfigCode(domain);
            }
        }
        /// <summary>
        /// 创建实体配置代码
        /// </summary>
        private void GeneratorEntityConfigCode(DomainModel domain)
        {
            if (domain.HasAttribute<NotEntityConfigAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore.Metadata.Builders;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.EFRepository.EntityConfigs");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}配置基类");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public class {domain.Name}ConfigBase : BaseEntityConfig<{domain.Name}>");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 配置");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        public override void Configure(EntityTypeBuilder<{domain.Name}> builder)");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            builder = BaseConfigure(builder);");
            codeContent.AppendLine($"            builder.ToTable(m => m.HasComment(\"{domain.Annotation}\"));");
            foreach (PropertyModel property in domain.Properties)
            {
                codeContent.AppendLine($"            builder.Property(e => e.{property.Name})");
                if (!property.CanNull)
                {
                    codeContent.AppendLine($"                .IsRequired()");
                }
                codeContent.AppendLine($"                .HasComment(\"{property.Annotation}\")");
                AttributeModel? attribute = property.GetAttribute<StringLengthAttribute>();
                if (attribute is not null)
                {
                    codeContent.AppendLine($"                .HasMaxLength({attribute.GetAttributeArgument()?.Value})");
                }
                codeContent.Insert(codeContent.Length - 2, ";");
            }
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}配置类");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {domain.Name}Config : {domain.Name}ConfigBase {{ }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleReposiroty, "EntityConfigs", $"{domain.Name}Config.cs");
        }
        /// <summary>
        /// 创建数据库上下文代码
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorDBContextCode(List<DomainModel> domains)
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Repository");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {_moduleName}数据库上下文");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public sealed partial class {_moduleName}DBContext(DbContextOptions<{_moduleName}DBContext> options) : DbContext(options)");
            codeContent.AppendLine($"    {{");
            foreach (DomainModel domain in domains)
            {
                if (domain.HasAttribute<NotInDBContextAttribute>()) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {domain.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public DbSet<{domain.Name}> {domain.Name} {{ get; set; }}");
            }
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 配置模型");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleReposiroty, $"{_moduleName}DBContext.cs");
        }
        /// <summary>
        /// 创建工作单元代码
        /// </summary>
        [GeneratorCodeMethod]
        private void GeneratorUnitOfWorkCode()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Repository");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {_moduleName}工作单元");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public partial class {_moduleName}UnitOfWorkImpl({_moduleName}DBContext context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<{_moduleName}DBContext>(context, serviceProvider) {{ }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleReposiroty, $"{_moduleName}UnitOfWorkImpl.cs");
        }
        /// <summary>
        /// 创建仓储代码
        /// </summary>
        /// <param name="domains"></param>
        [GeneratorCodeMethod]
        private void GeneratorRepositoryCode(List<DomainModel> domains)
        {
            foreach (DomainModel domain in domains)
            {
                GeneratorIRepositoryCode(domain);
                GeneratorRepositoryImplCode(domain);
            }
        }
        /// <summary>
        /// 创建仓储接口代码
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorIRepositoryCode(DomainModel domain)
        {
            if (domain.HasAttribute<NotRepositoryAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Repositories");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}仓储");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<CacheAttribute>())
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Repository : I{_projectName}CacheRepository<{domain.Name}, Guid> {{ }}");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Repository : I{_projectName}Repository<{domain.Name}, Guid> {{ }}");
            }
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleAbstractions, "Repositories", $"I{domain.Name}Repository.cs");
        }
        /// <summary>
        /// 创建仓储实现代码
        /// </summary>
        /// <param name="domain"></param>
        private void GeneratorRepositoryImplCode(DomainModel domain)
        {
            if (domain.HasAttribute<NotRepositoryAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Repository.Repositories");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}仓储");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<CacheAttribute>())
            {
                codeContent.AppendLine($"    public partial class {domain.Name}RepositoryImpl({_moduleName}DBContext dbContext, ICacheHelper cacheManager) : {_projectName}CacheRepositoryImpl<{domain.Name}, Guid, {_moduleName}DBContext>(dbContext, cacheManager), I{domain.Name}Repository {{ }}");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {domain.Name}RepositoryImpl({_moduleName}DBContext dbContext) : {_projectName}RepositoryImpl<{domain.Name}, Guid, {_moduleName}DBContext>(dbContext), I{domain.Name}Repository {{ }}");
            }
            codeContent.AppendLine($"}}");
            codeContent.SaveAs(_moduleReposiroty, "Repositories", $"{domain.Name}RepositoryImpl.cs");
        }
    }
}
