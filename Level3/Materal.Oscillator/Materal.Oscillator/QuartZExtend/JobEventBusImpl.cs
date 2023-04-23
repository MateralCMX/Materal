using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.QuartZExtend
{
    public class WorkEventBusImpl : IWorkEventBus
    {
        private readonly Dictionary<string, Action<string, ScheduleWorkView>> eventAction = new();
        private readonly Dictionary<string, Func<string, ScheduleWorkView, Task>> eventTask = new();
        public void SendEvent(string eventValue, ScheduleWorkView scheduleWork)
        {
            if (!eventAction.ContainsKey(eventValue)) return;
            eventAction[eventValue]?.Invoke(eventValue, scheduleWork);
        }

        public async Task SendEventAsync(string eventValue, ScheduleWorkView scheduleWork)
        {
            if (!eventTask.ContainsKey(eventValue)) return;
            await eventTask[eventValue].Invoke(eventValue, scheduleWork);
        }

        public void Subscribe(string eventValue, Action<string, ScheduleWorkView> eventHandler)
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

        public void Subscribe(string eventValue, Func<string, ScheduleWorkView, Task> eventHandler)
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
