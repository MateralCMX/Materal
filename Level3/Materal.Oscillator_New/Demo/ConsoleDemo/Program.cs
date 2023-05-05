using Materal.Abstractions;
using Materal.Oscillator;
using Materal.Oscillator.LocalDR;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Answers;
using Materal.Oscillator.SqliteRepository;
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
            SqliteConfigModel dbConfig = new()
            {
                Source = "D:\\Project\\Materal\\Materal\\Level3\\Materal.Oscillator_New\\Test\\Materal.Oscillator.Test\\bin\\Debug\\net6.0\\Oscillator.db"
            };
            serviceCollection.AddOscillatorSqliteRepository(dbConfig);
            SqliteConfigModel drDBConfig = new()
            {
                Source = "./DROscillator.db"
            };
            serviceCollection.AddOscillatorLocalDR(drDBConfig);
            serviceCollection.AddSingleton<IOscillatorListener, OscillatorListenerImpl>();
            serviceCollection.AddSingleton<IRetryAnswerListener, RetryAnswerListenerImpl>();
            MateralServices.Services = serviceCollection.BuildServiceProvider();
            _services = MateralServices.Services;
            IMigrateHelper<OscillatorDBContext> migrateHelper = _services.GetRequiredService<IMigrateHelper<OscillatorDBContext>>();
            migrateHelper.Migrate();
            IMigrateHelper<OscillatorLocalDRDBContext> drMigrateHelper = _services.GetRequiredService<IMigrateHelper<OscillatorLocalDRDBContext>>();
            drMigrateHelper.Migrate();
            _host = _services.GetRequiredService<IOscillatorHost>();
        }
        public static async Task Main()
        {
            await _host.StartAsync();
            //(List<ScheduleDTO> dataList, _) = await _host.GetScheduleListAsync(new QueryScheduleModel { PageIndex = 1, PageSize = 1 });
            //if (dataList.Count <= 0) return;
            //ScheduleDTO dataInfo = dataList.First();
            //await _host.StartAsync(dataInfo.ID);
            //await _host.RunNowAsync(dataInfo.ID);
            Console.ReadKey();
        }
    }
}