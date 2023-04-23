using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.SqlServerRepositoryImpl.ModelConfig
{
    public sealed class ScheduleWorkConfig : BaseEntityConfig<ScheduleWork>
    {
        public override void Configure(EntityTypeBuilder<ScheduleWork> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.ScheduleID)
                .IsRequired();
            builder.Property(m => m.WorkID)
                .IsRequired();
            builder.Property(m => m.OrderIndex)
                .IsRequired();
            builder.Property(m => m.SuccessEvent)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.FailEvent)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
