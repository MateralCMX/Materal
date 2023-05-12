using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.Step;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    public class StepController : BusinessFlowServiceControllerBase<Step, Step, IStepService, AddStepModel, EditStepModel, QueryStepModel>
    {
        public StepController(IServiceProvider service) : base(service)
        {
        }
        /// <summary>
        /// 根据流程模版唯一标识获取步骤列表信息
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<Step>>> GetListByFlowTemplateIDAsync([Required]Guid flowTemplateID)
        {
            List<Step> result = await DefaultService.GetListByFlowTemplateIDAsync(flowTemplateID);
            return ResultModel<List<Step>>.Success(result);
        }
    }
}