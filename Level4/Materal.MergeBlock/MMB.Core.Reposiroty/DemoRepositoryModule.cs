using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Repository;
using Materal.TTA.Common.Model;
using Materal.TTA.SqliteEFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MMB.Demo.Repository
{
    public abstract class MMBRepositoryModule<T> : RepositoryModule<T, SqliteConfigModel>, IMergeBlockModule
        where T : DbContext
    {
        protected override void AddDBContext(IServiceCollection services, SqliteConfigModel dBConfig) => services.AddTTASqliteEFRepository<T>(dBConfig.ConnectionString, GetType().Assembly);
    }
}
