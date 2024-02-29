using Materal.Oscillator.DRSqlServerEFRepository;
using Materal.Oscillator.DRSqlServerEFRepository.Extensions;
using Materal.Oscillator.SqlServerEFRepository;
using Materal.Oscillator.SqlServerEFRepository.Extensions;
using Materal.TTA.SqlServerEFRepository;

namespace ConsoleDemo
{
    internal class SqlServerEFRepositoryHelper : IRepositoryHelper
    {
        public void AddDRRepository(IServiceCollection services)
        {
            SqlServerConfigModel drDBConfig = new()
            {
                Address = "82.156.11.176",
                Port = "1433",
                Name = "OscillatorDRTestDB",
                UserID = "sa",
                Password = "gdb@admin678",
                TrustServerCertificate = true
            };
            services.AddOscillatorDRSqlServerEFRepository(drDBConfig);
        }

        public void AddRepository(IServiceCollection services)
        {
            SqlServerConfigModel dbConfig = new()
            {
                Address = "82.156.11.176",
                Port = "1433",
                Name = "OscillatorTestDB",
                UserID = "sa",
                Password = "gdb@admin678",
                TrustServerCertificate = true
            };
            services.AddOscillatorSqlServerEFRepository(dbConfig);
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
