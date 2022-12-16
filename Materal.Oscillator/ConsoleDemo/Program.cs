using Materal.Common;
using Materal.DateTimeHelper;
using Materal.Oscillator;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Common;
using Materal.Oscillator.Abstractions.DataTransmitModel;
using Materal.Oscillator.Abstractions.Models.Plan;
using Materal.Oscillator.Abstractions.Models.Work;
using Materal.Oscillator.Abstractions.Services.Trigger;
using Materal.Oscillator.Answers;
using Materal.Oscillator.LocalDR;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.Works;
using Materal.TTA.EFRepository;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ConsoleDemo
{
    public class Program
    {
        public static string _targetDB = "Sqlite";
        public static async Task Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOscillatorService();//注入调度器服务
            serviceCollection.AddAutoMapper(Assembly.Load("Materal.Oscillator"));//注入AutoMapper
            #region SqlServer
            //serviceCollection.AddOscillatorSqlServerRepositoriesService(new SqlServerConfigModel
            //{
            //    Address = "175.27.194.19",
            //    Port = "1433",
            //    Name = "OscillatorTestDB",
            //    UserID = "sa",
            //    Password = "XMJry@456",
            //    TrustServerCertificate = true
            //});
            #endregion
            #region Sqlite
            serviceCollection.AddOscillatorSqliteRepositoriesService(new SqliteConfigModel
            {
                Source = "Oscillator.db"
            });
            #endregion
            serviceCollection.AddSingleton<IRetryAnswerListener, RetryAnswerListenerImpl>();
            serviceCollection.AddSingleton<IOscillatorListener, OscillatorListenerImpl>();
            serviceCollection.AddOscillatorLocalDRService(new SqliteConfigModel
            {
                Source = "OscillatorDR.db"
            });
            MateralServices.Services = serviceCollection.BuildServiceProvider();
            #region SqlServer
            //var sqlServerMigrateHelper = MateralServices.Services.GetService<MigrateHelper<OscillatorSqlServerDBContext>>() ?? throw new OscillatorException("获取服务失败");
            //await sqlServerMigrateHelper.MigrateAsync();
            #endregion
            #region Sqlite
            var sqliteMigrateHelper = MateralServices.Services.GetService<MigrateHelper<OscillatorSqliteDBContext>>() ?? throw new OscillatorException("获取服务失败");
            await sqliteMigrateHelper.MigrateAsync();
            #endregion
            var drMigrateHelper = MateralServices.Services.GetService<MigrateHelper<OscillatorLocalDRDBContext>>() ?? throw new OscillatorException("获取服务失败");
            await drMigrateHelper.MigrateAsync();
            IOscillatorManager oscillatorManager = MateralServices.Services.GetService<IOscillatorManager>() ?? throw new OscillatorException("获取管理器失败");
            #region 测试调度器
            //IOscillatorBuild build = oscillatorManager.CreateOscillatorBuild("测试调度器");
            //await build.AddPlan(new PlanModel
            //{
            //    Name = "测试计划",
            //    PlanTriggerData = new RepeatPlanTrigger
            //    {
            //        DateTrigger = new DateDayTrigger
            //        {
            //            Interval = 1,
            //            StartDate = DateTime.Now.ToDate().AddDay(-1)
            //        },
            //        EveryDayTrigger = new EveryDayRepeatTrigger
            //        {
            //            Interval = 1,
            //            IntervalType = EveryDayIntervalType.Second,
            //            StartTime = new Time(0, 0, 0)
            //        }
            //    }
            //}).AddWork(new WorkModel
            //{
            //    Name = "测试任务",
            //    WorkData = new ConsoleWork("喵喵喵喵喵")
            //}).BuildAsync();
            //List<ScheduleDTO> allSchedules = await oscillatorManager.GetAllScheduleListAsync();
            //if(allSchedules.Any())
            //{
            //    List<PlanDTO> allPlans = await oscillatorManager.GetAllPlanListAsync(allSchedules[0].ID);
            //    if(allPlans.Any())
            //    {
            //        await oscillatorManager.EditPlanAsync(new EditPlanModel
            //        {
            //            ID = allPlans[0].ID,
            //            Description = allPlans[0].Description,
            //            Enable = true,
            //            Name = allPlans[0].Name,
            //            PlanTriggerData = new OneTimePlanTrigger(DateTime.Now.AddSeconds(5)),
            //            ScheduleID = allPlans[0].ScheduleID
            //        });
            //    }
            //}
            //else
            //{
            //    IOscillatorBuild build = oscillatorManager.CreateOscillatorBuild("测试调度器");
            //    build.AddPlan(new PlanModel
            //    {
            //        Name = "测试计划",
            //        PlanTriggerData = new OneTimePlanTrigger(DateTime.Now.AddSeconds(5))
            //    }).AddWork(new WorkModel
            //    {
            //        Name = "测试任务",
            //        WorkData = new ConsoleWork("喵喵喵喵喵")
            //    })
            //    .AddSchedule("测试调度器2")
            //    .AddPlan(new PlanModel
            //    {
            //        Name = "测试计划2",
            //        PlanTriggerData = new RepeatPlanTrigger
            //        {
            //            DateTrigger = new DateDayTrigger
            //            {
            //                Interval = 1,
            //                StartDate = DateTime.Now.ToDate().AddDay(-1)
            //            },
            //            EveryDayTrigger = new EveryDayRepeatTrigger
            //            {
            //                Interval= 1,
            //                IntervalType = EveryDayIntervalType.Minute,
            //                StartTime = new Time(0,0,0)
            //            }
            //        }
            //    }).AddWork(new WorkModel
            //    {
            //        Name = "测试任务2",
            //        WorkData = new ConsoleWork("汪汪汪汪汪")
            //    });
            //    await build.BuildAsync();
            //}
            #endregion
            #region 测试启动
            await oscillatorManager.StartAllAsync();
            await oscillatorManager.RunNowAsync(Guid.Parse("E7E0001F-0618-44EC-9CF6-7532510DDD5C"));
            #endregion
            Console.ReadLine();
        }
    }
}