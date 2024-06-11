using Materal.MergeBlock.Abstractions.Oscillator;
using Materal.MergeBlock.Application.WebModule.Controllers;
using Materal.MergeBlock.OscillatorTest.Oscillator;
using Materal.Oscillator.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.OscillatorTest.Controllers
{
    /// <summary>
    /// Oscillator测试控制器
    /// </summary>
    public class OscillatorTestController(IOscillatorHost oscillatorHost, IServiceProvider serviceProvider) : MergeBlockControllerBase
    {
        /// <summary>
        /// 初始化任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task InitTestWorkAsync() => await oscillatorHost.InitNowWorkAsync<TestWorkData>(serviceProvider);
        /// <summary>
        /// 立即运行任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task RunNowTestWorkAsync() => await oscillatorHost.RunNowWorkAsync<TestWorkData>(serviceProvider);
    }
}
