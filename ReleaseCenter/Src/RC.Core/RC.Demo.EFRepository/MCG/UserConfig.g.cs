using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Materal.BaseCore.EFRepository;
using RC.Demo.Domain;

namespace RC.Demo.EFRepository.EntityConfigs
{
    /// <summary>
    /// 用户实体配置基类
    /// </summary>
    public abstract class UserConfigBase : BaseEntityConfig<User>
    {
        /// <summary>
        /// 配置实体
        /// </summary>
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder = BaseConfigure(builder);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Sex)
                .IsRequired();
            builder.Property(e => e.Account)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(32);
        }
    }
    /// <summary>
    /// 用户实体配置类
    /// </summary>
    public partial class UserConfig : UserConfigBase
    {
    }
}
