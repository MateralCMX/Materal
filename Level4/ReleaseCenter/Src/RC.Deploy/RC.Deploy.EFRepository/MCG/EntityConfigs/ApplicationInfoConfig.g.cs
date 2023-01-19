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
            builder.Property(e => e.Name)
                .IsRequired();
            builder.Property(e => e.RootPath)
                .IsRequired();
            builder.Property(e => e.MainModule)
                .IsRequired();
            builder.Property(e => e.ApplicationType)
                .IsRequired();
            builder.Property(e => e.IsIncrementalUpdating)
                .IsRequired();
            builder.Property(e => e.RunParams)
                .IsRequired(false);
        }
    }
    /// <summary>
    /// 应用程序信息实体配置类
    /// </summary>
    public partial class ApplicationInfoConfig : ApplicationInfoConfigBase
    {
    }
}
