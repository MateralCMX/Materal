using AutoMapper;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Common;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models.Plan;
using Materal.Oscillator.Abstractions.Models.Schedule;
using Materal.Oscillator.Abstractions.Models.ScheduleWork;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.AutoMapperProfile;
using Materal.Oscillator.DR.Domain;
using Materal.Oscillator.DR.Models;
using Materal.Oscillator.Models;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.QuartZExtend;
using Quartz;

namespace Materal.Oscillator
{
    /// <summary>
    /// 调度器服务
    /// </summary>
    public class OscillatorService
    {
        private readonly IOscillatorListener? _oscillatorListener;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IScheduleWorkViewRepository _scheduleWorkViewRepository;
        private readonly IMapper _mapper;
        private readonly IOscillatorUnitOfWork _unitOfWork;
        public OscillatorService(IMapper mapper, IOscillatorUnitOfWork unitOfWork, IOscillatorListener? oscillatorListener = null)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _scheduleRepository = _unitOfWork.GetRepository<IScheduleRepository>();
            _planRepository = _unitOfWork.GetRepository<IPlanRepository>();
            _scheduleWorkViewRepository = _unitOfWork.GetRepository<IScheduleWorkViewRepository>();
            _oscillatorListener = oscillatorListener;
        }
        #region 启动调度器
        /// <summary>
        /// 启动所有调度器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task StartAllAsync(ScheduleOperationModel? model)
        {
            try
            {
                QueryScheduleModel queryModel = new();
                model?.SetTerritoryProperties(queryModel);
                IList<Schedule> schedules = await _scheduleRepository.FindAsync(queryModel);
                if (schedules == null || schedules.Count <= 0) return;
                await StartAsync(schedules.ToArray());
            }
            finally
            {
                await QuartZHelper.StartAsync();
            }
        }
        /// <summary>
        /// 根据ID组启动调度器
        /// </summary>
        /// <param name="model"></param>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public async Task StartAsync(ScheduleOperationModel? model, params Guid[] scheduleIDs)
        {
            try
            {
                IList<Schedule>? schedules = await GetSchedulesAsync(model, scheduleIDs);
                if (schedules == null || schedules.Count <= 0) return;
                await StartAsync(schedules.ToArray());
            }
            finally
            {
                await QuartZHelper.StartAsync();
            }
        }
        /// <summary>
        /// 根据ID组立即启动调度器
        /// </summary>
        /// <param name="model"></param>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public async Task RunNowAsync(ScheduleOperationModel? model, params Guid[] scheduleIDs)
        {
            try
            {
                IList<Schedule>? schedules = await GetSchedulesAsync(model, scheduleIDs);
                if (schedules == null || schedules.Count <= 0) return;
                await RunNowAsync(schedules.ToArray());
            }
            finally
            {
                await QuartZHelper.StartAsync();
            }
        }
        /// <summary>
        /// 容灾启动
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="startWorkID"></param>
        /// <param name="flow"></param>
        /// <returns></returns>
        public async Task DRRunAsync(Flow flow)
        {
            try
            {
                Schedule schedule = flow.ScheduleData.JsonToObject<ScheduleFlowModel>();
                QueryScheduleWorkModel queryScheduleWorkModel = new()
                {
                    ScheduleID = schedule.ID
                };
                IList<ScheduleWorkView> scheduleWorks = await _scheduleWorkViewRepository.FindAsync(queryScheduleWorkModel);
                if (scheduleWorks.Count <= 0) return;
                IPlanTrigger planTrigger = new NowPlanTrigger();
                AddPlanModel addPlan = new()
                {
                    Enable = true,
                    Name = "容灾启动计划",
                    Description = "该计划是容灾重启的计划",
                    PlanTriggerData = planTrigger,
                    ScheduleID = schedule.ID
                };
                Plan tempPlan = _mapper.Map<Plan>(addPlan);
                tempPlan.ID = Guid.Empty;
                schedule.Name += $"_容灾自起{DateTime.Now:yyyyMMddHHmmssffff}";
                IJobDetail? job = GetJobDetail(schedule, scheduleWorks.ToArray(), flow);
                if (job == null) return;
                ITrigger? trigger = await PlanToTriggerAsync(schedule, tempPlan);
                if (trigger != null && _oscillatorListener != null)
                {
                    await _oscillatorListener.ScheduleReadyAsync(schedule, planTrigger);
                }
                if (trigger == null) return;
                await StartAsync(schedule, job, trigger);
            }
            finally
            {
                await QuartZHelper.StartAsync();
            }
        }
        #endregion
        #region 停止调度器
        /// <summary>
        /// 停止所有调度器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task StopAllAsync(ScheduleOperationModel? model)
        {
            QueryScheduleModel queryModel = new();
            model?.SetTerritoryProperties(queryModel);
            IList<Schedule> schedules = await _scheduleRepository.FindAsync(queryModel);
            if (schedules == null || schedules.Count <= 0) return;
            await StopAsync(schedules.ToArray());
        }
        /// <summary>
        /// 根据ID组停止调度器
        /// </summary>
        /// <param name="model"></param>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public async Task StopAsync(ScheduleOperationModel? model, params Guid[] scheduleIDs)
        {
            if (scheduleIDs == null || scheduleIDs.Length <= 0) return;
            QueryScheduleModel queryModel = new()
            {
                IDs = scheduleIDs.ToList()
            };
            model?.SetTerritoryProperties(queryModel);
            IList<Schedule> schedules = await _scheduleRepository.FindAsync(queryModel);
            if (schedules == null || schedules.Count <= 0) return;
            await StopAsync(schedules.ToArray());
        }
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="schedules"></param>
        /// <returns></returns>
        public async Task StopAsync(params Schedule[] schedules)
        {
            if (schedules == null || schedules.Length <= 0) return;
            foreach (Schedule schedule in schedules)
            {
                JobKey jobKey = QuartZHelper.GetJobKey(schedule);
                if (!await QuartZHelper.IsRuningAsync(jobKey)) continue;
                await QuartZHelper.RemoveJobAsync(jobKey);
                if(_oscillatorListener != null)
                {
                    await _oscillatorListener.ScheduleStopAsync(schedule);
                }
            }
        }
        #endregion
        #region 私有方法
        /// <summary>
        /// 获得调度器组
        /// </summary>
        /// <param name="model"></param>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        private async Task<IList<Schedule>?> GetSchedulesAsync(ScheduleOperationModel? model, params Guid[] scheduleIDs)
        {
            if (scheduleIDs == null || scheduleIDs.Length <= 0) return null;
            QueryScheduleModel queryModel = new()
            {
                IDs = scheduleIDs.ToList()
            };
            model?.SetTerritoryProperties(queryModel);
            IList<Schedule> schedules = await _scheduleRepository.FindAsync(queryModel);
            if (schedules == null || schedules.Count <= 0) return null;
            return schedules;
        }
        /// <summary>
        /// 启动指定调度器
        /// </summary>
        /// <param name="schedules"></param>
        /// <returns></returns>
        private async Task StartAsync(params Schedule[] schedules)
        {
            if (schedules == null || schedules.Length <= 0) return;
            Guid[] scheduleIDs = schedules.Select(m => m.ID).ToArray();
            IList<Plan> plans = await _planRepository.FindAsync(new QueryPlanManagerModel
            {
                Enable = true,
                ScheduleIDs = scheduleIDs.ToList()
            });
            IList<ScheduleWorkView> scheduleWorks = await _scheduleWorkViewRepository.FindAsync(new QueryScheduleWorkModel
            {
                ScheduleIDs = scheduleIDs.ToList()
            });
            foreach (Schedule schedule in schedules)
            {
                ScheduleWorkView[] tempScheduleWorks = scheduleWorks.Where(m => m.ScheduleID == schedule.ID).ToArray();
                if (tempScheduleWorks.Length <= 0) continue;
                Plan[] tempPlans = plans.Where(m => m.ScheduleID == schedule.ID).ToArray();
                if (tempPlans.Length <= 0) continue;
                await StartAsync(schedule, tempScheduleWorks, tempPlans);
            }
        }
        /// <summary>
        /// 立即启动调度器
        /// </summary>
        /// <param name="schedules"></param>
        /// <returns></returns>
        private async Task RunNowAsync(params Schedule[] schedules)
        {
            if (schedules == null || schedules.Length <= 0) return;
            Guid[] scheduleIDs = schedules.Select(m => m.ID).ToArray();
            IList<ScheduleWorkView> scheduleWorks = await _scheduleWorkViewRepository.FindAsync(new QueryScheduleWorkModel
            {
                ScheduleIDs = scheduleIDs.ToList()
            });
            foreach (Schedule schedule in schedules)
            {
                ScheduleWorkView[] tempScheduleWorks = scheduleWorks.Where(m => m.ScheduleID == schedule.ID).ToArray();
                if (tempScheduleWorks.Length <= 0) continue;
                Plan tempPlan = new()
                {
                    Enable = true,
                    Name = "立即执行计划",
                    Description = "该计划是立即执行的内置计划",
                    ID = Guid.Empty,
                    PlanTriggerData = new NowPlanTrigger().Serialize(),
                    PlanTriggerType = nameof(NowPlanTrigger),
                    ScheduleID = schedule.ID
                };
                schedule.Name += $"_{DateTime.Now:yyyyMMddHHmmssffff}";
                await StartAsync(schedule, tempScheduleWorks, tempPlan);
            }
        }
        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWorks"></param>
        /// <param name="plans"></param>
        /// <returns></returns>
        private async Task StartAsync(Schedule schedule, ScheduleWorkView[] scheduleWorks, params Plan[] plans)
        {
            IJobDetail? job = GetJobDetail(schedule, scheduleWorks);
            if (job == null) return;
            List<ITrigger> triggers = new();
            foreach (Plan plan in plans)
            {
                ITrigger? trigger = await PlanToTriggerAsync(schedule, plan);
                if (trigger == null) continue;
                triggers.Add(trigger);
            }
            if (triggers.Count <= 0) return;
            if (await QuartZHelper.IsRuningAsync(job.Key)) return;
            await StartAsync(schedule, job, triggers.ToArray());
        }
        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="job"></param>
        /// <param name="triggers"></param>
        /// <returns></returns>
        private async Task StartAsync(Schedule schedule, IJobDetail job, params ITrigger[] triggers)
        {
            if (!schedule.Enable) throw new OscillatorException("调度器未启用");
            if (triggers.Length <= 0) throw new OscillatorException("至少需要一个计划");
            await QuartZHelper.AddNewJobAsync(job, triggers);
            if (_oscillatorListener != null)
            {
                await _oscillatorListener.ScheduleStartAsync(schedule);
            }
        }
        /// <summary>
        /// 获得作业明细
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWorks"></param>
        /// <param name="startWorkID"></param>
        /// <param name="flow"></param>
        /// <returns></returns>
        private IJobDetail? GetJobDetail(Schedule schedule, ScheduleWorkView[] scheduleWorks, Flow? flow = null)
        {
            if (scheduleWorks.Length <= 0) return null;
            JobKey jobKey = QuartZHelper.GetJobKey(schedule);
            var scheduleFlow = _mapper.Map<ScheduleFlowModel>(schedule);
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
        /// 计划转换为触发器
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        private async Task<ITrigger?> PlanToTriggerAsync(Schedule schedule, Plan plan)
        {
            if (!plan.Enable) return null;
            if (string.IsNullOrEmpty(plan.PlanTriggerData)) return null;
            IPlanTrigger? planTrigger = OscillatorConvertHelper.ConvertToInterface<IPlanTrigger>(plan.PlanTriggerType, plan.PlanTriggerData);
            if (planTrigger == null) return null;
            TriggerKey triggerKey = QuartZHelper.GetTriggerKey(schedule, plan);
            ITrigger? trigger = planTrigger.CreateTrigger(triggerKey);
            if(trigger != null && _oscillatorListener != null)
            {
                await _oscillatorListener.ScheduleReadyAsync(schedule, planTrigger);
            }
            return trigger;
        }
        #endregion
    }
}
