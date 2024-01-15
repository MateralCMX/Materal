using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.SqlServerEFRepository.ModelConfig
{
    /// <summary>
    /// 调度器配置
    /// </summary>
    public sealed class ScheduleConfig : BaseEntityConfig<Schedule>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Schedule> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Territory)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Enable)
                .IsRequired();
            builder.Property(m => m.Description)
                .IsRequired(false)
                .HasMaxLength(400);
        }
    }
}
