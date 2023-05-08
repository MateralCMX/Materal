using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.Abstractions.Works
{
    /// <summary>
    /// 无任务
    /// </summary>
    public class NoneWork : WorkBase, IWork
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="jobResults"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public override Task<string?> ExcuteAsync(List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work)
        {
            return Task.FromResult((string?)null);
        }
    }
}
