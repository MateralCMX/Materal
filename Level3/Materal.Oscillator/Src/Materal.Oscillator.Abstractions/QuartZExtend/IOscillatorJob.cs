using Materal.Oscillator.Abstractions.Domain;
using Quartz;

namespace Materal.Oscillator.Abstractions.QuartZExtend
{
    /// <summary>
    /// 调度器作业
    /// </summary>
    public interface IOscillatorJob : IJob
    {
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public Task SendEventAsync(string eventValue, ScheduleWork scheduleWork);
        /// <summary>
        /// 处理任务
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public Task<string> HandlerJobAsync(ScheduleWork scheduleWork);
    }
}
