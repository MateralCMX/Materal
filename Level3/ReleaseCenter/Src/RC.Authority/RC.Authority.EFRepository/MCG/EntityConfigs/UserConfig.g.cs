using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Materal.BaseCore.EFRepository;
using RC.Authority.Domain;

namespace RC.Authority.EFRepository.EntityConfigs
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
            builder.ToTable(m => m.HasComment("用户"));
            builder.Property(e => e.Name)
                .IsRequired()
                .HasComment("姓名")
                .HasMaxLength(100);
            builder.Property(e => e.Account)
                .IsRequired()
                .HasComment("账号")
                .HasMaxLength(100);
            builder.Property(e => e.Password)
                .IsRequired()
                .HasComment("密码")
                .HasMaxLength(100);
        }
    }
    /// <summary>
    /// 用户实体配置类
    /// </summary>
    public partial class UserConfig : UserConfigBase
    {
    }
}
