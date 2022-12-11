using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.SqliteRepositoryImpl.ModelConfig
{
    public sealed class PlanConfig : BaseEntityConfig<Plan>
    {
        public override void Configure(EntityTypeBuilder<Plan> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.ScheduleID)
                .IsRequired();
            builder.Property(m => m.Enable)
                .IsRequired();
            builder.Property(m => m.Description)
                .IsRequired(false)
                .HasMaxLength(400);
            builder.Property(m => m.PlanTriggerType)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.PlanTriggerData)
                .IsRequired()
                .HasMaxLength(4000);
        }
    }
}
