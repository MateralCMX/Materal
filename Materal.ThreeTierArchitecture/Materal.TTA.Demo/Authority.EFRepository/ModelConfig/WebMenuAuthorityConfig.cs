using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Authority.Domain;
namespace Authority.EFRepository.ModelConfig
{
    /// <summary>
    /// 网页菜单权限模型配置
    /// </summary>
    internal sealed class WebMenuAuthorityConfig : IEntityTypeConfiguration<WebMenuAuthority>
    {
        public void Configure(EntityTypeBuilder<WebMenuAuthority> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
        }
    }
}
