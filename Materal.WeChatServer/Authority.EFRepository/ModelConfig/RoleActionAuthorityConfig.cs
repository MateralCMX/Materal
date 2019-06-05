using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.EFRepository.ModelConfig
{
    internal sealed class RoleActionAuthorityConfig : IEntityTypeConfiguration<RoleActionAuthority>
    {
        public void Configure(EntityTypeBuilder<RoleActionAuthority> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.RoleID)
                .IsRequired();
            builder.Property(e => e.ActionAuthorityID)
                .IsRequired();

            builder.HasOne(d => d.Role)
                .WithMany(p => p.RoleActionAuthorities)
                .HasForeignKey(d => d.RoleID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(d => d.ActionAuthority)
                .WithMany(p => p.RoleActionAuthorities)
                .HasForeignKey(d => d.ActionAuthorityID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
