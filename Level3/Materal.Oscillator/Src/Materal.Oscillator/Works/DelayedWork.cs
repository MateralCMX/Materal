using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works
{
    /// <summary>
    /// 延时任务
    /// </summary>
    public class DelayedWork : WorkBase, IWork
    {
        /// <summary>
        /// 延时毫秒数
        /// </summary>
        public int MillisecondsDelay { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public DelayedWork() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="millisecondsDelay"></param>
        public DelayedWork(int millisecondsDelay = 100)
        {
            MillisecondsDelay = millisecondsDelay;
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="jobResults"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        public override async Task<string?> ExcuteAsync(List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work)
        {
            await Task.Delay(MillisecondsDelay);
            return null;
        }
    }
}
