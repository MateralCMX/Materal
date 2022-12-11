using Materal.Common;
using Materal.Oscillator;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Common;
using Materal.Oscillator.Abstractions.DataTransmitModel;
using Materal.Oscillator.Abstractions.Models.Plan;
using Materal.Oscillator.Abstractions.Models.Work;
using Materal.Oscillator.Answers;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.Works;
using Materal.TTA.EFRepository;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleDemo
{
    public class Program
    {
        public static async Task Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOscillatorService();//注入调度器服务
            #region Sqlite
            serviceCollection.AddOscillatorSqliteRepositoriesService(new SqliteConfigModel
            {
                Source = "Oscillator.db"
            });
            #endregion
            serviceCollection.AddSingleton<IRetryAnswerListener, RetryAnswerListenerImpl>();
            serviceCollection.AddSingleton<IOscillatorListener, OscillatorListenerImpl>();
            //serviceCollection.AddSingleton<IOscillatorDR, OscillatorLocalDR>();
            MateralServices.Services = serviceCollection.BuildServiceProvider();
            #region Sqlite
            var migrateHelper = MateralServices.Services.GetService<MigrateHelper<OscillatorSqliteDBContext>>() ?? throw new OscillatorException("获取服务失败");
            await migrateHelper.MigrateAsync();
            #endregion
            #region 测试调度器
            IOscillatorManager oscillatorManager = MateralServices.Services.GetService<IOscillatorManager>() ?? throw new OscillatorException("获取管理器失败");
            List<ScheduleDTO> allSchedules = await oscillatorManager.GetAllScheduleListAsync();
            if(allSchedules.Any())
            {
                List<PlanDTO> allPlans = await oscillatorManager.GetAllPlanListAsync(allSchedules[0].ID);
                if(allPlans.Any())
                {
                    await oscillatorManager.EditPlanAsync(new EditPlanModel
                    {
                        ID = allPlans[0].ID,
                        Description = allPlans[0].Description,
                        Enable = true,
                        Name = allPlans[0].Name,
                        PlanTriggerData = new OneTimePlanTrigger(DateTime.Now.AddSeconds(5)),
                        ScheduleID = allPlans[0].ScheduleID
                    });
                }
            }
            else
            {
                IOscillatorBuild build = oscillatorManager.CreateOscillatorBuild("测试调度器");
                await build.AddPlan(new PlanModel
                {
                    Name = "测试计划",
                    PlanTriggerData = new OneTimePlanTrigger(DateTime.Now.AddSeconds(5))
                }).AddWork(new WorkModel
                {
                    Name = "测试任务",
                    WorkData = new ConsoleWork("喵喵喵喵喵")
                }).BuildAsync();
            }
            #endregion
            #region 测试启动
            await oscillatorManager.StartAllAsync();
            #endregion
            Console.ReadLine();
        }
    }
}