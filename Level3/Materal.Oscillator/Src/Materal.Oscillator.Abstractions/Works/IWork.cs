using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 执行任务
    /// </summary>
    public interface IWork
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="workData">任务结果集</param>
        /// <param name="workResult">任务结果集</param>
        /// <param name="nowIndex">当前任务步骤数</param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public Task<string?> ExcuteAsync(object workData, List<WorkResultModel> workResult, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work);
    }
    /// <summary>
    /// 任务
    /// </summary>
    public interface IWork<T> : IWork
        where T : IWorkData
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="workData">任务结果集</param>
        /// <param name="workResult">任务结果集</param>
        /// <param name="nowIndex">当前任务步骤数</param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public Task<string?> ExcuteAsync(T workData, List<WorkResultModel> workResult, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work);
    }
}
