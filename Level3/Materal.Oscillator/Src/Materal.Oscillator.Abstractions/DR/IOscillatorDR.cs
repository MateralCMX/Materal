using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.DR.Domain;

namespace Materal.Oscillator.Abstractions.DR
{
    /// <summary>
    /// 调度器容灾
    /// </summary>
    public interface IOscillatorDR
    {
        /// <summary>
        /// 调度器执行
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public Task ScheduleExecuteAsync(Schedule schedule);
        /// <summary>
        /// 调度器执行完毕
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public Task ScheduleExecutedAsync(Schedule schedule);
        /// <summary>
        /// 调度器执行
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public Task ScheduleExecuteAsync(Flow flow);
        /// <summary>
        /// 调度器执行完毕
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public Task ScheduleExecutedAsync(Flow flow);
        /// <summary>
        /// 任务执行
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public Task WorkExecuteAsync(Schedule schedule, ScheduleWork scheduleWork);
        /// <summary>
        /// 任务执行完毕
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="workResult"></param>
        /// <returns></returns>
        public Task WorkExecutedAsync(Schedule schedule, ScheduleWork scheduleWork, string? workResult);
        /// <summary>
        /// 任务执行
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public Task WorkExecuteAsync(Flow flow, ScheduleWork scheduleWork);
        /// <summary>
        /// 任务执行完毕
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="workResult"></param>
        /// <returns></returns>
        public Task WorkExecutedAsync(Flow flow, ScheduleWork scheduleWork, string? workResult);
        /// <summary>
        /// 调度器启动
        /// </summary>
        /// <returns></returns>
        public Task ScheduleStartAsync();
    }
}
