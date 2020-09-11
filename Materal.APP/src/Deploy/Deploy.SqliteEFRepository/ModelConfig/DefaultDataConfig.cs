using Deploy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deploy.SqliteEFRepository.ModelConfig
{
    internal sealed class DefaultDataConfig : IEntityTypeConfiguration<DefaultData>
    {
        public void Configure(EntityTypeBuilder<DefaultData> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Key)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Data)
                .IsRequired();
            builder.Property(e => e.ApplicationType)
                .IsRequired();
        }
    }
}
