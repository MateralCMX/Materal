using Materal.ConfigCenter.ProtalServer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.ConfigCenter.ProtalServer.SqliteEFRepository.ModelConfig
{
    internal sealed class NamespaceConfig : IEntityTypeConfiguration<Namespace>
    {
        public void Configure(EntityTypeBuilder<Namespace> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Description)
                .IsRequired();
        }
    }
}
