using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.SqliteEFRepository.ModelConfig
{
    /// <summary>
    /// 任务配置
    /// </summary>
    public sealed class WorkConfig : BaseEntityConfig<Work>
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Work> builder)
        {
            base.Configure(builder);
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.Description)
                .IsRequired(false)
                .HasMaxLength(400);
            builder.Property(m => m.WorkType)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(m => m.WorkData)
                .IsRequired()
                .HasMaxLength(4000);
        }
    }
}
