using Materal.Oscillator.DRSqliteEFRepository;
using Materal.Oscillator.SqliteEFRepository;
using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using Materal.TTA.EFRepository;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDemo
{
    internal class SqliteEFRepositoryHelper : IRepositoryHelper
    {
        public void AddDRRepository(IServiceCollection services)
        {
            SqliteConfigModel drDBConfig = new()
            {
                Source = "./DROscillator.db"
            };
            services.AddOscillatorDRSqliteEFRepository(drDBConfig);
        }

        public void AddRepository(IServiceCollection services)
        {
            SqliteConfigModel dbConfig = new()
            {
                Source = "./Oscillator.db"
            };
            services.AddOscillatorSqliteEFRepository(dbConfig);
        }

        public void Init(IServiceProvider services)
        {
            using IServiceScope scope = services.CreateScope();            
            IMigrateHelper migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<OscillatorDBContext>>();
            migrateHelper.Migrate();
            IMigrateHelper drMigrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<OscillatorDRDBContext>>();
            drMigrateHelper.Migrate();
        }
    }
}
