using Materal.Extensions;
using Materal.MergeBlock.Abstractions.Oscillator;
using Materal.MergeBlock.Application.WebModule.Controllers;
using Materal.MergeBlock.OscillatorTest.Oscillator;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Works;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.OscillatorTest.Controllers
{
    /// <summary>
    /// Oscillator测试控制器
    /// </summary>
    public class OscillatorTestController(IOscillatorHost oscillatorHost) : MergeBlockControllerBase
    {
        /// <summary>
        /// 初始化任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task InitTestWorkAsync()
        {
            IWorkData testWorkData = typeof(TestWorkData).InstantiationOrDefault<IWorkData>() ?? throw new OscillatorException("任务数据初始化失败");
            OscillatorInitManager.AddInitKey(testWorkData);
            await oscillatorHost.RunNowWorkDataAsync(testWorkData);
        }

        /// <summary>
        /// 立即运行任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task RunNowTestWorkAsync() => await oscillatorHost.RunNowWorkDataAsync<TestWorkData>();
    }
}
