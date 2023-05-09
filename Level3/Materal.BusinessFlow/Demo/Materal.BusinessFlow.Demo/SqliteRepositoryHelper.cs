using Materal.BusinessFlow.SqliteRepository;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BusinessFlow.Demo
{
    internal class SqliteRepositoryHelper : IRepositoryHelper
    {

        public void AddRepository(IServiceCollection services)
        {
            SqliteConfigModel dbConfig = new()
            {
                Source = "./Oscillator.db"
            };
            BusinessFlowDBOption oscillatorDB = new(dbConfig);
            services.AddBusinessFlowSqliteRepository(oscillatorDB);
        }

        public void Init(IServiceProvider services)
        {
            using IServiceScope scope = services.CreateScope();
            IMigrateHelper migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<BusinessFlowDBOption>>();
            migrateHelper.Migrate();
        }
    }
}
