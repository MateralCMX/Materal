using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.DR;
using Materal.Oscillator.DR.Domain;
using Materal.Oscillator.DR.Models;
using Materal.Oscillator.DR.Repositories;
using Materal.Oscillator.QuartZExtend;
using Quartz;
using System.Threading.Tasks.Dataflow;

namespace Materal.Oscillator.LocalDR
{
    public class OscillatorLocalDR : IOscillatorDR
    {
        private readonly IOscillatorDRUnitOfWork _unitOfWork;
        private readonly IFlowRepository _flowRepository;
        private readonly OscillatorService _oscillatorService;
        private readonly ActionBlock<Func<Task>> _actionBlock;
        public OscillatorLocalDR(IOscillatorDRUnitOfWork unitOfWork, OscillatorService oscillatorService)
        {
            _oscillatorService = oscillatorService;
            _actionBlock = new(SaveChange);
            _unitOfWork = unitOfWork;
            _flowRepository = _unitOfWork.GetRepository<IFlowRepository>();
        }
        public Task ScheduleExecuteAsync(Schedule schedule)
        {
            if (schedule is not ScheduleFlowModel scheduleFlow) return Task.CompletedTask;
            JobKey jobKey = QuartZHelper.GetJobKey(scheduleFlow);
            Flow flow = new()
            {
                JobKey = jobKey.ToString(),
                ScheduleID = scheduleFlow.ID,
                ScheduleData = scheduleFlow.ToJson(),
                AuthenticationCode = scheduleFlow.AuthenticationCode
            };
            _actionBlock.Post(async () =>
            {
                _unitOfWork.RegisterAdd(flow);
                await _unitOfWork.CommitAsync();
            });
            return Task.CompletedTask;
        }
        public Task ScheduleExecutedAsync(Schedule schedule)
        {
            if (schedule is not ScheduleFlowModel scheduleFlow) return Task.CompletedTask;
            _actionBlock.Post(async () =>
            {
                Flow? flow = await GetFlowBySchedultAsync(scheduleFlow);
                if (flow == null) return;
                _unitOfWork.RegisterDelete(flow);
                await _unitOfWork.CommitAsync();
            });
            return Task.CompletedTask;
        }
        public Task ScheduleExecuteAsync(Flow flow) => Task.CompletedTask;
        public Task ScheduleExecutedAsync(Flow flow)
        {
            _actionBlock.Post(async () =>
            {
                _unitOfWork.RegisterDelete(flow);
                await _unitOfWork.CommitAsync();
            });
            return Task.CompletedTask;
        }
        public Task WorkExecuteAsync(Schedule schedule, ScheduleWorkView scheduleWork)
        {
            if (schedule is not ScheduleFlowModel scheduleFlow) return Task.CompletedTask;
            _actionBlock.Post(async () =>
            {
                Flow? flow = await GetFlowBySchedultAsync(scheduleFlow);
                if (flow == null) return;
                await SaveEditAsync(flow, scheduleWork);
            });
            return Task.CompletedTask;
        }
        public Task WorkExecutedAsync(Schedule schedule, ScheduleWorkView scheduleWork, string? workResult)
        {
            if (schedule is not ScheduleFlowModel scheduleFlow) return Task.CompletedTask;
            _actionBlock.Post(async () =>
            {
                Flow? flow = await GetFlowBySchedultAsync(scheduleFlow);
                if (flow == null || flow.WorkID != scheduleWork.WorkID) return;
                await SaveEditAsync(flow, scheduleWork, workResult);
            });
            return Task.CompletedTask;
        }
        public Task WorkExecuteAsync(Flow flow, ScheduleWorkView scheduleWork)
        {
            _actionBlock.Post(async () =>
            {
                await SaveEditAsync(flow, scheduleWork);
            });
            return Task.CompletedTask;
        }
        public Task WorkExecutedAsync(Flow flow, ScheduleWorkView scheduleWork, string? workResult)
        {
            _actionBlock.Post(async () =>
            {
                await SaveEditAsync(flow, scheduleWork, workResult);
            });
            return Task.CompletedTask;
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
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        private async Task SaveEditAsync(Flow flow, ScheduleWorkView scheduleWork)
        {
            flow.WorkID = scheduleWork.WorkID;
            _unitOfWork.RegisterEdit(flow);
            await _unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="workResult"></param>
        /// <returns></returns>
        private async Task SaveEditAsync(Flow flow, ScheduleWorkView scheduleWork, string? workResult)
        {
            flow.WorkID = scheduleWork.WorkID;
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
        /// <summary>
        /// 保存更改
        /// </summary>
        /// <param name="task"></param>
        private void SaveChange(Func<Task> task)
        {
            task().Wait();
        }
        /// <summary>
        /// 调度器启动
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
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
