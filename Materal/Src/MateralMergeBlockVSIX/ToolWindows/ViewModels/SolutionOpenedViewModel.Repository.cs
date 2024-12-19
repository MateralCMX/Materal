#nullable enable
using Materal.Extensions;
using Materal.MergeBlock.GeneratorCode.Attributers;
using Materal.MergeBlock.GeneratorCode.Extensions;
using Materal.MergeBlock.GeneratorCode.Models;
using MateralMergeBlockVSIX.Extensions;
using MateralMergeBlockVSIX.ToolWindows.Attributes;
using Microsoft.VisualStudio.PlatformUI;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MateralMergeBlockVSIX.ToolWindows.ViewModels
{
    public partial class SolutionOpenedViewModel : ObservableObject
    {
        /// <summary>
        /// 创建实体配置代码
        /// </summary>
        [GeneratorCodeMethod]
        private async Task GeneratorEntityConfigsCodeAsync()
        {
            foreach (DomainModel domain in Context.Domains)
            {
                await GeneratorEntityConfigCodeAsync(domain);
            }
        }
        /// <summary>
        /// 创建实体配置代码
        /// </summary>
        private async Task GeneratorEntityConfigCodeAsync(DomainModel domain)
        {
            if (domain.HasAttribute<NotEntityConfigAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorEntityConfigCodeAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"using Microsoft.EntityFrameworkCore.Metadata.Builders;");
            codeContent.AppendLine($"");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Repository.EntityConfigs");
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
            if (domain.IsView)
            {
                codeContent.AppendLine($"            builder.ToView(\"{domain.Name}\");");
            }
            else
            {
                codeContent.AppendLine($"            builder.ToTable(m => m.HasComment(\"{domain.Annotation}\"));");
            }
            foreach (PropertyModel property in domain.Properties)
            {
                if (property.HasAttribute<NotEntityConfigAttribute>()) continue;
                codeContent.AppendLine($"            builder.Property(e => e.{property.Name})");
                if (!property.CanNull)
                {
                    codeContent.AppendLine($"                .IsRequired()");
                }
                codeContent.AppendLine($"                .HasComment(\"{property.Annotation}\")");
                AttributeArgumentModel? columnTypeArgument = property.GetAttribute<ColumnTypeAttribute>()?.GetAttributeArgument();
                if (columnTypeArgument is not null)
                {
                    codeContent.AppendLine($"                .HasColumnType({columnTypeArgument.Value})");
                }
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
            await codeContent.SaveAsAsync(Context, _moduleRepository, "EntityConfigs", $"{domain.Name}Config.cs");
        }
        /// <summary>
        /// 创建数据库上下文代码
        /// </summary>
        [GeneratorCodeMethod]
        private async Task GeneratorDBContextCodeAsync()
        {
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorDBContextCodeAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Repository");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {_moduleName}数据库上下文");
            codeContent.AppendLine($"    /// </summary>");
            codeContent.AppendLine($"    public sealed partial class {_moduleName}DBContext(DbContextOptions<{_moduleName}DBContext> options) : DbContext(options)");
            codeContent.AppendLine($"    {{");
            foreach (DomainModel domain in Context.Domains)
            {
                if (domain.HasAttribute<NotInDBContextAttribute>()) continue;
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// {domain.Annotation}");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        public DbSet<{domain.Name}>? {domain.Name} {{ get; set; }}");
            }
            codeContent.AppendLine($"        /// <summary>");
            codeContent.AppendLine($"        /// 配置模型");
            codeContent.AppendLine($"        /// </summary>");
            codeContent.AppendLine($"        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleRepository, $"{_moduleName}DBContext.cs");
        }
        /// <summary>
        /// 创建仓储代码
        /// </summary>
        [GeneratorCodeMethod]
        private async Task GeneratorRepositoryCodeAsync()
        {
            foreach (DomainModel domain in Context.Domains)
            {
                await GeneratorIRepositoryCodeAsync(domain);
                await GeneratorRepositoryImplCodeAsync(domain);
            }
        }
        /// <summary>
        /// 创建仓储接口代码
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorIRepositoryCodeAsync(DomainModel domain)
        {
            if (domain.HasAttribute<NotRepositoryAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorIRepositoryCodeAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Abstractions.Repositories");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}仓储");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<CacheAttribute>())
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Repository : I{_moduleName}CacheRepository<{domain.Name}>");
            }
            else
            {
                codeContent.AppendLine($"    public partial interface I{domain.Name}Repository : I{_moduleName}Repository<{domain.Name}>");
            }
            codeContent.AppendLine($"    {{");
            if (domain.IsIndexDomain && !domain.HasAttribute<EmptyIndexAttribute>())
            {
                PropertyModel? indexGroupPropertyModel = domain.GetIndexGroupProperty();
                if (indexGroupPropertyModel is null)
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// 获取最大位序");
                    codeContent.AppendLine($"        /// </summary>");
                    codeContent.AppendLine($"        /// <returns></returns>");
                    codeContent.AppendLine($"        Task<int> GetMaxIndexAsync();");
                }
                else
                {
                    codeContent.AppendLine($"        /// <summary>");
                    codeContent.AppendLine($"        /// 获取最大位序");
                    codeContent.AppendLine($"        /// </summary>");
                    codeContent.AppendLine($"        /// <param name=\"{indexGroupPropertyModel.Name.FirstLower()}\"></param>");
                    codeContent.AppendLine($"        /// <returns></returns>");
                    codeContent.AppendLine($"        Task<int> GetMaxIndexAsync({indexGroupPropertyModel.PredefinedType} {indexGroupPropertyModel.Name.FirstLower()});");
                }
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleAbstractions, "Repositories", $"I{domain.Name}Repository.cs");
        }
        /// <summary>
        /// 创建仓储实现代码
        /// </summary>
        /// <param name="domain"></param>
        private async Task GeneratorRepositoryImplCodeAsync(DomainModel domain)
        {
            if (domain.HasAttribute<NotRepositoryAttribute>()) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"/*");
            codeContent.AppendLine($" * Generator Code From MateralMergeBlock=>{nameof(GeneratorRepositoryImplCodeAsync)}");
            codeContent.AppendLine($" */");
            codeContent.AppendLine($"namespace {_projectName}.{_moduleName}.Repository.Repositories");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    /// <summary>");
            codeContent.AppendLine($"    /// {domain.Annotation}仓储");
            codeContent.AppendLine($"    /// </summary>");
            if (domain.HasAttribute<CacheAttribute>())
            {
                codeContent.AppendLine($"    public partial class {domain.Name}RepositoryImpl({_moduleName}DBContext dbContext, ICacheHelper cacheHelper) : {_moduleName}CacheRepositoryImpl<{domain.Name}>(dbContext, cacheHelper), I{domain.Name}Repository, IScopedDependency<I{domain.Name}Repository>");
                codeContent.AppendLine($"    {{");
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 获得所有缓存名称");
                codeContent.AppendLine($"        /// </summary>");
                codeContent.AppendLine($"        protected override string GetAllCacheName() => \"All{domain.Name}\";");
            }
            else
            {
                codeContent.AppendLine($"    public partial class {domain.Name}RepositoryImpl({_moduleName}DBContext dbContext) : {_moduleName}RepositoryImpl<{domain.Name}>(dbContext), I{domain.Name}Repository, IScopedDependency<I{domain.Name}Repository>");
                codeContent.AppendLine($"    {{");
            }
            if (domain.IsIndexDomain && !domain.HasAttribute<EmptyIndexAttribute>())
            {
                PropertyModel? indexGroupPropertyModel = domain.GetIndexGroupProperty();
                codeContent.AppendLine($"        /// <summary>");
                codeContent.AppendLine($"        /// 获取最大位序");
                codeContent.AppendLine($"        /// </summary>");
                if (indexGroupPropertyModel is null)
                {
                    codeContent.AppendLine($"        /// <returns></returns>");
                    codeContent.AppendLine($"        public async Task<int> GetMaxIndexAsync()");
                    codeContent.AppendLine($"        {{");
                    codeContent.AppendLine($"            if (!await DBSet.AnyAsync()) return -1;");
                    codeContent.AppendLine($"            int result = await DBSet.MaxAsync(m => m.Index);");
                }
                else
                {
                    codeContent.AppendLine($"        /// <param name=\"{indexGroupPropertyModel.Name.FirstLower()}\"></param>");
                    codeContent.AppendLine($"        /// <returns></returns>");
                    codeContent.AppendLine($"        public async Task<int> GetMaxIndexAsync({indexGroupPropertyModel.PredefinedType} {indexGroupPropertyModel.Name.FirstLower()})");
                    codeContent.AppendLine($"        {{");
                    codeContent.AppendLine($"            if (!await DBSet.AnyAsync(m => m.{indexGroupPropertyModel.Name} == {indexGroupPropertyModel.Name.FirstLower()})) return -1;");
                    codeContent.AppendLine($"            int result = await DBSet.Where(m => m.{indexGroupPropertyModel.Name} == {indexGroupPropertyModel.Name.FirstLower()}).MaxAsync(m => m.Index);");
                }
                codeContent.AppendLine($"            return result;");
                codeContent.AppendLine($"        }}");
            }
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            await codeContent.SaveAsAsync(Context, _moduleRepository, "Repositories", $"{domain.Name}RepositoryImpl.cs");
        }
    }
}
