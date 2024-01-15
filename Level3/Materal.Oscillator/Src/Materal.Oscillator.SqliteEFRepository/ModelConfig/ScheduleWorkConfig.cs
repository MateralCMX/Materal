using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.SqliteEFRepository.ModelConfig
{
    /// <summary>
    /// 调度器任务配置
    /// </summary>
    public sealed class ScheduleWorkConfig : BaseEntityConfig<ScheduleWork>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ScheduleWork> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.ScheduleID)
                .IsRequired();
            builder.Property(m => m.WorkID)
                .IsRequired();
            builder.Property(m => m.Index)
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
