using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Materal.BusinessFlow.WebAPIControllers.Controllers
{
    /// <summary>
    /// 流程控制器
    /// </summary>
    public class FlowController : BusinessFlowControllerBase
    {
        private readonly IBusinessFlowHost _host;
        /// <summary>
        /// 构造方法
        /// </summary>
        public FlowController(IBusinessFlowHost host)
        {
            _host = host;
        }
        /// <summary>
        /// 启动一个新的流程
        /// </summary>
        /// <param name="userID">用户唯一标识</param>
        /// <param name="flowTemplateID">流程模版唯一标识</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> StartNewFlowAsync([Required] Guid userID, [Required] Guid flowTemplateID)
        {
            Guid flowID = await _host.StartNewFlowAsync(flowTemplateID, userID);
            return ResultModel<Guid>.Success(flowID, "查询成功");
        }
        /// <summary>
        /// 获得用户参与流程模版列表
        /// </summary>
        /// <param name="userID">用户唯一标识</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<FlowTemplate>>> GetUserFlowTemplatesAsync([Required]Guid userID)
        {
            List<FlowTemplate> flowTemplates = await _host.GetUserFlowTemplatesAsync(userID);
            return ResultModel<List<FlowTemplate>>.Success(flowTemplates, "查询成功");
        }
        /// <summary>
        /// 获得待办事项
        /// </summary>
        /// <param name="userID">用户唯一标识</param>
        /// <param name="flowTemplateID">流程模版唯一标识</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<FlowRecordDTO>>> GetBacklogAsync([Required] Guid userID, Guid? flowTemplateID)
        {
            List<FlowRecordDTO> flowRecords;
            if (flowTemplateID.HasValue)
            {
                flowRecords = await _host.GetBacklogByUserIDAsync(flowTemplateID.Value, userID);
            }
            else
            {
                flowRecords = await _host.GetBacklogByUserIDAsync(userID);
            }
            List<FlowTemplate> flowTemplates = await _host.GetUserFlowTemplatesAsync(userID);
            return ResultModel<List<FlowRecordDTO>>.Success(flowRecords, "查询成功");
        }
    }
}
