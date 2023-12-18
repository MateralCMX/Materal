using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Repository;
using Materal.TTA.Common.Model;
using Materal.TTA.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MMB.Demo.Repository
{
    /// <summary>
    /// MMB仓储模块
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MMBRepositoryModule<T> : RepositoryModule<T, SqliteConfigModel>, IMergeBlockModule
        where T : DbContext
    {
        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <param name="services"></param>
        /// <param name="dBConfig"></param>
        protected override void AddDBContext(IServiceCollection services, SqliteConfigModel dBConfig) => services.AddTTASqliteEFRepository<T>(dBConfig.ConnectionString, GetType().Assembly);
    }
}
