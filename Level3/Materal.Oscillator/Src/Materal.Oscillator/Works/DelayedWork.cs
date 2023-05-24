using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works
{
    /// <summary>
    /// 延时任务
    /// </summary>
    public class DelayedWork : BaseWork<DelayedWorkData>, IWork<DelayedWorkData>
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="jobResults"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public override async Task<string?> ExcuteAsync(DelayedWorkData workData, List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work)
        {
            await Task.Delay(workData.MillisecondsDelay);
            return null;
        }
    }
}
