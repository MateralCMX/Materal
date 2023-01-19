using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.EFRepository.ModelConfig
{
    internal sealed class RoleWebMenuAuthorityConfig : IEntityTypeConfiguration<RoleWebMenuAuthority>
    {
        public void Configure(EntityTypeBuilder<RoleWebMenuAuthority> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.RoleID)
                .IsRequired();
            builder.Property(e => e.WebMenuAuthorityID)
                .IsRequired();

            builder.HasOne(d => d.Role)
                .WithMany(p => p.RoleWebMenuAuthorities)
                .HasForeignKey(d => d.RoleID)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(d => d.WebMenuAuthority)
                .WithMany(p => p.RoleWebMenuAuthorities)
                .HasForeignKey(d => d.WebMenuAuthorityID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
