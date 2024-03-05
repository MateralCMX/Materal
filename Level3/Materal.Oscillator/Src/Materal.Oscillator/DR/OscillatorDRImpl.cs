using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.DR;
using Materal.Oscillator.Abstractions.DR.Domain;
using Materal.Oscillator.Abstractions.DR.Models;
using Materal.Oscillator.Abstractions.DR.Repositories;
using Materal.Oscillator.QuartZExtend;
using Quartz;

namespace Materal.Oscillator.DR
{
    /// <summary>
    /// 调度器容灾
    /// </summary>
    public class OscillatorDRImpl(IServiceProvider serviceProvider, IOscillatorHost host) : IOscillatorDR
    {
        /// <summary>
        /// 调度器执行
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task ScheduleExecuteAsync(Schedule schedule)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IOscillatorDRUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IOscillatorDRUnitOfWork>();
            ScheduleFlowModel scheduleFlow = GetScheduleFlowModel(schedule);
            JobKey jobKey = OscillatorQuartZManager.GetJobKey(scheduleFlow);
            Flow flow = new()
            {
                JobKey = jobKey.ToString(),
                ScheduleID = scheduleFlow.ID,
                ScheduleData = scheduleFlow.ToJson(),
                AuthenticationCode = scheduleFlow.AuthenticationCode
            };
            unitOfWork.RegisterAdd(flow);
            await unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 调度器执行
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public Task ScheduleExecuteAsync(Flow flow) => Task.CompletedTask;
        /// <summary>
        /// 调度器执行完毕
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task ScheduleExecutedAsync(Schedule schedule)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IOscillatorDRUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IOscillatorDRUnitOfWork>();
            ScheduleFlowModel scheduleFlow = GetScheduleFlowModel(schedule);
            Flow? flow = await GetFlowBySchedultAsync(unitOfWork, scheduleFlow);
            if (flow == null) return;
            await ScheduleExecutedAsync(unitOfWork, flow);
        }
        /// <summary>
        /// 调度器执行完毕
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public async Task ScheduleExecutedAsync(Flow flow)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IOscillatorDRUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IOscillatorDRUnitOfWork>();
            await ScheduleExecutedAsync(unitOfWork, flow);
        }
        /// <summary>
        /// 调度器启动
        /// </summary>
        /// <returns></returns>
        public async Task ScheduleStartAsync()
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IOscillatorDRUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IOscillatorDRUnitOfWork>();
            IFlowRepository flowRepository = unitOfWork.GetRepository<IFlowRepository>();
            IList<Flow> allFlow = await flowRepository.FindAsync(m => true);
            foreach (Flow flow in allFlow)
            {
                await host.DRRunAsync(flow);
            }
        }
        /// <summary>
        /// 任务执行
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public async Task WorkExecuteAsync(Schedule schedule, ScheduleWork scheduleWork)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IOscillatorDRUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IOscillatorDRUnitOfWork>();
            ScheduleFlowModel scheduleFlow = GetScheduleFlowModel(schedule);
            Flow? flow = await GetFlowBySchedultAsync(unitOfWork, scheduleFlow);
            if (flow == null) return;
            await SaveEditAsync(unitOfWork, flow, scheduleWork);
        }
        /// <summary>
        /// 任务执行
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public async Task WorkExecuteAsync(Flow flow, ScheduleWork scheduleWork)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IOscillatorDRUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IOscillatorDRUnitOfWork>();
            await SaveEditAsync(unitOfWork, flow, scheduleWork);
        }
        /// <summary>
        /// 任务执行完毕
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="workResult"></param>
        /// <returns></returns>
        public async Task WorkExecutedAsync(Schedule schedule, ScheduleWork scheduleWork, string? workResult)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IOscillatorDRUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IOscillatorDRUnitOfWork>();
            ScheduleFlowModel scheduleFlow = GetScheduleFlowModel(schedule);
            Flow? flow = await GetFlowBySchedultAsync(unitOfWork, scheduleFlow);
            if (flow == null || flow.WorkID != scheduleWork.WorkID) return;
            await SaveEditAsync(unitOfWork, flow, scheduleWork, workResult);
        }
        /// <summary>
        /// 任务执行完毕
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="workResult"></param>
        /// <returns></returns>
        public async Task WorkExecutedAsync(Flow flow, ScheduleWork scheduleWork, string? workResult)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IOscillatorDRUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IOscillatorDRUnitOfWork>();
            await SaveEditAsync(unitOfWork, flow, scheduleWork, workResult);
        }
        #region 私有方法
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="flow"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        private async Task SaveEditAsync(IOscillatorDRUnitOfWork unitOfWork, Flow flow, ScheduleWork scheduleWork)
        {
            flow.WorkID = scheduleWork.WorkID;
            unitOfWork.RegisterEdit(flow);
            await unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="flow"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="workResult"></param>
        /// <returns></returns>
        private async Task SaveEditAsync(IOscillatorDRUnitOfWork unitOfWork, Flow flow, ScheduleWork scheduleWork, string? workResult)
        {
            flow.WorkID = scheduleWork.WorkID;
            List<string?> workResults;
            if (flow.WorkResults == null || string.IsNullOrWhiteSpace(flow.WorkResults))
            {
                workResults = [workResult];
            }
            else
            {
                workResults = flow.WorkResults.JsonToObject<List<string?>>();
                workResults.Add(workResult);
            }
            flow.WorkResults = workResults.ToJson();
            unitOfWork.RegisterEdit(flow);
            await unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 根据调度器获取流程
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="scheduleFlow"></param>
        /// <returns></returns>
        private async Task<Flow?> GetFlowBySchedultAsync(IOscillatorDRUnitOfWork unitOfWork, ScheduleFlowModel scheduleFlow)
        {
            IFlowRepository flowRepository = unitOfWork.GetRepository<IFlowRepository>();
            Flow? flow = await flowRepository.FirstOrDefaultAsync(new QueryFlowModel
            {
                AuthenticationCode = scheduleFlow.AuthenticationCode
            });
            return flow;
        }
        /// <summary>
        /// 获得调度器流程模型
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private ScheduleFlowModel GetScheduleFlowModel(Schedule schedule)
        {
            if (schedule is ScheduleFlowModel scheduleFlow) return scheduleFlow;
            scheduleFlow = schedule.CopyProperties<ScheduleFlowModel>();
            return scheduleFlow;
        }
        /// <summary>
        /// 调度器执行完毕
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="flow"></param>
        /// <returns></returns>
        private async Task ScheduleExecutedAsync(IOscillatorDRUnitOfWork unitOfWork, Flow flow)
        {
            unitOfWork.RegisterDelete(flow);
            await unitOfWork.CommitAsync();
        }
        #endregion
    }
}
