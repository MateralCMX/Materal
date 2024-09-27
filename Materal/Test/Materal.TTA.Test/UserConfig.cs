using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.TTA.Test
{
    /// <summary>
    /// 用户配置基类
    /// </summary>
    public class UserConfigBase : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// 配置
        /// </summary>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey((User e) => e.ID);
            builder.Property((User e) => e.ID)
                .HasConversion(m => m.ToString(), m => Guid.Parse(m));
            builder.Property((User e) => e.CreateTime).IsRequired();
            builder.Property((User e) => e.UpdateTime).IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasComment("姓名")
                .HasMaxLength(100);
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
    /// 用户配置类
    /// </summary>
    public partial class UserConfig : UserConfigBase { }
}
