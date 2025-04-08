using Materal.Extensions;
using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Repository.Abstractions;
using Materal.TTA.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: MergeBlockAssembly]
namespace MMB.Demo.Repository
{
    /// <summary>
    /// MMB仓储模块
    /// </summary>
    public abstract class MMBRepositoryModule<TDBContext>(string moduleName) : RepositoryModule<TDBContext, SqliteConfigModel>(moduleName)
        where TDBContext : DbContext
    {
        /// <inheritdoc/>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            if (context.Configuration is not IConfigurationBuilder configurationBuilder) return;
            Type moduleType = GetType();
            string configFilePath = moduleType.Assembly.GetDirectoryPath();
            configFilePath = Path.Combine(configFilePath, $"{moduleType.Namespace}.json");
            configurationBuilder.AddJsonFile(configFilePath, optional: true, reloadOnChange: true);
            base.OnConfigureServices(context);
        }
        /// <summary>
        /// 添加DBContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dBConfig"></param>
        protected override void AddDBContext(IServiceCollection services, SqliteConfigModel dBConfig)
        {
            services.AddTTASqliteEFRepository<TDBContext>(dBConfig.ConnectionString);
            //services.AddDbContext<TDBContext>(delegate (DbContextOptionsBuilder options)
            //{
            //    options.UseSqlite(dBConfig.ConnectionString, null).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //});
            //services.TryAddScoped<IMigrateHelper<TDBContext>, MigrateHelper<TDBContext>>();
        }
    }
}