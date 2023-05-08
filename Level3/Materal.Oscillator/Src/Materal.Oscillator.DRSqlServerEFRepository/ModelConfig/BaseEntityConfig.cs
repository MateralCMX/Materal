using Materal.Oscillator.Abstractions.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.Oscillator.DRSqlServerEFRepository.ModelConfig
{
    /// <summary>
    /// 基础配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : BaseDomain
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
        }
    }
}
