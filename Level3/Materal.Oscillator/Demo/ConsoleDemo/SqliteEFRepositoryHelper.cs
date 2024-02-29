using Materal.Oscillator.DRSqliteEFRepository;
using Materal.Oscillator.DRSqliteEFRepository.Extensions;
using Materal.Oscillator.SqliteEFRepository;
using Materal.Oscillator.SqliteEFRepository.Extensions;
using Materal.TTA.SqliteEFRepository;

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
