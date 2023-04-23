using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.Works
{
    public abstract class WorkBase : IWork
    {
        public virtual IWork Deserialization(string workData) => (IWork)workData.JsonToObject(GetType());
        public virtual IWork Deserialization(Work work) => Deserialization(work.WorkData);
        public abstract Task<string?> ExcuteAsync(List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWorkView scheduleWork);
        public virtual Task InitAsync() => Task.CompletedTask;
    }
}
