/*
 * Generator Code From MateralMergeBlock=>GeneratorEntityConfigCodeAsync
 */
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RC.Deploy.Repository.EntityConfigs
{
    /// <summary>
    /// 应用程序信息配置基类
    /// </summary>
    public class ApplicationInfoConfigBase : BaseEntityConfig<ApplicationInfo>
    {
        /// <summary>
        /// 配置
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
                .HasComment("运行参数");
        }
    }
    /// <summary>
    /// 应用程序信息配置类
    /// </summary>
    public partial class ApplicationInfoConfig : ApplicationInfoConfigBase { }
}
