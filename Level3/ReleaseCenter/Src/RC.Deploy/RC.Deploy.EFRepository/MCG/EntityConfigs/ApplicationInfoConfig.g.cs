using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Materal.BaseCore.EFRepository;
using RC.Deploy.Domain;

namespace RC.Deploy.EFRepository.EntityConfigs
{
    /// <summary>
    /// 应用程序信息实体配置基类
    /// </summary>
    public abstract class ApplicationInfoConfigBase : BaseEntityConfig<ApplicationInfo>
    {
        /// <summary>
        /// 配置实体
        /// </summary>
        public override void Configure(EntityTypeBuilder<ApplicationInfo> builder)
        {
            builder = BaseConfigure(builder);
            builder.ToTable(m => m.HasComment("应用程序信息"));
            builder.Property(e => e.Name)
                .IsRequired()
                .HasComment("名称");
            builder.Property(e => e.RootPath)
                .IsRequired()
                .HasComment("根路径");
            builder.Property(e => e.MainModule)
                .IsRequired()
                .HasComment("主模块");
            builder.Property(e => e.ApplicationType)
                .IsRequired()
                .HasComment("应用程序类型");
            builder.Property(e => e.IsIncrementalUpdating)
                .IsRequired()
                .HasComment("增量更新");
            builder.Property(e => e.RunParams)
                .IsRequired(false)
                .HasComment("运行参数");
        }
    }
    /// <summary>
    /// 应用程序信息实体配置类
    /// </summary>
    public partial class ApplicationInfoConfig : ApplicationInfoConfigBase
    {
    }
}
