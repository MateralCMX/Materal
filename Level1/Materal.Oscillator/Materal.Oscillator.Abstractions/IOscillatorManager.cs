using Materal.Model;
using Materal.Oscillator.Abstractions.DataTransmitModel;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Models.Answer;
using Materal.Oscillator.Abstractions.Models.Plan;
using Materal.Oscillator.Abstractions.Models.Schedule;
using Materal.Oscillator.Abstractions.Models.ScheduleWork;
using Materal.Oscillator.Abstractions.Models.Work;
using Materal.Oscillator.Abstractions.Models.WorkEvent;

namespace Materal.Oscillator.Abstractions
{
    public interface IOscillatorManager
    {
        /// <summary>
        /// 业务领域
        /// </summary>
        public string Territory { get; }
        /// <summary>
        /// 添加响应
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<Guid> AddAnswerAsync(AddAnswerModel model);
        /// <summary>
        /// 添加计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<Guid> AddPlanAsync(AddPlanModel model);
        /// <summary>
        /// 添加调度器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<Guid> AddScheduleAsync(AddScheduleModel model);
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<Guid> AddWorkAsync(AddWorkModel model);
        /// <summary>
        /// 添加任务事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<Guid> AddWorkEventAsync(AddWorkEventModel model);
        /// <summary>
        /// 添加调度器任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<Guid> AddScheduleWorkAsync(AddScheduleWorkModel model);
        /// <summary>
        /// 更改业务领域
        /// </summary>
        /// <param name="territory"></param>
        public void ChangeTerritory(string territory);
        /// <summary>
        /// 删除响应
        /// </summary>
        /// <param name="answerID"></param>
        /// <returns></returns>
        public Task DeleteAnswerAsync(Guid answerID);
        /// <summary>
        /// 删除计划
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public Task DeletePlanAsync(Guid planID);
        /// <summary>
        /// 删除调度器
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task DeleteScheduleAsync(Guid scheduleID);
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="workID"></param>
        /// <returns></returns>
        public Task DeleteWorkAsync(Guid workID);
        /// <summary>
        /// 删除任务事件
        /// </summary>
        /// <param name="workEventID"></param>
        /// <returns></returns>
        public Task DeleteWorkEventAsync(Guid workEventID);
        /// <summary>
        /// 删除调度器任务
        /// </summary>
        /// <param name="scheduleWorkID"></param>
        /// <returns></returns>
        public Task DeleteScheduleWorkAsync(Guid scheduleWorkID);
        /// <summary>
        /// 修改响应
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task EditAnswerAsync(EditAnswerModel model);
        /// <summary>
        /// 修改计划
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task EditPlanAsync(EditPlanModel model);
        /// <summary>
        /// 修改调度器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task EditScheduleAsync(EditScheduleModel model);
        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task EditWorkAsync(EditWorkModel model);
        /// <summary>
        /// 修改任务事件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task EditWorkEventAsync(EditWorkEventModel model);
        /// <summary>
        /// 修改调度器任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task EditScheduleWorkAsync(EditScheduleWorkModel model);
        /// <summary>
        /// 获得所有响应
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task<List<AnswerDTO>> GetAllAnswerListAsync(Guid scheduleID);
        /// <summary>
        /// 获得所有计划
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task<List<PlanDTO>> GetAllPlanListAsync(Guid scheduleID);
        /// <summary>
        /// 获得所有调度器
        /// </summary>
        /// <returns></returns>
        public Task<List<ScheduleDTO>> GetAllScheduleListAsync();
        /// <summary>
        /// 获得所有任务事件
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task<List<WorkEventDTO>> GetAllWorkEventListAsync(Guid scheduleID);
        /// <summary>
        /// 获得所有调度器任务
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task<List<ScheduleWorkDTO>> GetAllScheduleWorkListAsync(Guid scheduleID);
        /// <summary>
        /// 获得响应
        /// </summary>
        /// <param name="answerID"></param>
        /// <returns></returns>
        public Task<AnswerDTO?> GetAnswerAsync(Guid answerID);
        /// <summary>
        /// 获得响应列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<(List<AnswerDTO> data, PageModel pageInfo)> GetAnswerListAsync(QueryAnswerManagerModel model);
        /// <summary>
        /// 获得计划
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public Task<PlanDTO?> GetPlanAsync(Guid planID);
        /// <summary>
        /// 获得计划列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<(List<PlanDTO> data, PageModel pageInfo)> GetPlanListAsync(QueryPlanManagerModel model);
        /// <summary>
        /// 获得调度器
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task<ScheduleDTO?> GetScheduleAsync(Guid scheduleID);
        /// <summary>
        /// 获得调度器列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<(List<ScheduleDTO> data, PageModel pageInfo)> GetScheduleListAsync(QueryScheduleManagerModel model);
        /// <summary>
        /// 获得调度器
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task<ScheduleWorkDTO?> GetScheduleWorkAsync(Guid scheduleID);
        /// <summary>
        /// 获得调度器任务列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<(List<ScheduleWorkDTO> data, PageModel pageInfo)> GetScheduleWorkListAsync(QueryScheduleWorkManagerModel model);
        /// <summary>
        /// 获得任务
        /// </summary>
        /// <param name="workID"></param>
        /// <returns></returns>
        public Task<WorkDTO?> GetWorkAsync(Guid workID);
        /// <summary>
        /// 获得任务事件
        /// </summary>
        /// <param name="workEventID"></param>
        /// <returns></returns>
        public Task<WorkEventDTO?> GetWorkEventAsync(Guid workEventID);
        /// <summary>
        /// 获得任务事件列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<(List<WorkEventDTO> data, PageModel pageInfo)> GetWorkEventListAsync(QueryWorkEventManagerModel model);
        /// <summary>
        /// 获得任务列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<(List<WorkDTO> data, PageModel pageInfo)> GetWorkListAsync(QueryWorkModel model);
        /// <summary>
        /// 运行中
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task<bool> IsRuningAsync(Guid scheduleID);
        /// <summary>
        /// 运行中
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public Task<bool> IsRuningAsync(Schedule schedule);
        /// <summary>
        /// 立即启动调度器
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public Task RunNowAsync(Guid scheduleID);
        /// <summary>
        /// 启动所有调度器
        /// </summary>
        /// <returns></returns>
        public Task StartAllAsync();
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public Task StartAsync(params Guid[] scheduleIDs);
        /// <summary>
        /// 停止所有调度器
        /// </summary>
        /// <returns></returns>
        public Task StopAllAsync();
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        public Task StopAsync(params Guid[] scheduleIDs);
        /// <summary>
        /// 创建一个Oscillator构建器
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public IOscillatorBuild CreateOscillatorBuild(string name, string? description = null);
        /// <summary>
        /// 创建一个Oscillator构建器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IOscillatorBuild CreateOscillatorBuild(ScheduleModel model);
    }
}
