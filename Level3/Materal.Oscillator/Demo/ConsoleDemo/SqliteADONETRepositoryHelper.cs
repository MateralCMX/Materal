using Materal.Oscillator.DRSqliteADONETRepository;
using Materal.Oscillator.SqliteADONETRepository;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDemo
{
    internal class SqliteADONETRepositoryHelper : IRepositoryHelper
    {
        public void AddDRRepository(IServiceCollection services)
        {
            SqliteConfigModel drDBConfig = new()
            {
                Source = "./DROscillator.db"
            };
            OscillatorDRDBOption oscillatorDRDB = new(drDBConfig);
            services.AddOscillatorDRSqliteADONETRepository(oscillatorDRDB);
        }

        public void AddRepository(IServiceCollection services)
        {
            SqliteConfigModel dbConfig = new()
            {
                Source = "./Oscillator.db"
            };
            OscillatorDBOption oscillatorDB = new(dbConfig);
            services.AddOscillatorSqliteADONETRepository(oscillatorDB);
        }

        public void Init(IServiceProvider services)
        {
            using IServiceScope scope = services.CreateScope();            
            IMigrateHelper migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<OscillatorDBOption>>();
            migrateHelper.Migrate();
            IMigrateHelper drMigrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<OscillatorDRDBOption>>();
            drMigrateHelper.Migrate();
        }
    }
}
