using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Materal.BaseCore.EFRepository;
using RC.EnvironmentServer.Domain;

namespace RC.EnvironmentServer.EFRepository.EntityConfigs
{
    /// <summary>
    /// 配置项实体配置基类
    /// </summary>
    public abstract class ConfigurationItemConfigBase : BaseEntityConfig<ConfigurationItem>
    {
        /// <summary>
        /// 配置实体
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
    /// 配置项实体配置类
    /// </summary>
    public partial class ConfigurationItemConfig : ConfigurationItemConfigBase
    {
    }
}
