using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.DR.Domain;

namespace Materal.Oscillator.DR
{
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
        public Task WorkExecuteAsync(Schedule schedule, ScheduleWorkView scheduleWork);
        /// <summary>
        /// 任务执行完毕
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="workResult"></param>
        /// <returns></returns>
        public Task WorkExecutedAsync(Schedule schedule, ScheduleWorkView scheduleWork, string? workResult);
        /// <summary>
        /// 任务执行
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public Task WorkExecuteAsync(Flow flow, ScheduleWorkView scheduleWork);
        /// <summary>
        /// 任务执行完毕
        /// </summary>
        /// <param name="flow"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="workResult"></param>
        /// <returns></returns>
        public Task WorkExecutedAsync(Flow flow, ScheduleWorkView scheduleWork, string? workResult);
        /// <summary>
        /// 调度器启动
        /// </summary>
        /// <returns></returns>
        public Task ScheduleStartAsync();
    }
}
