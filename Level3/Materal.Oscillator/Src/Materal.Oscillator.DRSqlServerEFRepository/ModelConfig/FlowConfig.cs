using Materal.Oscillator.Abstractions.DR.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.DRSqlServerEFRepository.ModelConfig
{
    /// <summary>
    /// 流程配置
    /// </summary>
    public sealed class FlowConfig : BaseEntityConfig<Flow>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Flow> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.JobKey)
                .IsRequired();
            builder.Property(m => m.ScheduleData)
                .IsRequired();
            builder.Property(m => m.ScheduleID)
                .IsRequired();
            builder.Property(m => m.WorkID)
                .IsRequired(false);
            builder.Property(m => m.WorkResults)
                .IsRequired(false);
            builder.Property(m => m.AuthenticationCode)
                .IsRequired();
        }
    }
}
