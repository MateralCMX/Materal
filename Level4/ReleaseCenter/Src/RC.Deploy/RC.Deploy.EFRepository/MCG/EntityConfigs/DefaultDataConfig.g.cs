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
            builder.Property(e => e.ApplicationType)
                .IsRequired();
            builder.Property(e => e.Key)
                .IsRequired();
            builder.Property(e => e.Data)
                .IsRequired();
        }
    }
    /// <summary>
    /// 默认数据实体配置类
    /// </summary>
    public partial class DefaultDataConfig : DefaultDataConfigBase
    {
    }
}
