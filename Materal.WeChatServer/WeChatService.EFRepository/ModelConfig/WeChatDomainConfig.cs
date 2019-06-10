using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeChatService.Domain;
namespace WeChatService.EFRepository.ModelConfig
{
    /// <summary>
    /// 微信域名模型配置
    /// </summary>
    internal sealed class WeChatDomainConfig : IEntityTypeConfiguration<WeChatDomain>
    {
        public void Configure(EntityTypeBuilder<WeChatDomain> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.Index)
                .IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Url)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
