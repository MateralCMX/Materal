using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeChatService.Domain;
namespace WeChatService.EFRepository.ModelConfig
{
    /// <summary>
    /// 应用模型配置
    /// </summary>
    internal sealed class ApplicationConfig : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(e => e.ID);
            builder.Property(e => e.CreateTime)
                .IsRequired();
            builder.Property(e => e.UpdateTime)
                .IsRequired();
            builder.Property(e => e.AppID)
                .IsRequired();
            builder.Property(e => e.AppSecret)
                .IsRequired();
            builder.Property(e => e.EncodingAESKey);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.WeChatToken);
            builder.Property(e => e.UserID)
                .IsRequired();
            builder.Property(e => e.Remark);
        }
    }
}
