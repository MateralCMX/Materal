using Materal.BaseCore.EFRepository;
using MBC.Demo.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MBC.Demo.SqliteEFRepository.EntityConfigs
{
    /// <summary>
    /// 用户实体配置
    /// </summary>
    public class UserEntityConfig : BaseEntityConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            BaseConfigure(builder);
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(e => e.Account)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(e => e.Password)
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
