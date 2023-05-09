using Materal.BusinessFlow.SqlServerRepository;
using Materal.TTA.ADONETRepository;
using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.BusinessFlow.Demo
{
    internal class SqlServerRepositoryHelper : IRepositoryHelper
    {
        public void AddRepository(IServiceCollection services)
        {
            SqlServerConfigModel dbConfig = new()
            {
                Address = "82.156.11.176",
                Port = "1433",
                Name = "BusinessFlowTestDB",
                UserID = "sa",
                Password = "gdb@admin678",
                TrustServerCertificate = true
            };
            BusinessFlowDBOption dBOption = new(dbConfig);
            services.AddBusinessFlowSqlServerRepository(dBOption);
        }

        public void Init(IServiceProvider services)
        {
            using IServiceScope scope = services.CreateScope();
            IMigrateHelper migrateHelper = scope.ServiceProvider.GetRequiredService<IMigrateHelper<BusinessFlowDBOption>>();
            migrateHelper.Migrate();
        }
    }
}
