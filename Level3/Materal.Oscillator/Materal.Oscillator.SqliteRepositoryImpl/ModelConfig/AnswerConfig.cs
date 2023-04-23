using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.SqliteRepositoryImpl.ModelConfig
{
    public sealed class AnswerConfig : BaseEntityConfig<Answer>
    {
        public override void Configure(EntityTypeBuilder<Answer> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.ScheduleID)
                .IsRequired();
            builder.Property(m => m.WorkEvent)
                .IsRequired()
                .HasMaxLength(40);
            builder.Property(m => m.Enable)
                .IsRequired();
            builder.Property(m => m.OrderIndex)
                .IsRequired();
            builder.Property(m => m.Description)
                .IsRequired(false)
                .HasMaxLength(400);
            builder.Property(m => m.AnswerType)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.AnswerData)
                .IsRequired()
                .HasMaxLength(4000);
        }
    }
}
