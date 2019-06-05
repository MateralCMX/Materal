using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.EFRepository.ModelConfig
{
    internal sealed class ActionAuthorityConfig : IEntityTypeConfiguration<ActionAuthority>
    {
        public void Configure(EntityTypeBuilder<ActionAuthority> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.ActionGroupCode)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Remark)
                .IsRequired();
        }
    }
}
