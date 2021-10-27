using Deploy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deploy.SqliteEFRepository.ModelConfig
{
    internal sealed class ApplicationInfoConfig : IEntityTypeConfiguration<ApplicationInfo>
    {
        public void Configure(EntityTypeBuilder<ApplicationInfo> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Path)
                .IsRequired();
            builder.Property(e => e.MainModule)
                .IsRequired();
            builder.Property(e => e.ApplicationType)
                .IsRequired();
            builder.Property(e => e.RunParams);
        }
    }
}
