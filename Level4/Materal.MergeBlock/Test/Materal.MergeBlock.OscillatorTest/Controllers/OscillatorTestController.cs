using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Application.WebModule.Controllers;
using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.OscillatorTest.Controllers
{
    /// <summary>
    /// Oscillator���Կ�����
    /// </summary>
    public class OscillatorTestController(IOscillatorHost oscillatorHost) : MergeBlockControllerBase
    {
        [HttpGet]
        public async Task RunNowScheduleAsync()
        {
            (List<ScheduleDTO> data, _) = await oscillatorHost.GetScheduleListAsync(new QueryScheduleModel
            {
                PageIndex = 1,
                PageSize = 1
            });
            if(data.Count <= 0) throw new MergeBlockModuleException("û���ҵ����õĶ�ʱ����");
            await oscillatorHost.RunNowAsync(data.First().ID);
        }
    }
}
