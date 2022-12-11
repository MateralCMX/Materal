using Materal.ConvertHelper;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.DR;
using Materal.Oscillator.DR.Domain;
using Materal.Oscillator.DR.Models;
using Materal.Oscillator.DR.Repositories;
using Materal.Oscillator.QuartZExtend;
using Quartz;

namespace Materal.Oscillator.LocalDR
{
    public class OscillatorLocalDR : IOscillatorDR
    {
        private readonly IOscillatorUnitOfWork _unitOfWork;
        private readonly IFlowRepository _flowRepository;
        private readonly OscillatorService _oscillatorService;
        public OscillatorLocalDR(IFlowRepository flowRepository, OscillatorService oscillatorService, IOscillatorUnitOfWork unitOfWork)
        {
            _flowRepository = flowRepository;
            _oscillatorService = oscillatorService;
            _unitOfWork = unitOfWork;
        }
        public async Task InitAsync()
        {
            await _flowRepository.InitTableAsync();
        }
        public async Task ScheduleExecuteAsync(Schedule schedule)
        {
            if (schedule is not ScheduleFlowModel scheduleFlow) return;
            JobKey jobKey = QuartZHelper.GetJobKey(scheduleFlow);
            Flow flow = new()
            {
                JobKey = jobKey.ToString(),
                ScheduleID = scheduleFlow.ID,
                ScheduleData = scheduleFlow.ToJson(),
                AuthenticationCode = scheduleFlow.AuthenticationCode
            };
            _unitOfWork.RegisterAdd(flow);
            await _unitOfWork.CommitAsync();
            await ScheduleExecuteAsync(flow);
        }
        public async Task ScheduleExecutedAsync(Schedule schedule)
        {
            if (schedule is not ScheduleFlowModel scheduleFlow) return;
            Flow? flow = await GetFlowBySchedultAsync(scheduleFlow);
            if (flow == null) return;
            await ScheduleExecutedAsync(flow);
        }
        public Task ScheduleExecuteAsync(Flow flow) => Task.CompletedTask;
        public async Task ScheduleExecutedAsync(Flow flow)
        {
            _unitOfWork.RegisterDelete(flow);
            await _unitOfWork.CommitAsync();
        }
        public async Task WorkExecuteAsync(Schedule schedule, ScheduleWorkView scheduleWork)
        {
            if (schedule is not ScheduleFlowModel scheduleFlow) return;
            Flow? flow = await GetFlowBySchedultAsync(scheduleFlow);
            if (flow == null) return;
            await WorkExecuteAsync(flow, scheduleWork);
        }
        public async Task WorkExecutedAsync(Schedule schedule, ScheduleWorkView scheduleWork, string? workResult)
        {
            if (schedule is not ScheduleFlowModel scheduleFlow) return;
            Flow? flow = await GetFlowBySchedultAsync(scheduleFlow);
            if (flow == null || flow.WorkID != scheduleWork.WorkID) return;
            await WorkExecutedAsync(flow, scheduleWork, workResult);
        }
        public async Task WorkExecuteAsync(Flow flow, ScheduleWorkView scheduleWork)
        {
            flow.WorkID = scheduleWork.WorkID;
            _unitOfWork.RegisterEdit(flow);
            await _unitOfWork.CommitAsync();
        }
        public async Task WorkExecutedAsync(Flow flow, ScheduleWorkView scheduleWork, string? workResult)
        {
            List<string?> workResults;
            if (string.IsNullOrWhiteSpace(flow.WorkResults))
            {
                workResults = new List<string?> { workResult };
            }
            else
            {
                workResults = flow.WorkResults.JsonToObject<List<string?>>();
                workResults.Add(workResult);
            }
            flow.WorkResults = workResults.ToJson();
            _unitOfWork.RegisterEdit(flow);
            await _unitOfWork.CommitAsync();
        }
        public async Task ScheduleStartAsync()
        {
            IList<Flow> allFlow = await _flowRepository.FindAsync(new QueryFlowModel
            {
                PageIndex = 1,
                PageSize = int.MaxValue
            });
            foreach (Flow flow in allFlow)
            {
                await ScheduleStartAsync(flow);
            }
        }
        #region 私有方法
        private async Task ScheduleStartAsync(Flow flow)
        {
            await _oscillatorService.DRRunAsync(flow);
        }
        /// <summary>
        /// 根据调度器获取流程
        /// </summary>
        /// <param name="scheduleFlow"></param>
        /// <returns></returns>
        private async Task<Flow?> GetFlowBySchedultAsync(ScheduleFlowModel scheduleFlow)
        {
            Flow? flow = await _flowRepository.FirstOrDefaultAsync(new QueryFlowModel
            {
                AuthenticationCode = scheduleFlow.AuthenticationCode
            });
            return flow;
        }
        #endregion
    }
}
