using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.EFRepository.ModelConfig
{
    internal sealed class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Account)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Sex)
                .IsRequired()
                .HasColumnType("tinyint");
            builder.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);
            builder.Property(e => e.Email)
                .IsRequired();
            builder.Property(e => e.IsDelete)
                .IsRequired();

            builder.HasQueryFilter(q => q.IsDelete == false);
        }
    }
}
