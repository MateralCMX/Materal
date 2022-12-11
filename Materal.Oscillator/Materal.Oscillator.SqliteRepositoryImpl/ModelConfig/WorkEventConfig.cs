using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.SqliteRepositoryImpl.ModelConfig
{
    public sealed class WorkEventConfig : BaseEntityConfig<WorkEvent>
    {
        public override void Configure(EntityTypeBuilder<WorkEvent> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Value)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.ScheduleID)
                .IsRequired();
            builder.Property(m => m.Description)
                .IsRequired(false)
                .HasMaxLength(400);
        }
    }
}
