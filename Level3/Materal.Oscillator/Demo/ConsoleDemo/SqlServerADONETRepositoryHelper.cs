using Materal.Oscillator.DRSqlServerADONETRepository;
using Materal.Oscillator.SqlServerADONETRepository;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDemo
{
    internal class SqlServerADONETRepositoryHelper : IRepositoryHelper
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
            OscillatorDRDBOption dBOption = new(drDBConfig);
            services.AddOscillatorDRSqlServerADONETRepository(dBOption);
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
            OscillatorDBOption dBOption = new(dbConfig);
            services.AddOscillatorSqlServerADONETRepository(dBOption);
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
