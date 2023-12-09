using Materal.MergeBlock.Abstractions;
using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using Materal.TTA.EFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.MergeBlock.Repository
{
    public abstract class RepositoryModule<T, TDBConfigType> : MergeBlockModule, IMergeBlockModule
        where T : DbContext
        where TDBConfigType : IDBConfigModel
    {
        private const string _configKey = "DBConfig";
        protected abstract void AddDBContext(IServiceCollection services, TDBConfigType dBConfig);
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            TDBConfigType dbConfig = context.Configuration.GetValueObject<TDBConfigType>(_configKey) ?? throw new MergeBlockException($"获取数据库配置[{_configKey}]失败");
            AddDBContext(context.Services, dbConfig);
            await base.OnConfigServiceAsync(context);
        }
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            using IServiceScope serviceScope = context.ServiceProvider.CreateScope();
            IMigrateHelper migrateHelper = serviceScope.ServiceProvider.GetRequiredService<IMigrateHelper<T>>();
            await migrateHelper.MigrateAsync();
            await base.OnApplicationInitAsync(context);
        }
    }
}
