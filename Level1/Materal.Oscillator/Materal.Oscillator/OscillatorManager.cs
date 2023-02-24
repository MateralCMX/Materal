using AutoMapper;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Common;
using Materal.Oscillator.Abstractions.DataTransmitModel;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.Models.Answer;
using Materal.Oscillator.Abstractions.Models.Plan;
using Materal.Oscillator.Abstractions.Models.Schedule;
using Materal.Oscillator.Abstractions.Models.ScheduleWork;
using Materal.Oscillator.Abstractions.Models.Work;
using Materal.Oscillator.Abstractions.Models.WorkEvent;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.Models;
using Materal.Oscillator.QuartZExtend;
using Materal.Utils.Model;
using Quartz;

namespace Materal.Oscillator
{
    public class OscillatorManager : ScheduleOperationModel, IOscillatorManager
    {
        private readonly IMapper _mapper;
        private readonly IOscillatorUnitOfWork _unitOfWork;
        private readonly IAnswerRepository _answerRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IScheduleWorkRepository _scheduleWorkRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IWorkEventRepository _workEventRepository;
        private readonly IAnswerViewRepository _answerViewRepository;
        private readonly IPlanViewRepository _planViewRepository;
        private readonly IScheduleWorkViewRepository _scheduleWorkViewRepository;
        private readonly IWorkEventViewRepository _workEventViewRepository;
        private readonly OscillatorService _oscillatorService;
        public OscillatorManager(IMapper mapper, IAnswerRepository answerRepository, IPlanRepository planRepository, IScheduleRepository scheduleRepository, IScheduleWorkRepository scheduleWorkRepository, IWorkRepository workRepository, IWorkEventRepository workEventRepository, IAnswerViewRepository answerViewRepository, IPlanViewRepository planViewRepository, IScheduleWorkViewRepository scheduleWorkViewRepository, IWorkEventViewRepository workEventViewRepository, OscillatorService oscillatorService, IOscillatorUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _answerRepository = answerRepository;
            _planRepository = planRepository;
            _scheduleRepository = scheduleRepository;
            _scheduleWorkRepository = scheduleWorkRepository;
            _workRepository = workRepository;
            _workEventRepository = workEventRepository;
            _answerViewRepository = answerViewRepository;
            _planViewRepository = planViewRepository;
            _scheduleWorkViewRepository = scheduleWorkViewRepository;
            _workEventViewRepository = workEventViewRepository;
            _oscillatorService = oscillatorService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> AddAnswerAsync(AddAnswerModel model) => await AddAsync<Answer>(model);
        public async Task<Guid> AddPlanAsync(AddPlanModel model) => await AddAsync<Plan>(model);
        public async Task<Guid> AddScheduleAsync(AddScheduleModel model) => await AddAsync<Schedule>(model);
        public async Task<Guid> AddWorkAsync(AddWorkModel model) => await AddAsync<Work>(model);
        public async Task<Guid> AddWorkEventAsync(AddWorkEventModel model) => await AddAsync<WorkEvent>(model);
        public async Task<Guid> AddScheduleWorkAsync(AddScheduleWorkModel model) => await AddAsync<ScheduleWork>(model);
        public void ChangeTerritory(string territory)
        {
            if (string.IsNullOrWhiteSpace(territory)) throw new OscillatorException("业务领域不能为空");
            Territory = territory;
        }
        public async Task DeleteAnswerAsync(Guid answerID) => await DeleteAsync(answerID, _answerRepository);
        public async Task DeletePlanAsync(Guid planID) => await DeleteAsync(planID, _planRepository);
        public async Task DeleteScheduleAsync(Guid scheduleID) => await DeleteAsync(scheduleID, _scheduleRepository);
        public async Task DeleteWorkAsync(Guid workID) => await DeleteAsync(workID, _workRepository);
        public async Task DeleteWorkEventAsync(Guid workEventID) => await DeleteAsync(workEventID, _workEventRepository);
        public async Task DeleteScheduleWorkAsync(Guid scheduleWorkID) => await DeleteAsync(scheduleWorkID, _scheduleWorkRepository);
        public async Task EditAnswerAsync(EditAnswerModel model) => await EditAsync(model, _answerRepository);
        public async Task EditPlanAsync(EditPlanModel model) => await EditAsync(model, _planRepository);
        public async Task EditScheduleAsync(EditScheduleModel model) => await EditAsync(model, _scheduleRepository);
        public async Task EditWorkAsync(EditWorkModel model) => await EditAsync(model, _workRepository);
        public async Task EditWorkEventAsync(EditWorkEventModel model) => await EditAsync(model, _workEventRepository);
        public async Task EditScheduleWorkAsync(EditScheduleWorkModel model) => await EditAsync(model, _scheduleWorkRepository);
        public async Task<List<AnswerDTO>> GetAllAnswerListAsync(Guid scheduleID) => await GetListAsync<AnswerView, QueryAnswerModel, AnswerDTO>(scheduleID, _answerViewRepository);
        public async Task<List<PlanDTO>> GetAllPlanListAsync(Guid scheduleID) => await GetListAsync<PlanView, QueryPlanModel, PlanDTO>(scheduleID, _planViewRepository);
        public async Task<List<ScheduleWorkDTO>> GetAllScheduleWorkListAsync(Guid scheduleID) => await GetListAsync<ScheduleWorkView, QueryScheduleWorkModel, ScheduleWorkDTO>(scheduleID, _scheduleWorkViewRepository);
        public async Task<List<ScheduleDTO>> GetAllScheduleListAsync() => await GetListAsync<Schedule, QueryScheduleModel, ScheduleDTO>(_scheduleRepository);
        public async Task<List<WorkEventDTO>> GetAllWorkEventListAsync(Guid scheduleID) => await GetListAsync<WorkEventView, QueryWorkEventModel, WorkEventDTO>(scheduleID, _workEventViewRepository);
        public async Task<AnswerDTO?> GetAnswerAsync(Guid answerID) => await GetInfoAsync<AnswerView, AnswerDTO>(answerID, _answerViewRepository);
        public async Task<PlanDTO?> GetPlanAsync(Guid planID) => await GetInfoAsync<PlanView, PlanDTO>(planID, _planViewRepository);
        public async Task<ScheduleDTO?> GetScheduleAsync(Guid scheduleID) => await GetInfoAsync<Schedule, ScheduleDTO>(scheduleID, _scheduleRepository);
        public async Task<WorkDTO?> GetWorkAsync(Guid workID) => await GetInfoAsync<Work, WorkDTO>(workID, _workRepository);
        public async Task<WorkEventDTO?> GetWorkEventAsync(Guid workEventID) => await GetInfoAsync<WorkEventView, WorkEventDTO>(workEventID, _workEventViewRepository);
        public async Task<ScheduleWorkDTO?> GetScheduleWorkAsync(Guid scheduleID) => await GetInfoAsync<ScheduleWorkView, ScheduleWorkDTO>(scheduleID, _scheduleWorkViewRepository);
        public async Task<(List<ScheduleDTO> data, PageModel pageInfo)> GetScheduleListAsync(QueryScheduleManagerModel model) => await PagingAsync<Schedule, QueryScheduleModel, ScheduleDTO>(model, _scheduleRepository);
        public async Task<(List<AnswerDTO> data, PageModel pageInfo)> GetAnswerListAsync(QueryAnswerManagerModel model) => await PagingAsync<AnswerView, QueryAnswerModel, AnswerDTO>(model, _answerViewRepository);
        public async Task<(List<PlanDTO> data, PageModel pageInfo)> GetPlanListAsync(QueryPlanManagerModel model) => await PagingAsync<PlanView, QueryPlanModel, PlanDTO>(model, _planViewRepository);
        public async Task<(List<WorkEventDTO> data, PageModel pageInfo)> GetWorkEventListAsync(QueryWorkEventManagerModel model) => await PagingAsync<WorkEventView, QueryWorkEventModel, WorkEventDTO>(model, _workEventViewRepository);
        public async Task<(List<ScheduleWorkDTO> data, PageModel pageInfo)> GetScheduleWorkListAsync(QueryScheduleWorkManagerModel model) => await PagingAsync<ScheduleWorkView, QueryScheduleWorkModel, ScheduleWorkDTO>(model, _scheduleWorkViewRepository);
        public async Task<(List<WorkDTO> data, PageModel pageInfo)> GetWorkListAsync(QueryWorkModel model) => await PagingAsync<Work, QueryWorkModel, WorkDTO>(model, _workRepository);
        public async Task<bool> IsRuningAsync(Guid scheduleID)
        {
            var queryModel = new QueryScheduleModel
            {
                ID = scheduleID
            };
            SetTerritoryProperties(queryModel);
            Schedule? schedule = await _scheduleRepository.FirstOrDefaultAsync(queryModel);
            if (schedule == null) return false;
            return await IsRuningAsync(schedule);
        }
        public async Task<bool> IsRuningAsync(Schedule schedule)
        {
            JobKey jobKey = QuartZHelper.GetJobKey(schedule);
            return await QuartZHelper.IsRuningAsync(jobKey);
        }
        public async Task RunNowAsync(Guid scheduleID) => await _oscillatorService.RunNowAsync(this, scheduleID);
        public async Task StartAllAsync() => await _oscillatorService.StartAllAsync(this);
        public async Task StartAsync(params Guid[] scheduleIDs) => await _oscillatorService.StartAsync(this, scheduleIDs);
        public async Task StopAllAsync() => await _oscillatorService.StopAllAsync(this);
        public async Task StopAsync(params Guid[] scheduleIDs) => await _oscillatorService.StopAsync(this, scheduleIDs);
        public IOscillatorBuild CreateOscillatorBuild(string name, string? description) => CreateOscillatorBuild(new ScheduleModel
        {
            Name = name,
            Description = description
        });
        public IOscillatorBuild CreateOscillatorBuild(ScheduleModel model) => new OscillatorBuild(model, this);
        #region 私有方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        private async Task<Guid> AddAsync<T>(object model)
            where T : BaseDomain
        {
            model.Validation();
            T domain = _mapper.Map<T>(model);
            SetTerritoryProperties(domain);
            domain.Validation();
            _unitOfWork.RegisterAdd(domain);
            await _unitOfWork.CommitAsync();
            return domain.ID;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        private async Task EditAsync<T>(IEditModel model, IOscillatorRepository<T> repository)
            where T : BaseDomain
        {
            model.Validation();
            T? domain = await repository.FirstOrDefaultAsync(model.ID);
            if (domain == null) throw new OscillatorException("数据不存在");
            _mapper.Map(model, domain);
            SetTerritoryProperties(domain);
            domain.Validation();
            _unitOfWork.RegisterEdit(domain);
            await _unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        private async Task DeleteAsync<T>(Guid id, IOscillatorRepository<T> repository)
            where T : BaseDomain
        {
            T? domain = await repository.FirstOrDefaultAsync(id);
            if (domain == null) throw new OscillatorException("数据不存在");
            _unitOfWork.RegisterDelete(domain);
            await _unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TQuery"></typeparam>
        /// <param name="model"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        private async Task<List<TDTO>> GetListAsync<T, TQuery, TDTO>(Guid scheduleID, IOscillatorRepository<T> repository)
            where T : BaseDomain
            where TQuery : FilterModel, IScheduleIDModel, new()
        {
            TQuery queryModel = new()
            {
                ScheduleID = scheduleID
            };
            SetTerritoryProperties(queryModel);
            List<T> data = await repository.FindAsync(queryModel);
            List<TDTO> result = _mapper.Map<List<TDTO>>(data);
            return result;
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TQuery"></typeparam>
        /// <param name="model"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        private async Task<List<TDTO>> GetListAsync<T, TQuery, TDTO>(IOscillatorRepository<T> repository)
            where T : BaseDomain
            where TQuery : FilterModel, new()
        {
            TQuery queryModel = new();
            SetTerritoryProperties(queryModel);
            List<T> data = await repository.FindAsync(queryModel);
            List<TDTO> result = _mapper.Map<List<TDTO>>(data);
            return result;
        }
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TQuery"></typeparam>
        /// <param name="model"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        private async Task<(List<TDTO> data, PageModel pageInfo)> PagingAsync<T, TQuery, TDTO>(PageRequestModel model, IOscillatorRepository<T> repository)
            where T : BaseDomain
            where TQuery : PageRequestModel
        {
            TQuery queryModel = _mapper.Map<TQuery>(model);
            SetTerritoryProperties(queryModel);
            (List<T> data, PageModel pageInfo) = await repository.PagingAsync(queryModel);
            List<TDTO> result = _mapper.Map<List<TDTO>>(data);
            return (result, pageInfo);
        }
        /// <summary>
        /// 查询信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TDTO"></typeparam>
        /// <param name="id"></param>
        /// <param name="repository"></param>
        /// <returns></returns>
        private async Task<TDTO?> GetInfoAsync<T, TDTO>(Guid id, IOscillatorRepository<T> repository)
        where T : BaseDomain
        {
            T? data = await repository.FirstOrDefaultAsync(id);
            if (data == null) return default;
            TDTO result = _mapper.Map<TDTO>(data);
            return result;
        }
        #endregion
    }
}
