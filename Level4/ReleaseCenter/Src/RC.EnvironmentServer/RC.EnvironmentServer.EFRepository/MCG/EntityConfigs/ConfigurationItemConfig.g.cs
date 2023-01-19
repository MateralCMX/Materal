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
            builder.Property(e => e.ProjectID)
                .IsRequired();
            builder.Property(e => e.ProjectName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.NamespaceID)
                .IsRequired();
            builder.Property(e => e.NamespaceName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Key)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Value)
                .IsRequired();
            builder.Property(e => e.Description)
                .IsRequired()
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
