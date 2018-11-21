using Materal.ApplicationUpdate.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.ApplicationUpdate.EFRepository.ModelConfigs
{
    internal sealed class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //主键
            builder.HasKey(m => m.ID);
            //属性
            builder.Property(m => m.Account)
                .IsRequired();
            builder.Property(m => m.CreateTime)
                .IsRequired();
            builder.Property(m => m.Name)
                .IsRequired();
            builder.Property(m => m.Password)
                .IsRequired();
            builder.Property(m => m.Token)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(m => m.TokenExpireDate)
                .IsRequired();
            builder.Property(m => m.UpdateTime)
                .IsRequired();
        }
    }
}
