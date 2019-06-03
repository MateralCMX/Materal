using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Authority.Domain;
namespace Authority.EFRepository.ModelConfig
{
    /// <summary>
    /// 角色网页菜单权限模型配置
    /// </summary>
    internal sealed class RoleWebMenuAuthorityConfig : IEntityTypeConfiguration<RoleWebMenuAuthority>
    {
        public void Configure(EntityTypeBuilder<RoleWebMenuAuthority> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
        }
    }
}
