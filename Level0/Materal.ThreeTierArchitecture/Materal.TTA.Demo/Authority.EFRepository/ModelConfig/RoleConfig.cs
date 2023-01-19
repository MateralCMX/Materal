using Authority.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authority.EFRepository.ModelConfig
{
    internal sealed class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Name)
                .HasMaxLength(100);
            builder.Property(e => e.ParentID);
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.IsDelete)
                .IsRequired();

            builder.HasQueryFilter(q => q.IsDelete == false);

            builder.HasOne(m => m.Parent)
                .WithMany(m => m.Child)
                .HasForeignKey(m => m.ParentID);
        }
    }
}
