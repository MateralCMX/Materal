using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Materal.BaseCore.EFRepository;
using RC.Deploy.Domain;

namespace RC.Deploy.EFRepository.EntityConfigs
{
    /// <summary>
    /// 默认数据实体配置基类
    /// </summary>
    public abstract class DefaultDataConfigBase : BaseEntityConfig<DefaultData>
    {
        /// <summary>
        /// 配置实体
        /// </summary>
        public override void Configure(EntityTypeBuilder<DefaultData> builder)
        {
            builder = BaseConfigure(builder);
            builder.ToTable(m => m.HasComment("默认数据"));
            builder.Property(e => e.ApplicationType)
                .IsRequired()
                .HasComment("应用程序类型");
            builder.Property(e => e.Key)
                .IsRequired()
                .HasComment("键");
            builder.Property(e => e.Data)
                .IsRequired()
                .HasComment("数据");
        }
    }
    /// <summary>
    /// 默认数据实体配置类
    /// </summary>
    public partial class DefaultDataConfig : DefaultDataConfigBase
    {
    }
}
