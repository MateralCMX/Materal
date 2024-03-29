using Materal.MergeBlock.Abstractions.Oscillator;
using Materal.MergeBlock.OscillatorTest.WorkData;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Works;
using Microsoft.Extensions.Logging;

namespace Materal.MergeBlock.OscillatorTest.Works
{
    /// <summary>
    /// 测试作业
    /// </summary>
    public partial class TestWork(ILoggerFactory loggerFactory) : MergeBlockWork<TestWorkData>(loggerFactory)
    {
        /// <summary>
        /// 作业执行
        /// </summary>
        /// <param name="workData"></param>
        /// <param name="jobResults"></param>
        /// <param name="nowIndex"></param>
        /// <param name="schedule"></param>
        /// <param name="scheduleWork"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        protected override async Task<string?> WorkExcuteAsync(TestWorkData workData, List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work)
        {
            Logger?.LogInformation("--------------------------------\r\n测试作业\r\n--------------------------------");
            await Task.CompletedTask;
            return null;
        }
    }
}
