using ConsoleDemo.Works;
using Dy.Oscillator.Abstractions;
using Materal.Logger.ConfigModels;
using Materal.Logger.ConsoleLogger;
using Materal.Logger.Extensions;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.Answers;
using Materal.Oscillator.PlanTriggers;

namespace ConsoleDemo
{
    public class Program
    {
        private static readonly IServiceProvider _services;
        private static readonly IOscillatorHost _host;
        static Program()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            PageRequestModel.PageStartNumber = 1;
            serviceCollection.AddMateralUtils();
            serviceCollection.AddMateralLogger(config =>
            {
                config.AddConsoleTarget("LifeConsole");
                config.AddAllTargetsRule(minLevel: LogLevel.Information, logLevels: new() { ["Microsoft.EntityFrameworkCore"] = LogLevel.Warning });
            });
            serviceCollection.AddOscillator(Assembly.Load("ConsoleDemo"));
            SqliteEFRepositoryHelper repositoryHelper = new();
            //SqlServerEFRepositoryHelper repositoryHelper = new();
            repositoryHelper.AddDRRepository(serviceCollection);
            repositoryHelper.AddRepository(serviceCollection);
            serviceCollection.AddSingleton<IOscillatorListener, OscillatorListenerImpl>();
            serviceCollection.AddSingleton<IRetryAnswerListener, RetryAnswerListenerImpl>();
            MateralServices.Services = serviceCollection.BuildServiceProvider();
            OscillatorServices.Services = MateralServices.Services;
            _services = MateralServices.Services;
            _services.UseMateralLoggerAsync().Wait();
            repositoryHelper.Init(_services);
            _host = _services.GetRequiredService<IOscillatorHost>();
        }
        public static async Task Main()
        {
            await ClearAllDataAsync();
            await InitHostTestAsync();
            await _host.StartAsync();
            //(List<ScheduleDTO> dataList, _) = await _host.GetScheduleListAsync(new QueryScheduleModel { PageIndex = 1, PageSize = 1 });
            //if (dataList.Count <= 0) return;
            //ScheduleDTO dataInfo = dataList.First();
            //await _host.RunNowAsync(dataInfo.ID);
            //await _host.StartAsync(dataInfo.ID);
            Console.ReadKey();
        }
        private static async Task InitHostTestAsync()
        {
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
                    WorkData = new DemoWorkData() { Message = "喵喵喵" }
                };
                workID = await _host.AddWorkAsync(model);
                if (workID == Guid.Empty) throw new Exception("添加任务失败");
                model = new()
                {
                    Name = "TestWork2",
                    Description = "测试任务2",
                    WorkData = new DemoWorkData() { Message = "汪汪汪" }
                };
                work2ID = await _host.AddWorkAsync(model);
                if (work2ID == Guid.Empty) throw new Exception("添加任务2失败");
            }
            #endregion
            #region 修改
            {
                (List<WorkDTO> dataList, _) = await _host.GetWorkListAsync(new QueryWorkModel { Name = "TestWork", PageIndex = 1, PageSize = 10 });
                if (dataList.Count <= 0) throw new Exception("查询任务失败");
                WorkDTO dataInfo = await _host.GetWorkInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) throw new Exception("查询任务失败");
                EditWorkModel model = new()
                {
                    ID = dataInfo.ID,
                    Name = dataInfo.Name,
                    Description = dataInfo.Description,
                    WorkData = dataInfo.WorkData
                };
                await _host.EditWorkAsync(model);
                dataInfo = await _host.GetWorkInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) throw new Exception("查询任务失败");
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
                    Answers =
                    [
                        new()
                        {
                            Name = "测试任务响应",
                            AnswerData = new ConsoleAnswer() { Message = "测试任务响应输出" },
                            WorkEvent = "Success"
                        }
                    ],
                    Plans =
                    [
                        new()
                        {
                            Name = "执行一次",
                            Description = "执行一次",
                            PlanTriggerData = new OneTimePlanTrigger { StartTime = DateTime.Now.AddSeconds(5) }
                        }
                    ],
                    Works =
                    [
                        new() { WorkID = workID }
                    ]
                };
                scheduleID = await _host.AddScheduleAsync(model);
                if (scheduleID == Guid.Empty) throw new Exception("添加调度器失败");
            }
            #endregion
            #region 修改
            {
                (List<ScheduleDTO> dataList, _) = await _host.GetScheduleListAsync(new QueryScheduleModel { Name = "TestSchedule", PageIndex = 1, PageSize = 10 });
                if (dataList.Count <= 0) throw new Exception("查询调度器失败");
                ScheduleDTO dataInfo = await _host.GetScheduleInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) throw new Exception("查询调度器失败");
                EditScheduleModel model = new()
                {
                    ID = dataInfo.ID,
                    Name = dataInfo.Name,
                    Description = dataInfo.Description,
                    Territory = dataInfo.Territory,
                    Enable = dataInfo.Enable,
                    Answers =
                    [
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
                    ],
                    Plans =
                    [
                        new()
                        {
                            Name = "执行一次",
                            Description = "执行一次",
                            PlanTriggerData = new OneTimePlanTrigger { StartTime = DateTime.Now.AddSeconds(5) }
                        }
                        //new()
                        //{
                        //    Name = "每5秒执行一次",
                        //    Description = "每5秒执行一次",
                        //    PlanTriggerData = new RepeatPlanTrigger
                        //    {
                        //        DateTrigger = new DateDayTrigger(),
                        //        EveryDayTrigger = new EveryDayRepeatTrigger() { Interval = 5, IntervalType = EveryDayIntervalType.Second }
                        //    }
                        //}
                    ],
                    Works =
                    [
                        new() { WorkID = workID },
                        new() { WorkID = work2ID }
                    ]
                };
                await _host.EditScheduleAsync(model);
                dataInfo = await _host.GetScheduleInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) throw new Exception("查询调度器失败");
            }
            #endregion
            #endregion
        }
        private static async Task ClearAllDataAsync()
        {
            using IServiceScope scope = _services.CreateScope();
            IOscillatorUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IOscillatorUnitOfWork>();
            IAnswerRepository answerRepository = unitOfWork.GetRepository<IAnswerRepository>();
            List<Answer> answers = await answerRepository.FindAsync(m => true);
            foreach (var item in answers)
            {
                unitOfWork.RegisterDelete(item);
            }
            IPlanRepository planRepository = unitOfWork.GetRepository<IPlanRepository>();
            List<Plan> plans = await planRepository.FindAsync(m => true);
            foreach (var item in plans)
            {
                unitOfWork.RegisterDelete(item);
            }
            IScheduleRepository scheduleRepository = unitOfWork.GetRepository<IScheduleRepository>();
            List<Schedule> schedules = await scheduleRepository.FindAsync(m => true);
            foreach (var item in schedules)
            {
                unitOfWork.RegisterDelete(item);
            }
            IScheduleWorkRepository scheduleWorkRepository = unitOfWork.GetRepository<IScheduleWorkRepository>();
            List<ScheduleWork> scheduleWorks = await scheduleWorkRepository.FindAsync(m => true);
            foreach (var item in scheduleWorks)
            {
                unitOfWork.RegisterDelete(item);
            }
            IWorkRepository workRepository = unitOfWork.GetRepository<IWorkRepository>();
            List<Work> works = await workRepository.FindAsync(m => true);
            foreach (var item in works)
            {
                unitOfWork.RegisterDelete(item);
            }
            await unitOfWork.CommitAsync();
        }
    }
}