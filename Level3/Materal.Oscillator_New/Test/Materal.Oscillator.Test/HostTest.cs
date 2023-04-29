using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.SqliteRepository;
using Materal.Oscillator.Works;
using Materal.TTA.EFRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.Test.RepositoryTest.SqliteEF
{
    [TestClass]
    public class HostTest : BaseTest
    {
        public override void AddServices(IServiceCollection services)
        {
        }
        /// <summary>
        /// 初始化主机测试
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task InitHostTest()
        {
            IOscillatorHost host = GetServiceTest<IOscillatorHost>();
            Guid workID;
            Guid scheduleID;
            #region 任务
            #region 添加
            {
                AddWorkModel model = new()
                {
                    Name = "TestWork",
                    Description = "测试任务",
                    WorkData = new ConsoleWork() { Message = "喵喵喵" }
                };
                workID = await host.AddWorkAsync(model);
                if (workID == Guid.Empty) Assert.Fail("添加任务失败");
            }
            #endregion
            #region 修改
            {
                EditWorkModel model = new()
                {
                    ID = workID,
                    Name = "TestWork",
                    Description = "测试任务",
                    WorkData = new ConsoleWork() { Message = "汪汪汪" }
                };
                await host.EditWorkAsync(model);
            }
            #endregion
            #region 查询
            {
                (List<WorkDTO> data, _) = await host.GetWorkListAsync(new QueryWorkModel { Name = "TestWork", PageIndex = 1, PageSize = 10 });
                if (data.Count <= 0) Assert.Fail("查询任务失败");
                WorkDTO work = await host.GetWorkInfoAsync(data.First().ID);
                if(work == null) Assert.Fail("查询任务失败");
            }
            #endregion
            #endregion
            #region 调度器
            #region 添加
            {
                AddScheduleModel model = new()
                {
                    Name = "TestWork",
                    Description = "测试任务",
                    Plans = new()
                    {
                        new()
                        {
                            Name = "执行一次",
                            Description = "执行一次",
                            PlanTriggerData = new OneTimePlanTrigger
                            {
                                StartTime = DateTime.Now.AddSeconds(5)
                            }
                        }
                    },
                    Wokrs = new()
                    {
                        new()
                        {
                            WorkID = workID,
                            FailEvent = "Fial",
                            SuccessEvent = "OK",
                        }
                    }
                };
                scheduleID = await host.AddScheduleAsync(model);
                if (scheduleID == Guid.Empty) Assert.Fail("添加调度器失败");
            }
            #endregion
            #endregion
        }
        /// <summary>
        /// 初始化仓储测试
        /// </summary>
        [TestMethod]
        public async Task InitRepositoryTest()
        {
            IMigrateHelper<OscillatorDBContext> migrateHelper = GetServiceTest<IMigrateHelper<OscillatorDBContext>>();
            await migrateHelper.MigrateAsync();
            GetServiceTest<IOscillatorUnitOfWork>();
            GetServiceTest<IAnswerRepository>();
            GetServiceTest<IPlanRepository>();
            GetServiceTest<IScheduleRepository>();
            GetServiceTest<IScheduleWorkRepository>();
            GetServiceTest<IWorkEventRepository>();
            GetServiceTest<IWorkRepository>();
        }
    }
}