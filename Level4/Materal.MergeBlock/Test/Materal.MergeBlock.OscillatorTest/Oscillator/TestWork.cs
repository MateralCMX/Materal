using Materal.MergeBlock.Abstractions.Oscillator;
using Materal.Oscillator.Abstractions.Works;
using Microsoft.Extensions.Logging;

namespace Materal.MergeBlock.OscillatorTest.Oscillator
{
    /// <summary>
    /// 测试任务
    /// </summary>
    public partial class TestWork(ILogger<TestWork> logger) : MergeBlockWork<TestWorkData>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="workContext"></param>
        /// <returns></returns>
        protected override async Task InitAsync(IWorkContext workContext)
        {
            logger.LogInformation($"--------------------------------\r\n测试作业-初始化-{Data.Message}\r\n--------------------------------");
            await base.InitAsync(workContext);
        }
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="workContext"></param>
        /// <returns></returns>
        protected override async Task WorkExecuteAsync(IWorkContext workContext)
        {
            logger.LogInformation($"--------------------------------\r\n测试作业-{Data.Message}\r\n--------------------------------");
            await Task.CompletedTask;
        }
    }
}
