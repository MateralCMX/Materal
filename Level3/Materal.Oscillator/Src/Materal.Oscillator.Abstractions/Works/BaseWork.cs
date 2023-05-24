using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 任务
    /// </summary>
    public abstract class BaseWork<T> : IWork<T>
        where T : IWorkData
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="jobResults"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public abstract Task<string?> ExcuteAsync(T workData, List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work);
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="workResult"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="OscillatorException"></exception>
        public Task<string?> ExcuteAsync(object workData, List<WorkResultModel> workResult, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work)
        {
            if (workData is not T tWorkData) throw new OscillatorException("数据类型错误");
            return ExcuteAsync(tWorkData, workResult, nowIndex, schedule, scheduleWork, work);
        }
    }
}
