using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.QuartZExtend
{
    public interface IWorkEventBus
    {
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="eventHandler"></param>
        public void Subscribe(string eventValue, Action<string, ScheduleWorkView> eventHandler);
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="eventHandler"></param>
        public void Subscribe(string eventValue, Func<string, ScheduleWorkView, Task> eventHandler);
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        public void SendEvent(string eventValue, ScheduleWorkView scheduleWork);
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        public Task SendEventAsync(string eventValue, ScheduleWorkView scheduleWork);
    }
}
