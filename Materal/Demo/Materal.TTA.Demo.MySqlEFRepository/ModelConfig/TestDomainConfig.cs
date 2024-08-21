using Materal.TTA.Demo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.TTA.Demo.MySqlEFRepository.ModelConfig
{
    public class TestDomainConfig : IEntityTypeConfiguration<TestDomain>
    {
        public void Configure(EntityTypeBuilder<TestDomain> builder)
        {
            builder.Property(m => m.ID)
                .IsRequired();
            builder.Property(m => m.StringType)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.IntType)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.ByteType)
                .IsRequired();
            builder.Property(m => m.DecimalType)
                .IsRequired();
            builder.Property(m => m.EnumType)
                .IsRequired();
            builder.Property(m => m.DateTimeType)
                .IsRequired();
        }
    }
}
