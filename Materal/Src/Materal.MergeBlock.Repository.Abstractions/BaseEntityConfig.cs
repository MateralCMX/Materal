using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.MergeBlock.Repository.Abstractions
{
    /// <summary>
    /// 基础配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEntityConfig<T> : IEntityTypeConfiguration<T> where T : BaseDomain
    {
        /// <summary>
        /// 基础配置
        /// </summary>
        /// <param name="builder"></param>
        public EntityTypeBuilder<T> BaseConfigure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            return builder;
        }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="builder"></param>
        public abstract void Configure(EntityTypeBuilder<T> builder);
    }
}
