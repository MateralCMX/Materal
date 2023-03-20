using Materal.BaseCore.EFRepository;
using MBC.Demo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MBC.Demo.EFRepository.EntityConfigs
{
    /// <summary>
    /// 菜单权限实体配置
    /// </summary>
    public class MenuAuthorityConfig : BaseEntityConfig<MenuAuthority>
    {
        public override void Configure(EntityTypeBuilder<MenuAuthority> builder)
        {
            BaseConfigure(builder);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasComment("名称")
                .HasMaxLength(100);
            builder.Property(e => e.Code)
                .IsRequired()
                .HasComment("代码")
                .HasMaxLength(100);
            builder.Property(e => e.Path)
                .IsRequired(false)
                .HasComment("地址")
                .HasMaxLength(100);
            builder.Property(e => e.Index)
                .IsRequired()
                .HasComment("位序");
            builder.Property(e => e.ParentID)
                .IsRequired(false)
                .HasComment("父级");
            builder.Property(e => e.SubSystemID)
                .IsRequired()
                .HasComment("所属子系统唯一标识");
        }
    }
}
