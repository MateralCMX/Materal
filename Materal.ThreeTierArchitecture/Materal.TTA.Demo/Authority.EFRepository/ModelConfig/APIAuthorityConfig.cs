using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.EFRepository.ModelConfig
{
    internal sealed class APIAuthorityConfig : IEntityTypeConfiguration<APIAuthority>
    {
        public void Configure(EntityTypeBuilder<APIAuthority> builder)
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
            builder.Property(e => e.Remark)
                .IsRequired();
            builder.Property(e => e.ParentID);

            builder.HasOne(m => m.Parent)
                .WithMany(m => m.Child)
                .HasForeignKey(m => m.ParentID);
        }
    }
}
