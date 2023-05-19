using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.WebAPIControllers.Models;
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
        public async Task<ResultModel<List<FlowTemplateDTO>>> GetUserFlowTemplatesAsync([Required] Guid userID)
        {
            List<FlowTemplateDTO> flowTemplates = await _host.GetUserFlowTemplatesAsync(userID);
            return ResultModel<List<FlowTemplateDTO>>.Success(flowTemplates, "查询成功");
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
            return ResultModel<List<FlowRecordDTO>>.Success(flowRecords, "查询成功");
        }
        /// <summary>
        /// 根据流程记录唯一标识获得流程数据
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<Dictionary<string, object?>>> GetFlowDatasByFlowRecordIDAsync(Guid flowTemplateID, Guid flowRecordID)
        {
            Dictionary<string, object?> result = await _host.GetFlowDatasByFlowRecordIDAsync(flowTemplateID, flowRecordID);
            return ResultModel<Dictionary<string, object?>>.Success(result, "查询成功");
        }
        /// <summary>
        /// 根据流程唯一标识获得流程数据
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<Dictionary<string, object?>>> GetFlowDatasAsync(Guid flowTemplateID, Guid flowID)
        {
            Dictionary<string, object?> result = await _host.GetFlowDatasAsync(flowTemplateID, flowID);
            return ResultModel<Dictionary<string, object?>>.Success(result, "查询成功");
        }
        /// <summary>
        /// 完成节点
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> ComplateFlowNodeAsync(OperationFlowRequestModel requestModel)
        {
            await _host.ComplateFlowNodeAsync(requestModel.FlowTemplateID, requestModel.FlowRecordID, requestModel.UserID, requestModel.JsonData);
            return ResultModel.Success("提交成功");
        }
        /// <summary>
        /// 打回节点
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> RepulseFlowNodeAsync(OperationFlowRequestModel requestModel)
        {
            await _host.RepulseFlowNodeAsync(requestModel.FlowTemplateID, requestModel.FlowRecordID, requestModel.UserID, requestModel.JsonData);
            return ResultModel.Success("打回成功");
        }
        /// <summary>
        /// 保存流程数据
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> SaveFlowDataAsync(SaveFlowDataRequestModel requestModel)
        {
            await _host.SaveFlowDataAsync(requestModel.FlowTemplateID, requestModel.FlowRecordID, requestModel.JsonData);
            return ResultModel.Success("保存成功");
        }
    }
}
