using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.EFRepository.ModelConfig
{
    internal sealed class WebMenuAuthorityConfig : IEntityTypeConfiguration<WebMenuAuthority>
    {
        public void Configure(EntityTypeBuilder<WebMenuAuthority> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Code)
                .HasMaxLength(100);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Style);
            builder.Property(e => e.Index)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(e => e.Remark)
                .IsRequired();
            builder.Property(e => e.ParentID);

            builder.HasOne(m => m.Parent)
                .WithMany(m => m.Child)
                .HasForeignKey(m => m.ParentID);
        }
    }
}
