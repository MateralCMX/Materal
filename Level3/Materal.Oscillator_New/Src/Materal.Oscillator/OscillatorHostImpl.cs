using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Answers;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.PlanTriggers;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.Abstractions.Works;
using Materal.TTA.Common;
using Materal.Utils.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        /// 启动所有调度器
        /// </summary>
        /// <returns></returns>
        public Task StartAllAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 停止所有调度器
        /// </summary>
        /// <returns></returns>
        public Task StopAllAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据调度器唯一标识启动调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public Task StartAsync(params Guid[] scheduleIDs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 根据调度器唯一标识停止调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public Task StopAsync(params Guid[] scheduleIDs)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 立刻执行一个调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public Task RunNowAsync(params Guid[] scheduleIDs)
        {
            throw new NotImplementedException();
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
            work.Name = model.Name;
            work.WorkType = model.WorkType;
            work.WorkData = model.WorkData.Serialize();
            work.Description = model.Description;
            work.Validation();
            unitOfWork.RegisterEdit(work);
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
            AddWorkEvents(unitOfWork, model.WorkEvents, schedule.ID);
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
            await EditWorkEventsAsync(unitOfWork, model.WorkEvents, schedule.ID);
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
            result.WorkEvents = await GetWorkEventListByScheduleIDAsync(unitOfWork, result.ID);
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
            Dictionary<Guid, List<WorkEventDTO>> workEvents = await GetWorkEventListByScheduleIDAsync(unitOfWork, scheduleIDs);
            Dictionary<Guid, List<PlanDTO>> plans = await GetPlanListByScheduleIDAsync(unitOfWork, scheduleIDs);
            foreach (ScheduleDTO schedule in result)
            {
                schedule.Answers = answers.ContainsKey(schedule.ID) ? answers[schedule.ID] : new();
                schedule.Works = works.ContainsKey(schedule.ID) ? works[schedule.ID] : new();
                schedule.WorkEvents = workEvents.ContainsKey(schedule.ID) ? workEvents[schedule.ID] : new();
                schedule.Plans = plans.ContainsKey(schedule.ID) ? plans[schedule.ID] : new();
            }

            return (result, pageInfo);
        }
        /// <summary>
        /// 删除调度器
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task DeleteScheduleAsync(Guid scheduleID)
        {
            throw new NotImplementedException();
        }
        #region 私有方法
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
        /// 添加任务事件组
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="models"></param>
        /// <param name="scheduleID"></param>
        private void AddWorkEvents(IOscillatorUnitOfWork unitOfWork, List<AddWorkEventModel> models, Guid scheduleID)
        {
            foreach (AddWorkEventModel model in models)
            {
                AddWorkEvent(unitOfWork, model, scheduleID);
            }
        }
        /// <summary>
        /// 添加任务事件
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="model"></param>
        /// <param name="scheduleID"></param>
        private void AddWorkEvent(IOscillatorUnitOfWork unitOfWork, AddWorkEventModel model, Guid scheduleID)
        {
            WorkEvent domain = model.CopyProperties<WorkEvent>();
            domain.ScheduleID = scheduleID;
            domain.Validation();
            unitOfWork.RegisterAdd(domain);
        }
        /// <summary>
        /// 修改任务事件组
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="models"></param>
        /// <param name="scheduleID"></param>
        private async Task EditWorkEventsAsync(IOscillatorUnitOfWork unitOfWork, List<AddWorkEventModel> models, Guid scheduleID)
        {
            IWorkEventRepository repository = unitOfWork.GetRepository<IWorkEventRepository>();
            List<WorkEvent> allDomains = await repository.FindAsync(m => m.ScheduleID == scheduleID);
            foreach (WorkEvent domain in allDomains)
            {
                unitOfWork.RegisterDelete(domain);
            }
            foreach (AddWorkEventModel model in models)
            {
                AddWorkEvent(unitOfWork, model, scheduleID);
            }
        }
        /// <summary>
        /// 根据调度器唯一标识获取任务事件列表
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        private async Task<List<WorkEventDTO>> GetWorkEventListByScheduleIDAsync(IOscillatorUnitOfWork unitOfWork, Guid scheduleID)
        {
            IWorkEventRepository repository = unitOfWork.GetRepository<IWorkEventRepository>();
            List<WorkEvent> domains = await repository.FindAsync(m => m.ScheduleID == scheduleID);
            List<WorkEventDTO> result = domains.Select(m => new WorkEventDTO(m)).ToList();
            return result;
        }
        /// <summary>
        /// 根据调度器唯一标识获取任务事件列表
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        private async Task<Dictionary<Guid, List<WorkEventDTO>>> GetWorkEventListByScheduleIDAsync(IOscillatorUnitOfWork unitOfWork, params Guid[] scheduleIDs)
        {
            IWorkEventRepository repository = unitOfWork.GetRepository<IWorkEventRepository>();
            Dictionary<Guid, List<WorkEventDTO>> result = new();
            List<WorkEvent> domains = await repository.FindAsync(m => scheduleIDs.Contains(m.ScheduleID));
            foreach (IGrouping<Guid, WorkEvent> item in domains.GroupBy(m => m.ScheduleID))
            {
                result.Add(item.Key, item.Select(m => new WorkEventDTO(m)).ToList());
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
