using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Materal.BaseCore.EFRepository;
using MBC.Demo.Domain;

namespace MBC.Demo.EFRepository.EntityConfigs
{
    /// <summary>
    /// 我的树实体配置基类
    /// </summary>
    public abstract class MyTreeConfigBase : BaseEntityConfig<MyTree>
    {
        /// <summary>
        /// 配置实体
        /// </summary>
        public override void Configure(EntityTypeBuilder<MyTree> builder)
        {
            builder = BaseConfigure(builder);
            builder.ToTable(m => m.HasComment("我的树"));
            builder.Property(e => e.Name)
                .IsRequired()
                .HasComment("名称")
                .HasMaxLength(20);
            builder.Property(e => e.ParentID)
                .IsRequired(false)
                .HasComment("父级唯一标识");
            builder.Property(e => e.Index)
                .IsRequired()
                .HasComment("位序");
        }
    }
    /// <summary>
    /// 我的树实体配置类
    /// </summary>
    public partial class MyTreeConfig : MyTreeConfigBase
    {
    }
}
