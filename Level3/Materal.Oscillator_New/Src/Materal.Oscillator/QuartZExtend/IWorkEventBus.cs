using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.QuartZExtend
{
    /// <summary>
    /// 任务事件总线
    /// </summary>
    public interface IWorkEventBus
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="eventHandler"></param>
        public void Subscribe(string eventValue, Action<string, ScheduleWork> eventHandler);
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="eventHandler"></param>
        public void Subscribe(string eventValue, Func<string, ScheduleWork, Task> eventHandler);
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        public void SendEvent(string eventValue, ScheduleWork scheduleWork);
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        public Task SendEventAsync(string eventValue, ScheduleWork scheduleWork);
    }
}
