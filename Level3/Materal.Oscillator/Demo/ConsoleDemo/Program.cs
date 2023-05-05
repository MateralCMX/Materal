using Materal.Abstractions;
using Materal.Oscillator;
using Materal.Oscillator.LocalDR;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Answers;
using Materal.Oscillator.SqliteRepository;
using Materal.Oscillator.SqlServerRepository;
using Materal.TTA.Common.Model;
using Materal.TTA.EFRepository;
using Materal.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleDemo
{
    public class Program
    {
        private static readonly IServiceProvider _services;
        private static readonly IOscillatorHost _host;
        static Program()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            MateralConfig.PageStartNumber = 1;
            serviceCollection.AddMateralUtils();
            serviceCollection.AddOscillator();
            SqliteConfigModel drDBConfig = new()
            {
                Source = "./DROscillator.db"
            };
            serviceCollection.AddOscillatorLocalDR(drDBConfig);

            SqliteConfigModel dbConfig = new()
            {
                Source = "./Oscillator.db"
            };
            serviceCollection.AddOscillatorSqliteRepository(dbConfig);

            //SqlServerConfigModel dbConfig = new()
            //{
            //    Address = "82.156.11.176",
            //    Port = "1433",
            //    Name = "OscillatorTestDB",
            //    UserID = "sa",
            //    Password = "gdb@admin678",
            //    TrustServerCertificate = true
            //};
            //serviceCollection.AddOscillatorSqlServerRepository(dbConfig);

            serviceCollection.AddSingleton<IOscillatorListener, OscillatorListenerImpl>();
            serviceCollection.AddSingleton<IRetryAnswerListener, RetryAnswerListenerImpl>();
            MateralServices.Services = serviceCollection.BuildServiceProvider();
            _services = MateralServices.Services;

            IMigrateHelper<OscillatorSqliteDBContext> migrateHelper = _services.GetRequiredService<IMigrateHelper<OscillatorSqliteDBContext>>();
            migrateHelper.Migrate();
            //IMigrateHelper<OscillatorSqlServerDBContext> migrateHelper = _services.GetRequiredService<IMigrateHelper<OscillatorSqlServerDBContext>>();
            //migrateHelper.Migrate();

            IMigrateHelper<OscillatorLocalDRDBContext> drMigrateHelper = _services.GetRequiredService<IMigrateHelper<OscillatorLocalDRDBContext>>();
            drMigrateHelper.Migrate();
            _host = _services.GetRequiredService<IOscillatorHost>();
        }
        public static async Task Main()
        {
            //await _host.StartAsync();
            (List<ScheduleDTO> dataList, _) = await _host.GetScheduleListAsync(new QueryScheduleModel { PageIndex = 1, PageSize = 1 });
            if (dataList.Count <= 0) return;
            ScheduleDTO dataInfo = dataList.First();
            await _host.RunNowAsync(dataInfo.ID);
            //await _host.StartAsync(dataInfo.ID);
            Console.ReadKey();
        }
    }
}