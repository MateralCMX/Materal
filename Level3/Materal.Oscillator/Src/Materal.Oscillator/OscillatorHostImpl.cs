using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.DR.Domain;
using Materal.Oscillator.Abstractions.DR.Models;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Helper;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.Abstractions.Works;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.QuartZExtend;
using Quartz;

namespace Materal.Oscillator
{
    /// <summary>
    /// Oscillator主机
    /// </summary>
    public class OscillatorHostImpl : IOscillatorHost
    {
        private readonly IServiceProvider _serviceProvider;
        /// <summary>
        /// 构造方法
        /// </summary>
        public OscillatorHostImpl(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 容灾启动
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public async Task DRRunAsync(Flow flow)
        {
            try
            {
                using IServiceScope serviceScope = _serviceProvider.CreateScope();
                IServiceProvider serviceProvider = serviceScope.ServiceProvider;
                IScheduleRepository scheduleRepository = serviceProvider.GetRequiredService<IScheduleRepository>();
                IScheduleWorkRepository scheduleWorkViewRepository = serviceProvider.GetRequiredService<IScheduleWorkRepository>();
                IOscillatorListener? oscillatorListener = serviceProvider.GetService<IOscillatorListener>();
                Schedule schedule = flow.ScheduleData.JsonToObject<ScheduleFlowModel>();
                List<ScheduleWork> scheduleWorks = await scheduleWorkViewRepository.FindAsync(m => m.ScheduleID == schedule.ID);
                if (scheduleWorks.Count <= 0) return;
                IPlanTrigger planTrigger = new NowPlanTrigger();
                Plan tempPlan = new()
                {
                    Name = "容灾启动计划",
                    Description = "该计划是容灾重启的计划",
                    ID = Guid.Empty,
                    PlanTriggerData = planTrigger.Serialize(),
                    PlanTriggerType = nameof(NowPlanTrigger),
                    ScheduleID = schedule.ID
                };
                tempPlan.ID = Guid.Empty;
                schedule.Name += $"_容灾自起{DateTime.Now:yyyyMMddHHmmssffff}";
                IJobDetail? job = GetJobDetail(schedule, scheduleWorks.ToArray(), flow);
                if (job == null) return;
                ITrigger? trigger = await PlanToTriggerAsync(oscillatorListener, schedule, tempPlan);
                if (trigger != null && oscillatorListener != null)
                {
                    await oscillatorListener.ScheduleReadyAsync(schedule, planTrigger);
                }
                if (trigger == null) return;
                await StartAsync(oscillatorListener, schedule, job, trigger);
            }
            finally
            {
                await OscillatorQuartZManager.StartAsync();
            }
        }
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task StartAsync(Expression<Func<Schedule, bool>> expression)
        {
            try
            {
                using IServiceScope serviceScope = _serviceProvider.CreateScope();
                IServiceProvider serviceProvider = serviceScope.ServiceProvider;
                IScheduleRepository scheduleRepository = serviceProvider.GetRequiredService<IScheduleRepository>();
                List<Schedule> schedules = await scheduleRepository.FindAsync(expression);
                if (schedules == null || schedules.Count <= 0) return;
                await StartAsync(serviceProvider, schedules.ToArray());
            }
            finally
            {
                await OscillatorQuartZManager.StartAsync();
            }
        }
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="territory"></param>
        /// <returns></returns>
        public async Task StartAsync(string? territory = null)
        {
            Expression<Func<Schedule, bool>> expression = territory == null ? m => true : m => m.Territory == territory;
            await StartAsync(expression);
        }
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public async Task StartAsync(params Guid[] scheduleIDs)
        {
            Expression<Func<Schedule, bool>> expression = m => scheduleIDs.Contains(m.ID);
            await StartAsync(expression);
        }
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="schedules"></param>
        /// <returns></returns>
        public async Task StartAsync(params Schedule[] schedules)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            await StartAsync(serviceProvider, schedules);
        }
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task StopAsync(Expression<Func<Schedule, bool>> expression)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IScheduleRepository scheduleRepository = serviceProvider.GetRequiredService<IScheduleRepository>();
            List<Schedule> schedules = await scheduleRepository.FindAsync(expression);
            if (schedules == null || schedules.Count <= 0) return;
            IOscillatorListener? oscillatorListener = serviceProvider.GetService<IOscillatorListener>();
            await StopAsync(oscillatorListener, schedules.ToArray());
        }
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="territory"></param>
        /// <returns></returns>
        public async Task StopAsync(string? territory = null)
        {
            Expression<Func<Schedule, bool>> expression = territory == null ? m => true : m => m.Territory == territory;
            await StopAsync(expression);
        }
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public async Task StopAsync(params Guid[] scheduleIDs)
        {
            Expression<Func<Schedule, bool>> expression = m => scheduleIDs.Contains(m.ID);
            await StopAsync(expression);
        }
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="schedules"></param>
        /// <returns></returns>
        public async Task StopAsync(params Schedule[] schedules)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorListener? oscillatorListener = serviceProvider.GetService<IOscillatorListener>();
            await StopAsync(oscillatorListener, schedules);
        }
        /// <summary>
        /// 立刻执行调度器
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task RunNowAsync(Expression<Func<Schedule, bool>> expression)
        {
            try
            {
                using IServiceScope serviceScope = _serviceProvider.CreateScope();
                IServiceProvider serviceProvider = serviceScope.ServiceProvider;
                IScheduleRepository scheduleRepository = serviceProvider.GetRequiredService<IScheduleRepository>();
                List<Schedule> schedules = await scheduleRepository.FindAsync(expression);
                if (schedules == null || schedules.Count <= 0) return;
                await RunNowAsync(serviceProvider, schedules.ToArray());
            }
            finally
            {
                await OscillatorQuartZManager.StartAsync();
            }
        }
        /// <summary>
        /// 立刻执行调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public async Task RunNowAsync(params Guid[] scheduleIDs)
        {
            Expression<Func<Schedule, bool>> expression = m => scheduleIDs.Contains(m.ID);
            await RunNowAsync(expression);
        }
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddWorkAsync(AddWorkModel model)
        {
            model.Validation();
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();
            Work work = model.CopyProperties<Work>(nameof(Work.WorkData));
            work.WorkData = model.WorkData.Serialize();
            work.Validation();
            unitOfWork.RegisterAdd(work);
            await unitOfWork.CommitAsync();
            return work.ID;
        }
        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditWorkAsync(EditWorkModel model)
        {
            model.Validation();
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();
            IWorkRepository workRepository = unitOfWork.GetRepository<IWorkRepository>();
            Work work = await workRepository.FirstAsync(m => m.ID == model.ID);
            model.CopyProperties(work, nameof(Work.WorkData));
            work.WorkData = model.WorkData.Serialize();
            work.Validation();
            unitOfWork.RegisterEdit(work);
            await unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="workID"></param>
        /// <returns></returns>
        public async Task DeleteWorkAsync(Guid workID)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();
            IScheduleWorkRepository scheduleWorkRepository = unitOfWork.GetRepository<IScheduleWorkRepository>();
            if (await scheduleWorkRepository.ExistedAsync(m => m.WorkID == workID)) throw new OscillatorException("任务已被使用");
            IWorkRepository workRepository = unitOfWork.GetRepository<IWorkRepository>();
            Work work = await workRepository.FirstAsync(m => m.ID == workID);
            unitOfWork.RegisterDelete(work);
            await unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 获得任务信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<WorkDTO> GetWorkInfoAsync(Guid id)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IWorkRepository workRepository = serviceProvider.GetRequiredService<IWorkRepository>();
            Work work = await workRepository.FirstAsync(m => m.ID == id);
            WorkDTO result = new(work);
            return result;
        }
        /// <summary>
        /// 获得任务列表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(List<WorkDTO> data, PageModel pageInfo)> GetWorkListAsync(QueryWorkModel model)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IWorkRepository workRepository = serviceProvider.GetRequiredService<IWorkRepository>();
            (List<Work> works, PageModel pageInfo) = await workRepository.PagingAsync(model);
            List<WorkDTO> result = works.Select(m=>new WorkDTO(m)).ToList();
            return (result, pageInfo);
        }
        /// <summary>
        /// 添加调度器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Guid> AddScheduleAsync(AddScheduleModel model)
        {
            model.Validation();
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();
            Schedule schedule = model.CopyProperties<Schedule>();
            schedule.Validation();
            unitOfWork.RegisterAdd(schedule);
            AddPlans(unitOfWork, model.Plans, schedule.ID);
            AddAnswers(unitOfWork, model.Answers, schedule.ID);
            AddScheduleWorks(unitOfWork, model.Works, schedule.ID);
            await unitOfWork.CommitAsync();
            return schedule.ID;
        }
        /// <summary>
        /// 修改调度器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task EditScheduleAsync(EditScheduleModel model)
        {
            model.Validation();
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();
            IScheduleRepository scheduleRepository = unitOfWork.GetRepository<IScheduleRepository>();
            Schedule schedule = scheduleRepository.First(m => m.ID == model.ID);
            model.CopyProperties(schedule);
            schedule.Validation();
            unitOfWork.RegisterEdit(schedule);
            await EditPlansAsync(unitOfWork, model.Plans, schedule.ID);
            await EditAnswersAsync(unitOfWork, model.Answers, schedule.ID);
            await EditScheduleWorksAsync(unitOfWork, model.Works, schedule.ID);
            await unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 获得调度器信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ScheduleDTO> GetScheduleInfoAsync(Guid id)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();

            IScheduleRepository scheduleRepository = unitOfWork.GetRepository<IScheduleRepository>();
            Schedule schedule = await scheduleRepository.FirstAsync(id);
            ScheduleDTO result = new(schedule);

            result.Answers = await GetAnswerListByScheduleIDAsync(unitOfWork, result.ID);
            result.Works = await GetWorkListByScheduleIDAsync(unitOfWork, result.ID);
            result.Plans = await GetPlanListByScheduleIDAsync(unitOfWork, result.ID);

            return result;
        }
        /// <summary>
        /// 获得调度器列表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(List<ScheduleDTO> data, PageModel pageInfo)> GetScheduleListAsync(QueryScheduleModel model)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();

            IScheduleRepository scheduleRepository = unitOfWork.GetRepository<IScheduleRepository>();
            (List<Schedule> schedules, PageModel pageInfo) = await scheduleRepository.PagingAsync(model);
            List<ScheduleDTO> result = schedules.Select(m => new ScheduleDTO(m)).ToList();

            Guid[] scheduleIDs = result.Select(m => m.ID).ToArray();
            Dictionary<Guid, List<AnswerDTO>> answers = await GetAnswerListByScheduleIDAsync(unitOfWork, scheduleIDs);
            Dictionary<Guid, List<WorkDTO>> works = await GetWorkListByScheduleIDAsync(unitOfWork, scheduleIDs);
            Dictionary<Guid, List<PlanDTO>> plans = await GetPlanListByScheduleIDAsync(unitOfWork, scheduleIDs);
            foreach (ScheduleDTO schedule in result)
            {
                schedule.Answers = answers.ContainsKey(schedule.ID) ? answers[schedule.ID] : new();
                schedule.Works = works.ContainsKey(schedule.ID) ? works[schedule.ID] : new();
                schedule.Plans = plans.ContainsKey(schedule.ID) ? plans[schedule.ID] : new();
            }

            return (result, pageInfo);
        }
        /// <summary>
        /// 删除调度器
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public async Task DeleteScheduleAsync(Guid scheduleID)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();
            IScheduleRepository scheduleRepository = unitOfWork.GetRepository<IScheduleRepository>();
            Schedule schedule = await scheduleRepository.FirstAsync(scheduleID);
            IOscillatorListener? oscillatorListener = serviceProvider.GetService<IOscillatorListener>();
            await StopAsync(oscillatorListener, schedule);
            IPlanRepository planRepository = unitOfWork.GetRepository<IPlanRepository>();
            List<Plan> plans = await planRepository.FindAsync(m => m.ScheduleID == scheduleID);
            IAnswerRepository answerRepository = unitOfWork.GetRepository<IAnswerRepository>();
            List<Answer> answers = await answerRepository.FindAsync(m => m.ScheduleID == scheduleID);
            IScheduleWorkRepository scheduleWorkRepository = unitOfWork.GetRepository<IScheduleWorkRepository>();
            List<ScheduleWork> scheduleWorks = await scheduleWorkRepository.FindAsync(m => m.ScheduleID == scheduleID);
        }
        /// <summary>
        /// 禁用调度器
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DisableScheduleAsync(Guid scheduleID)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();
            IScheduleRepository scheduleRepository = unitOfWork.GetRepository<IScheduleRepository>();
            Schedule schedule = await scheduleRepository.FirstAsync(scheduleID);
            schedule.Enable = false;
            unitOfWork.RegisterEdit(schedule);
            await unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 启用调度器
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task EnableScheduleAsync(Guid scheduleID)
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;
            IOscillatorUnitOfWork unitOfWork = serviceProvider.GetRequiredService<IOscillatorUnitOfWork>();
            IScheduleRepository scheduleRepository = unitOfWork.GetRepository<IScheduleRepository>();
            Schedule schedule = await scheduleRepository.FirstAsync(scheduleID);
            schedule.Enable = true;
            unitOfWork.RegisterEdit(schedule);
            await unitOfWork.CommitAsync();
        }
        #region 私有方法
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="oscillatorListener"></param>
        /// <param name="schedules"></param>
        /// <returns></returns>
        private async Task StopAsync(IOscillatorListener? oscillatorListener, params Schedule[] schedules)
        {
            if (schedules == null || schedules.Length <= 0) return;
            foreach (Schedule schedule in schedules)
            {
                JobKey jobKey = OscillatorQuartZManager.GetJobKey(schedule);
                if (!await OscillatorQuartZManager.IsRuningAsync(jobKey)) continue;
                await OscillatorQuartZManager.RemoveJobAsync(jobKey);
                if (oscillatorListener != null)
                {
                    await oscillatorListener.ScheduleStopAsync(schedule);
                }
            }
        }
        /// <summary>
        /// 立即启动调度器
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="schedules"></param>
        /// <returns></returns>
        private async Task RunNowAsync(IServiceProvider serviceProvider, params Schedule[] schedules)
        {
            if (schedules == null || schedules.Length <= 0) return;
            Guid[] scheduleIDs = schedules.Select(m => m.ID).ToArray();
            IScheduleWorkRepository scheduleWorkRepository = serviceProvider.GetRequiredService<IScheduleWorkRepository>();
            List<ScheduleWork> scheduleWorks = await scheduleWorkRepository.FindAsync(m => scheduleIDs.Contains(m.ScheduleID));
            foreach (Schedule schedule in schedules)
            {
                ScheduleWork[] tempScheduleWorks = scheduleWorks.Where(m => m.ScheduleID == schedule.ID).ToArray();
                if (tempScheduleWorks.Length <= 0) continue;
                Plan tempPlan = new()
                {
                    Name = "立即执行计划",
                    Description = "该计划是立即执行的内置计划",
                    ID = Guid.Empty,
                    PlanTriggerData = new NowPlanTrigger().Serialize(),
                    PlanTriggerType = nameof(NowPlanTrigger),
                    ScheduleID = schedule.ID
                };
                schedule.Name += $"_{DateTime.Now:yyyyMMddHHmmssffff}";
                await StartAsync(serviceProvider, schedule, tempScheduleWorks, tempPlan);
            }
        }
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="schedules"></param>
        /// <returns></returns>
        private async Task StartAsync(IServiceProvider serviceProvider, params Schedule[] schedules)
        {
            if (schedules == null || schedules.Length <= 0) return;
            Guid[] scheduleIDs = schedules.Select(m => m.ID).ToArray();
            IPlanRepository planRepository = serviceProvider.GetRequiredService<IPlanRepository>();
            List<Plan> plans = await planRepository.FindAsync(m => scheduleIDs.Contains(m.ScheduleID));
            IScheduleWorkRepository scheduleWorkRepository = serviceProvider.GetRequiredService<IScheduleWorkRepository>();
            List<ScheduleWork> scheduleWorks = await scheduleWorkRepository.FindAsync(m => scheduleIDs.Contains(m.ScheduleID));
            foreach (Schedule schedule in schedules)
            {
                ScheduleWork[] tempScheduleWorks = scheduleWorks.Where(m => m.ScheduleID == schedule.ID).ToArray();
                if (tempScheduleWorks.Length <= 0) continue;
                Plan[] tempPlans = plans.Where(m => m.ScheduleID == schedule.ID).ToArray();
                if (tempPlans.Length <= 0) continue;
                await StartAsync(serviceProvider, schedule, tempScheduleWorks, tempPlans);
            }
        }
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWorks"></param>
        /// <param name="plans"></param>
        /// <returns></returns>
        private async Task StartAsync(IServiceProvider serviceProvider, Schedule schedule, ScheduleWork[] scheduleWorks, params Plan[] plans)
        {
            IOscillatorListener? oscillatorListener = serviceProvider.GetService<IOscillatorListener>();
            IJobDetail? job = GetJobDetail(schedule, scheduleWorks);
            if (job == null) return;
            List<ITrigger> triggers = new();
            foreach (Plan plan in plans)
            {
                ITrigger? trigger = await PlanToTriggerAsync(oscillatorListener, schedule, plan);
                if (trigger == null) continue;
                triggers.Add(trigger);
            }
            if (triggers.Count <= 0) return;
            if (await OscillatorQuartZManager.IsRuningAsync(job.Key)) return;
            await StartAsync(oscillatorListener, schedule, job, triggers.ToArray());
        }
        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="oscillatorListener"></param>
        /// <param name="schedule"></param>
        /// <param name="job"></param>
        /// <param name="triggers"></param>
        /// <returns></returns>
        private async Task StartAsync(IOscillatorListener? oscillatorListener, Schedule schedule, IJobDetail job, params ITrigger[] triggers)
        {
            if (!schedule.Enable) throw new OscillatorException("调度器未启用");
            if (triggers.Length <= 0) throw new OscillatorException("至少需要一个计划");
            await OscillatorQuartZManager.AddNewJobAsync(job, triggers);
            if (oscillatorListener != null)
            {
                await oscillatorListener.ScheduleStartAsync(schedule);
            }
        }
        /// <summary>
        /// 计划转换为触发器
        /// </summary>
        /// <param name="oscillatorListener"></param>
        /// <param name="schedule"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        private async Task<ITrigger?> PlanToTriggerAsync(IOscillatorListener? oscillatorListener, Schedule schedule, Plan plan)
        {
            if (string.IsNullOrEmpty(plan.PlanTriggerData)) return null;
            IPlanTrigger? planTrigger = OscillatorConvertHelper.ConvertToInterface<IPlanTrigger>(plan.PlanTriggerType, plan.PlanTriggerData);
            if (planTrigger == null) return null;
            TriggerKey triggerKey = OscillatorQuartZManager.GetTriggerKey(schedule, plan);
            ITrigger? trigger = planTrigger.CreateTrigger(triggerKey);
            if (trigger != null && oscillatorListener != null)
            {
                await oscillatorListener.ScheduleReadyAsync(schedule, planTrigger);
            }
            return trigger;
        }
        /// <summary>
        /// 获得作业明细
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWorks"></param>
        /// <param name="flow"></param>
        /// <returns></returns>
        private IJobDetail? GetJobDetail(Schedule schedule, ScheduleWork[] scheduleWorks, Flow? flow = null)
        {
            if (scheduleWorks.Length <= 0) return null;
            JobKey jobKey = OscillatorQuartZManager.GetJobKey(schedule);
            ScheduleFlowModel scheduleFlow = schedule.CopyProperties<ScheduleFlowModel>();
            JobDataMap dataMap = new()
            {
                [OscillatorJob.ScheduleDataMapKey] = scheduleFlow,
                [OscillatorJob.WorksDataMapKey] = scheduleWorks
            };
            if (flow != null)
            {
                dataMap.Add(OscillatorJob.FlowMapKey, flow);
            }
            IJobDetail quartZJobDetail = JobBuilder.Create<OscillatorJob>()
                .WithIdentity(jobKey)
                .UsingJobData(dataMap)
                .Build();
            return quartZJobDetail;
        }
        /// <summary>
        /// 添加调度器任务
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="models"></param>
        /// <param name="scheduleID"></param>
        private void AddScheduleWorks(IOscillatorUnitOfWork unitOfWork, List<AddScheduleWorkModel> models, Guid scheduleID)
        {
            for (int i = 0; i < models.Count; i++)
            {
                AddScheduleWorkModel model = models[i];
                AddScheduleWork(unitOfWork, model, scheduleID, i);
            }
        }
        /// <summary>
        /// 添加调度器任务
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="model"></param>
        /// <param name="scheduleID"></param>
        /// <param name="index"></param>
        private void AddScheduleWork(IOscillatorUnitOfWork unitOfWork, AddScheduleWorkModel model, Guid scheduleID, int index)
        {
            ScheduleWork domain = model.CopyProperties<ScheduleWork>();
            domain.ScheduleID = scheduleID;
            domain.Index = index;
            domain.Validation();
            unitOfWork.RegisterAdd(domain);
        }
        /// <summary>
        /// 修改调度器任务
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="models"></param>
        /// <param name="scheduleID"></param>
        private async Task EditScheduleWorksAsync(IOscillatorUnitOfWork unitOfWork, List<AddScheduleWorkModel> models, Guid scheduleID)
        {
            IScheduleWorkRepository repository = unitOfWork.GetRepository<IScheduleWorkRepository>();
            List<ScheduleWork> allDomains = await repository.FindAsync(m => m.ScheduleID == scheduleID);
            foreach (ScheduleWork domain in allDomains)
            {
                unitOfWork.RegisterDelete(domain);
            }
            for (int i = 0; i < models.Count; i++)
            {
                AddScheduleWorkModel model = models[i];
                AddScheduleWork(unitOfWork, model, scheduleID, i);
            }
        }
        /// <summary>
        /// 添加计划组
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="models"></param>
        /// <param name="scheduleID"></param>
        private void AddPlans(IOscillatorUnitOfWork unitOfWork, List<AddPlanModel> models, Guid scheduleID)
        {
            foreach (AddPlanModel model in models)
            {
                AddPlan(unitOfWork, model, scheduleID);
            }
        }
        /// <summary>
        /// 添加计划
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="model"></param>
        /// <param name="scheduleID"></param>
        private void AddPlan(IOscillatorUnitOfWork unitOfWork, AddPlanModel model, Guid scheduleID)
        {
            Plan domain = model.CopyProperties<Plan>(nameof(Plan.PlanTriggerData));
            domain.PlanTriggerData = model.PlanTriggerData.Serialize();
            domain.ScheduleID = scheduleID;
            domain.Validation();
            unitOfWork.RegisterAdd(domain);
        }
        /// <summary>
        /// 修改计划组
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="models"></param>
        /// <param name="scheduleID"></param>
        private async Task EditPlansAsync(IOscillatorUnitOfWork unitOfWork, List<AddPlanModel> models, Guid scheduleID)
        {
            IPlanRepository repository = unitOfWork.GetRepository<IPlanRepository>();
            List<Plan> allDomains = await repository.FindAsync(m => m.ScheduleID == scheduleID);
            foreach (Plan domain in allDomains)
            {
                unitOfWork.RegisterDelete(domain);
            }
            foreach (AddPlanModel model in models)
            {
                AddPlan(unitOfWork, model, scheduleID);
            }
        }
        /// <summary>
        /// 根据调度器唯一标识获取计划列表
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        private async Task<List<PlanDTO>> GetPlanListByScheduleIDAsync(IOscillatorUnitOfWork unitOfWork, Guid scheduleID)
        {
            IPlanRepository repository = unitOfWork.GetRepository<IPlanRepository>();
            List<Plan> domains = await repository.FindAsync(m => m.ScheduleID == scheduleID);
            List<PlanDTO> result = domains.Select(m => new PlanDTO(m)).ToList();
            return result;
        }
        /// <summary>
        /// 根据调度器唯一标识获取计划列表
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        private async Task<Dictionary<Guid, List<PlanDTO>>> GetPlanListByScheduleIDAsync(IOscillatorUnitOfWork unitOfWork, params Guid[] scheduleIDs)
        {
            IPlanRepository repository = unitOfWork.GetRepository<IPlanRepository>();
            Dictionary<Guid, List<PlanDTO>> result = new();
            List<Plan> domains = await repository.FindAsync(m => scheduleIDs.Contains(m.ScheduleID));
            foreach (IGrouping<Guid, Plan> item in domains.GroupBy(m=>m.ScheduleID))
            {
                result.Add(item.Key, item.Select(m => new PlanDTO(m)).ToList());
            }
            return result;
        }
        /// <summary>
        /// 添加响应组
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="models"></param>
        /// <param name="scheduleID"></param>
        private void AddAnswers(IOscillatorUnitOfWork unitOfWork, List<AddAnswerModel> models, Guid scheduleID)
        {
            for (int i = 0; i < models.Count; i++)
            {
                AddAnswerModel model = models[i];
                AddAnswer(unitOfWork, model, scheduleID, i);
            }
        }
        /// <summary>
        /// 添加响应
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="model"></param>
        /// <param name="scheduleID"></param>
        /// <param name="index"></param>
        private void AddAnswer(IOscillatorUnitOfWork unitOfWork, AddAnswerModel model, Guid scheduleID, int index)
        {
            Answer domain = model.CopyProperties<Answer>(nameof(Answer.AnswerData));
            domain.AnswerData = model.AnswerData.Serialize();
            domain.ScheduleID = scheduleID;
            domain.Index = index;
            domain.Validation();
            unitOfWork.RegisterAdd(domain);
        }
        /// <summary>
        /// 修改响应组
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="models"></param>
        /// <param name="scheduleID"></param>
        private async Task EditAnswersAsync(IOscillatorUnitOfWork unitOfWork, List<AddAnswerModel> models, Guid scheduleID)
        {
            IAnswerRepository repository = unitOfWork.GetRepository<IAnswerRepository>();
            List<Answer> allDomains = await repository.FindAsync(m => m.ScheduleID == scheduleID);
            foreach (Answer domain in allDomains)
            {
                unitOfWork.RegisterDelete(domain);
            }
            for (int i = 0; i < models.Count; i++)
            {
                AddAnswerModel model = models[i];
                AddAnswer(unitOfWork, model, scheduleID, i);
            }
        }
        /// <summary>
        /// 根据调度器唯一标识获取响应列表
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        private async Task<List<AnswerDTO>> GetAnswerListByScheduleIDAsync(IOscillatorUnitOfWork unitOfWork, Guid scheduleID)
        {
            IAnswerRepository repository = unitOfWork.GetRepository<IAnswerRepository>();
            List<Answer> domains = await repository.FindAsync(m => m.ScheduleID == scheduleID);
            List<AnswerDTO> result = domains.OrderBy(m=>m.Index).Select(m => new AnswerDTO(m)).ToList();
            return result;
        }
        /// <summary>
        /// 根据调度器唯一标识获取响应列表
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        private async Task<Dictionary<Guid, List<AnswerDTO>>> GetAnswerListByScheduleIDAsync(IOscillatorUnitOfWork unitOfWork, params Guid[] scheduleIDs)
        {
            IAnswerRepository repository = unitOfWork.GetRepository<IAnswerRepository>();
            Dictionary<Guid, List<AnswerDTO>> result = new();
            List<Answer> domains = await repository.FindAsync(m => scheduleIDs.Contains(m.ScheduleID));
            foreach (IGrouping<Guid, Answer> item in domains.GroupBy(m => m.ScheduleID))
            {
                result.Add(item.Key, item.OrderBy(m => m.Index).Select(m => new AnswerDTO(m)).ToList());
            }
            return result;
        }
        /// <summary>
        /// 根据调度器唯一标识获取任务列表
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        private async Task<List<WorkDTO>> GetWorkListByScheduleIDAsync(IOscillatorUnitOfWork unitOfWork, Guid scheduleID)
        {
            IWorkRepository repository = unitOfWork.GetRepository<IWorkRepository>();
            IScheduleWorkRepository scheduleWorkRepository = unitOfWork.GetRepository<IScheduleWorkRepository>();
            List<ScheduleWork> scheduleWorks = await scheduleWorkRepository.FindAsync(m => m.ScheduleID == scheduleID);
            List<Guid> workIDs = scheduleWorks.Select(m => m.WorkID).Distinct().ToList();
            List<Work> domains = await repository.FindAsync(m => workIDs.Contains(m.ID));
            List<WorkDTO> result = domains.Select(m => new WorkDTO(m)).ToList();
            return result;
        }
        /// <summary>
        /// 根据调度器唯一标识获取任务列表
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        private async Task<Dictionary<Guid, List<WorkDTO>>> GetWorkListByScheduleIDAsync(IOscillatorUnitOfWork unitOfWork, params Guid[] scheduleIDs)
        {
            IWorkRepository repository = unitOfWork.GetRepository<IWorkRepository>();
            IScheduleWorkRepository scheduleWorkRepository = unitOfWork.GetRepository<IScheduleWorkRepository>();
            Dictionary<Guid, List<WorkDTO>> result = new();
            List<ScheduleWork> scheduleWorks = await scheduleWorkRepository.FindAsync(m => scheduleIDs.Contains(m.ScheduleID));
            List<Guid> workIDs = scheduleWorks.Select(m => m.WorkID).Distinct().ToList();
            List<Work> domains = await repository.FindAsync(m => workIDs.Contains(m.ID));
            foreach (IGrouping<Guid, ScheduleWork> item in scheduleWorks.GroupBy(m => m.ScheduleID))
            {
                List<Guid> tempWorkIDs = item.Select(m => m.WorkID).Distinct().ToList();
                result.Add(item.Key, domains.Where(m => tempWorkIDs.Contains(m.ID)).Select(m => new WorkDTO(m)).ToList());
            }
            return result;
        }
        #endregion
    }
}
