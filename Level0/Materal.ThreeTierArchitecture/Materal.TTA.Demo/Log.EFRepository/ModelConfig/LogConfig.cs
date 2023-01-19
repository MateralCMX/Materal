using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Log.EFRepository.ModelConfig
{
    internal sealed class LogConfig : IEntityTypeConfiguration<Domain.Log>
    {
        public void Configure(EntityTypeBuilder<Domain.Log> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Application)
                .IsRequired();
            builder.Property(e => e.Callsite);
            builder.Property(e => e.Exception);
            builder.Property(e => e.Level)
                .IsRequired();
            builder.Property(e => e.Logger);
            builder.Property(e => e.Message)
                .IsRequired();
        }
    }
}
