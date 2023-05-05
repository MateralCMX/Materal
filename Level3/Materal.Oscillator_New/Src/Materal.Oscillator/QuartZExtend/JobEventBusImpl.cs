using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.QuartZExtend
{
    /// <summary>
    /// 任务事件总线
    /// </summary>
    public class WorkEventBusImpl : IWorkEventBus
    {
        private readonly Dictionary<string, Action<string, ScheduleWork>> eventAction = new();
        private readonly Dictionary<string, Func<string, ScheduleWork, Task>> eventTask = new();
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        public void SendEvent(string eventValue, ScheduleWork scheduleWork)
        {
            if (!eventAction.ContainsKey(eventValue)) return;
            eventAction[eventValue]?.Invoke(eventValue, scheduleWork);
        }
        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="scheduleWork"></param>
        /// <returns></returns>
        public async Task SendEventAsync(string eventValue, ScheduleWork scheduleWork)
        {
            if (!eventTask.ContainsKey(eventValue)) return;
            await eventTask[eventValue].Invoke(eventValue, scheduleWork);
        }
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="eventHandler"></param>
        public void Subscribe(string eventValue, Action<string, ScheduleWork> eventHandler)
        {
            if (eventAction.ContainsKey(eventValue))
            {
                eventAction[eventValue] = eventHandler;
            }
            else
            {
                eventAction.Add(eventValue, eventHandler);
            }
        }
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventValue"></param>
        /// <param name="eventHandler"></param>
        public void Subscribe(string eventValue, Func<string, ScheduleWork, Task> eventHandler)
        {
            if (eventTask.ContainsKey(eventValue))
            {
                eventTask[eventValue] = eventHandler;
            }
            else
            {
                eventTask.Add(eventValue, eventHandler);
            }
        }
    }
}
