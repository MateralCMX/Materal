using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.Answers;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.Works;
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
        public async Task InitHostTestAsync()
        {
            IOscillatorHost host = GetServiceTest<IOscillatorHost>();
            Guid workID;
            Guid work2ID;
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
                model = new()
                {
                    Name = "TestWork2",
                    Description = "测试任务2",
                    WorkData = new ConsoleWork() { Message = "汪汪汪" }
                };
                work2ID = await host.AddWorkAsync(model);
                if (work2ID == Guid.Empty) Assert.Fail("添加任务2失败");
            }
            #endregion
            #region 修改
            {
                (List<WorkDTO> dataList, _) = await host.GetWorkListAsync(new QueryWorkModel { Name = "TestWork", PageIndex = 1, PageSize = 10 });
                if (dataList.Count <= 0) Assert.Fail("查询任务失败");
                WorkDTO dataInfo = await host.GetWorkInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) Assert.Fail("查询任务失败");
                EditWorkModel model = new()
                {
                    ID = dataInfo.ID,
                    Name = dataInfo.Name,
                    Description = dataInfo.Description,
                    WorkData = dataInfo.WorkData
                };
                await host.EditWorkAsync(model);
                dataInfo = await host.GetWorkInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) Assert.Fail("查询任务失败");
            }
            #endregion
            #endregion
            #region 调度器
            #region 添加
            {
                AddScheduleModel model = new()
                {
                    Name = "TestSchedule",
                    Description = "测试调度",
                    Answers = new()
                    {
                        new()
                        {
                            Name = "测试任务响应",
                            AnswerData = new ConsoleAnswer() { Message = "测试任务响应输出" },
                            WorkEvent = "Success"
                        }
                    },
                    Plans = new()
                    {
                        new()
                        {
                            Name = "执行一次",
                            Description = "执行一次",
                            PlanTriggerData = new OneTimePlanTrigger { StartTime = DateTime.Now.AddSeconds(5) }
                        }
                    },
                    Works = new()
                    {
                        new() { WorkID = workID }
                    }
                };
                scheduleID = await host.AddScheduleAsync(model);
                if (scheduleID == Guid.Empty) Assert.Fail("添加调度器失败");
            }
            #endregion
            #region 修改
            {
                (List<ScheduleDTO> dataList, _) = await host.GetScheduleListAsync(new QueryScheduleModel { Name = "TestSchedule", PageIndex = 1, PageSize = 10 });
                if (dataList.Count <= 0) Assert.Fail("查询调度器失败");
                ScheduleDTO dataInfo = await host.GetScheduleInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) Assert.Fail("查询调度器失败");
                EditScheduleModel model = new()
                {
                    ID = dataInfo.ID,
                    Name = dataInfo.Name,
                    Description = dataInfo.Description,
                    Territory = dataInfo.Territory,
                    Enable = dataInfo.Enable,
                    Answers = new()
                    {
                        new()
                        {
                            Name = "测试任务响应",
                            AnswerData = new ConsoleAnswer() { Message = "测试任务响应输出" },
                            WorkEvent = "Success"
                        },
                        new()
                        {
                            Name = "测试任务响应2",
                            AnswerData = new ConsoleAnswer() { Message = "测试任务响应输出2" },
                            WorkEvent = "Success"
                        }
                    },
                    Plans = new()
                    {
                        new()
                        {
                            Name = "执行一次",
                            Description = "执行一次",
                            PlanTriggerData = new OneTimePlanTrigger { StartTime = DateTime.Now.AddSeconds(5) }
                        }
                    },
                    Works = new()
                    {
                        new() { WorkID = workID },
                        new() { WorkID = work2ID }
                    }
                };
                await host.EditScheduleAsync(model);
                dataInfo = await host.GetScheduleInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) Assert.Fail("查询调度器失败");
            }
            #endregion
            #endregion
        }
        /// <summary>
        /// 初始化仓储测试
        /// </summary>
        [TestMethod]
        public void InitRepositoryTest()
        {
            GetServiceTest<IOscillatorUnitOfWork>();
            GetServiceTest<IAnswerRepository>();
            GetServiceTest<IPlanRepository>();
            GetServiceTest<IScheduleRepository>();
            GetServiceTest<IScheduleWorkRepository>();
            GetServiceTest<IWorkRepository>();
        }
    }
}