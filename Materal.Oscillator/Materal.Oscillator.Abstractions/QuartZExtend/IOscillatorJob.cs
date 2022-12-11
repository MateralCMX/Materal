using Materal.Oscillator.Abstractions.Domain;
using Quartz;

namespace Materal.Oscillator.Abstractions.QuartZExtend
{
    public interface IOscillatorJob : IJob
    {
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public Task SendEventAsync(string eventValue, ScheduleWorkView scheduleWork);
        /// <summary>
        /// 处理任务
        /// </summary>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public Task<string> HandlerJobAsync(ScheduleWorkView scheduleWork);
    }
}
