﻿using Materal.Abstractions;
using Materal.TTA.Common.Model;
using Materal.TTA.Demo.SqliteEFRepository;
using Materal.TTA.EFRepository;
using Materal.TTA.SqliteEFRepository;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.TTA.Demo
{
    public static class SqliteEFHelper
    {
        public static IServiceCollection AddSqliteEFTTA(this IServiceCollection services)
        {
            SqliteConfigModel dbConfig = new()
            {
                Source = "TTATestDB.db"
            };
            services.AddTTASqliteEFRepository<TTADemoDBContext>(dbConfig.ConnectionString, Assembly.Load("Materal.TTA.Demo.SqliteEFRepository"));
            return services;
        }
        public static async Task MigrateAsync(IServiceProvider serviceProvider)
        {
            MigrateHelper<TTADemoDBContext> migrateHelper = serviceProvider.GetService<MigrateHelper<TTADemoDBContext>>() ?? throw new MateralException("获取实例失败");
            await migrateHelper.MigrateAsync();
        }
    }
}
