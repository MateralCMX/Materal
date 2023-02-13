using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Materal.BaseCore.EFRepository;
using RC.ServerCenter.Domain;

namespace RC.ServerCenter.EFRepository.EntityConfigs
{
    /// <summary>
    /// 项目实体配置基类
    /// </summary>
    public abstract class ProjectConfigBase : BaseEntityConfig<Project>
    {
        /// <summary>
        /// 配置实体
        /// </summary>
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            builder = BaseConfigure(builder);
            builder.ToTable(m => m.HasComment("项目"));
            builder.Property(e => e.Name)
                .IsRequired()
                .HasComment("名称")
                .HasMaxLength(50);
            builder.Property(e => e.Description)
                .IsRequired()
                .HasComment("描述")
                .HasMaxLength(200);
        }
    }
    /// <summary>
    /// 项目实体配置类
    /// </summary>
    public partial class ProjectConfig : ProjectConfigBase
    {
    }
}
