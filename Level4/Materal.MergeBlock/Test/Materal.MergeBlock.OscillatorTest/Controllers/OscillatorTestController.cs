using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Application.WebModule.Controllers;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.OscillatorTest.Controllers
{
    /// <summary>
    /// Oscillator测试控制器
    /// </summary>
    public class OscillatorTestController(IOscillatorHost oscillatorHost) : MergeBlockControllerBase
    {
        /// <summary>
        /// 立即运行调度器
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MergeBlockModuleException"></exception>
        [HttpGet]
        public async Task RunNowScheduleAsync()
        {
            (List<ScheduleDTO> data, _) = await oscillatorHost.GetScheduleListAsync(new QueryScheduleModel
            {
                PageIndex = 1,
                PageSize = 1
            });
            if (data.Count <= 0) throw new MergeBlockModuleException("没有找到可用的定时任务");
            await oscillatorHost.RunNowAsync(data.First().ID);
        }
    }
}
