/*
 * Generator Code From MateralMergeBlock=>GeneratorEntityConfigCodeAsync
 */
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RC.EnvironmentServer.EFRepository.EntityConfigs
{
    /// <summary>
    /// 配置项配置基类
    /// </summary>
    public class ConfigurationItemConfigBase : BaseEntityConfig<ConfigurationItem>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public override void Configure(EntityTypeBuilder<ConfigurationItem> builder)
        {
            builder = BaseConfigure(builder);
            builder.ToTable(m => m.HasComment("配置项"));
            builder.Property(e => e.ProjectID)
                .IsRequired()
                .HasComment("项目唯一标识");
            builder.Property(e => e.ProjectName)
                .IsRequired()
                .HasComment("项目名称")
                .HasMaxLength(50);
            builder.Property(e => e.NamespaceID)
                .IsRequired()
                .HasComment("命名空间唯一标识");
            builder.Property(e => e.NamespaceName)
                .IsRequired()
                .HasComment("命名空间名称")
                .HasMaxLength(50);
            builder.Property(e => e.Key)
                .IsRequired()
                .HasComment("键")
                .HasMaxLength(50);
            builder.Property(e => e.Value)
                .IsRequired()
                .HasComment("值");
            builder.Property(e => e.Description)
                .IsRequired()
                .HasComment("描述")
                .HasMaxLength(200);
        }
    }
    /// <summary>
    /// 配置项配置类
    /// </summary>
    public partial class ConfigurationItemConfig : ConfigurationItemConfigBase { }
}
