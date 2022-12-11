using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.Works
{
    public class NoneWork : WorkBase, IWork
    {
        public override Task<string?> ExcuteAsync(List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWorkView scheduleWork)
        {
            return Task.FromResult((string?)null);
        }
    }
}
