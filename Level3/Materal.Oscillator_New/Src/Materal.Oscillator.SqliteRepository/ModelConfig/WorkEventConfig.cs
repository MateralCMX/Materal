using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.SqliteRepositoryImpl.ModelConfig
{
    /// <summary>
    /// 任务事件配置
    /// </summary>
    public sealed class WorkEventConfig : BaseEntityConfig<WorkEvent>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
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
