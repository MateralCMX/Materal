using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works
{
    public class DelayedWork : WorkBase, IWork
    {
        /// <summary>
        /// 延时毫秒数
        /// </summary>
        public int MillisecondsDelay { get; set; }

        public DelayedWork() { }

        public DelayedWork(int millisecondsDelay = 100)
        {
            MillisecondsDelay = millisecondsDelay;
        }

        public override async Task<string?> ExcuteAsync(List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWorkView scheduleWork)
        {
            await Task.Delay(MillisecondsDelay);
            return null;
        }
    }
}
