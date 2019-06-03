using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Authority.Domain;
namespace Authority.EFRepository.ModelConfig
{
    /// <summary>
    /// 功能权限模型配置
    /// </summary>
    internal sealed class ActionAuthorityConfig : IEntityTypeConfiguration<ActionAuthority>
    {
        public void Configure(EntityTypeBuilder<ActionAuthority> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
        }
    }
}
