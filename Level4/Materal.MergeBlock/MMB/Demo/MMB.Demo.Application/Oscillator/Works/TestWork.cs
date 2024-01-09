using Materal.MergeBlock.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Works;

namespace MMB.Demo.Application.Oscillator.Works
{
    ///// <summary>
    ///// 测试作业
    ///// </summary>
    //public partial class TestWork(ILogger<TestWork> logger) : MergeBlockWork<TestWorkData>
    //{
    //    /// <summary>
    //    /// 作业执行
    //    /// </summary>
    //    /// <param name="workData"></param>
    //    /// <param name="jobResults"></param>
    //    /// <param name="nowIndex"></param>
    //    /// <param name="schedule"></param>
    //    /// <param name="scheduleWork"></param>
    //    /// <param name="work"></param>
    //    /// <returns></returns>
    //    protected override async Task<string?> WorkExcuteAsync(TestWorkData workData, List<WorkResultModel> jobResults, int nowIndex, Schedule schedule, ScheduleWork scheduleWork, Work work)
    //    {
    //        logger.LogInformation("测试作业");
    //        await Task.CompletedTask;
    //        return null;
    //    }
    //}
    /// <summary>
    /// 测试作业
    /// </summary>
    public partial class TestWork() : MergeBlockWork<TestWorkData>
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
            await Task.CompletedTask;
            return null;
        }
    }
}
