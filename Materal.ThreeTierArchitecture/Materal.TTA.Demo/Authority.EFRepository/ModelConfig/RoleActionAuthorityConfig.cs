using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Authority.Domain;
namespace Authority.EFRepository.ModelConfig
{
    /// <summary>
    /// 角色功能权限模型配置
    /// </summary>
    internal sealed class RoleActionAuthorityConfig : IEntityTypeConfiguration<RoleActionAuthority>
    {
        public void Configure(EntityTypeBuilder<RoleActionAuthority> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
        }
    }
}
