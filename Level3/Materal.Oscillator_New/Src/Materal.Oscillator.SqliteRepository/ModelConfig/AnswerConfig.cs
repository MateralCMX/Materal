using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.SqliteRepositoryImpl.ModelConfig
{
    /// <summary>
    /// 响应配置
    /// </summary>
    public sealed class AnswerConfig : BaseEntityConfig<Answer>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
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
            builder.Property(m => m.Index)
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
