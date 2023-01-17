using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Materal.BaseCore.EFRepository;
using RC.ServerCenter.Domain;

namespace RC.ServerCenter.EFRepository.EntityConfigs
{
    /// <summary>
    /// 命名空间实体配置基类
    /// </summary>
    public abstract class NamespaceConfigBase : BaseEntityConfig<Namespace>
    {
        /// <summary>
        /// 配置实体
        /// </summary>
        public override void Configure(EntityTypeBuilder<Namespace> builder)
        {
            builder = BaseConfigure(builder);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(e => e.ProjectID)
                .IsRequired();
        }
    }
    /// <summary>
    /// 命名空间实体配置类
    /// </summary>
    public partial class NamespaceConfig : NamespaceConfigBase
    {
    }
}
