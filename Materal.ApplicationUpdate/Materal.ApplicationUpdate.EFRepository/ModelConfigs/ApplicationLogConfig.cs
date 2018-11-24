using Materal.ApplicationUpdate.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Materal.ApplicationUpdate.EFRepository.ModelConfigs
{
    internal sealed class ApplicationLogConfig : IEntityTypeConfiguration<ApplicationLog>
    {
        public void Configure(EntityTypeBuilder<ApplicationLog> builder)
        {
            //主键
            builder.HasKey(m => m.ID);
            //属性
            builder.Property(m => m.Application);
            builder.Property(m => m.CreateTime)
                .IsRequired();
            builder.Property(m => m.Callsite);
            builder.Property(m => m.Exception);
            builder.Property(m => m.Level);
            builder.Property(m => m.Logger);
            builder.Property(m => m.Message);
        }
    }
}
