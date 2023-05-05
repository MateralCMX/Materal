using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Materal.Utils.Model;
using System.Linq.Expressions;

namespace Materal.Oscillator.Abstractions
{
    /// <summary>
    /// 调度器主机
    /// </summary>
    public interface IOscillatorHost
    {
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task StartAsync(Expression<Func<Schedule, bool>> expression);
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="territory"></param>
        /// <returns></returns>
        Task StartAsync(string? territory = null);
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        Task StartAsync(params Guid[] scheduleIDs);
        /// <summary>
        /// 启动调度器
        /// </summary>
        /// <param name="schedules"></param>
        /// <returns></returns>
        Task StartAsync(params Schedule[] schedules);
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task StopAsync(Expression<Func<Schedule, bool>> expression);
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="territory"></param>
        /// <returns></returns>
        Task StopAsync(string? territory = null);
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        Task StopAsync(params Guid[] scheduleIDs);
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="schedules"></param>
        /// <returns></returns>
        Task StopAsync(params Schedule[] schedules);
        /// <summary>
        /// 立刻执行调度器
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task RunNowAsync(Expression<Func<Schedule, bool>> expression);
        /// <summary>
        /// 立刻执行调度器
        /// </summary>
        /// <param name="scheduleIDs"></param>
        /// <returns></returns>
        Task RunNowAsync(params Guid[] scheduleIDs);
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddWorkAsync(AddWorkModel model);
        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditWorkAsync(EditWorkModel model);
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="workID"></param>
        /// <returns></returns>
        Task DeleteWorkAsync(Guid workID);
        /// <summary>
        /// 获得任务信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<WorkDTO> GetWorkInfoAsync(Guid id);
        /// <summary>
        /// 获得任务列表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(List<WorkDTO> data, PageModel pageInfo)> GetWorkListAsync(QueryWorkModel model);
        /// <summary>
        /// 添加调度器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Guid> AddScheduleAsync(AddScheduleModel model);
        /// <summary>
        /// 修改调度器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task EditScheduleAsync(EditScheduleModel model);
        /// <summary>
        /// 获得调度器信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ScheduleDTO> GetScheduleInfoAsync(Guid id);
        /// <summary>
        /// 获得调度器列表信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<(List<ScheduleDTO> data, PageModel pageInfo)> GetScheduleListAsync(QueryScheduleModel model);
        /// <summary>
        /// 删除调度器
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        Task DeleteScheduleAsync(Guid scheduleID);
    }
}
