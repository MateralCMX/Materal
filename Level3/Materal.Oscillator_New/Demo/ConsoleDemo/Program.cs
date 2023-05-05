using Materal.Abstractions;
using Materal.Oscillator;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.SqliteRepository;
using Materal.TTA.Common.Model;
using Materal.TTA.EFRepository;
using Materal.Utils;
using Microsoft.Extensions.DependencyInjection;

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
                Source = "Oscillator.db"
            };
            serviceCollection.AddOscillatorSqliteRepository(dbConfig);
            MateralServices.Services = serviceCollection.BuildServiceProvider();
            _services = MateralServices.Services;
            IMigrateHelper<OscillatorDBContext> migrateHelper = _services.GetRequiredService<IMigrateHelper<OscillatorDBContext>>();
            migrateHelper.Migrate();
            _host = _services.GetRequiredService<IOscillatorHost>();
        }
        public static async Task Main()
        {
            #region 启动所有任务
            {
                await _host.StartAsync();
            }
            #endregion
            #region 启动单独的任务
            {
                //(List<ScheduleDTO> dataList, _) = await _host.GetScheduleListAsync(new QueryScheduleModel { PageIndex = 1, PageSize = 1 });
                //if (dataList.Count <= 0) return;
                //ScheduleDTO dataInfo = dataList.First();
                //await _host.StartAsync(dataInfo.ID);
                //await _host.RunNowAsync(dataInfo.ID);
            }
            #endregion
            Console.ReadKey();
        }
    }
}