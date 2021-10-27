using ConfigCenter.Environment.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigCenter.Environment.SqliteEFRepository.ModelConfig
{
    internal sealed class ConfigurationItemConfig : IEntityTypeConfiguration<ConfigurationItem>
    {
        public void Configure(EntityTypeBuilder<ConfigurationItem> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.ProjectID)
                .IsRequired();
            builder.Property(e => e.NamespaceID)
                .IsRequired();
            builder.Property(e => e.ProjectName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.NamespaceName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Key)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Value)
                .IsRequired(false);
            builder.Property(e => e.Description)
                .IsRequired();
        }
    }
}
