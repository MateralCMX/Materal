using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.EFRepository.ModelConfig
{
    internal sealed class RoleAPIAuthorityConfig : IEntityTypeConfiguration<RoleAPIAuthority>
    {
        public void Configure(EntityTypeBuilder<RoleAPIAuthority> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.RoleID)
                .IsRequired();
            builder.Property(e => e.APIAuthorityID)
                .IsRequired();

            builder.HasOne(d => d.Role)
                .WithMany(p => p.RoleAPIAuthorities)
                .HasForeignKey(d => d.RoleID)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(d => d.APIAuthority)
                .WithMany(p => p.RoleAPIAuthorities)
                .HasForeignKey(d => d.APIAuthorityID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
