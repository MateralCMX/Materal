using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RC.Deploy.EFRepository.EntityConfigs
{
    /// <summary>
    /// 默认数据配置基类
    /// </summary>
    public class DefaultDataConfigBase : BaseEntityConfig<DefaultData>
    {
        /// <summary>
        /// 配置
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
    /// 默认数据配置类
    /// </summary>
    public partial class DefaultDataConfig : DefaultDataConfigBase { }
}
