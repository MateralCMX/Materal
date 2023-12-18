using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMB.Demo.Abstractions.Domain;

namespace MMB.Demo.EFRepository.EntityConfigs
{
    /// <summary>
    /// 用户实体配置基类
    /// </summary>
    public class UserConfigBase : BaseEntityConfig<User>
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
            builder.Property(e => e.Sex)
                .IsRequired()
                .HasComment("性别");
            builder.Property(e => e.Account)
                .IsRequired()
                .HasComment("账号")
                .HasMaxLength(50);
            builder.Property(e => e.Password)
                .IsRequired()
                .HasComment("密码")
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
