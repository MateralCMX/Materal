using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Works;

namespace Materal.Oscillator.Works
{
    /// <summary>
    /// 控制台任务
    /// </summary>
    public class ConsoleWork : BaseWork<ConsoleWorkData>, IWork<ConsoleWorkData>
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
        public override Task<string?> ExcuteAsync(ConsoleWorkData workData, List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work)
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine(workData.Message);
            Console.WriteLine("--------------------------------------------------------");
            return Task.FromResult((string?)null);
        }
    }
}
