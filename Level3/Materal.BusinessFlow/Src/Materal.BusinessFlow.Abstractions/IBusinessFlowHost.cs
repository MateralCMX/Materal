using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;

namespace Materal.BusinessFlow.Abstractions
{
    public interface IBusinessFlowHost
    {
        /// <summary>
        /// 启动一个新的流程
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="initiatorID"></param>
        /// <returns></returns>
        Task<Guid> StartNewFlowAsync(Guid flowTemplateID, Guid initiatorID);
        /// <summary>
        /// 根据用户唯一标识获得待办事项
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<FlowRecordDTO>> GetBacklogByUserIDAsync(Guid flowTemplateID, Guid userID);
        /// <summary>
        /// 根据用户唯一标识获得待办事项
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<FlowRecordDTO>> GetBacklogByUserIDAsync(Guid userID);
        /// <summary>
        /// 获得用户参与流程模版列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<FlowTemplateDTO>> GetUserFlowTemplatesAsync(Guid userID);
        /// <summary>
        /// 完成节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="userID"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        Task ComplateFlowNodeAsync(Guid flowTemplateID, Guid flowRecordID, Guid userID, string jsonData);
        /// <summary>
        /// 打回节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="userID"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        Task RepulseFlowNodeAsync(Guid flowTemplateID, Guid flowRecordID, Guid userID, string jsonData);
        /// <summary>
        /// 保存流程数据
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        Task SaveFlowDataAsync(Guid flowTemplateID, Guid flowRecordID, string jsonData);
        /// <summary>
        /// 运行自动节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="runErrorNode"></param>
        /// <returns></returns>
        Task RunAutoNodeAsync(Guid flowTemplateID, bool runErrorNode = true);
        /// <summary>
        /// 运行所有自动节点
        /// </summary>
        /// <param name="runErrorNode"></param>
        /// <returns></returns>
        Task RunAllAutoNodeAsync(bool runErrorNode = true);
        /// <summary>
        /// 根据流程记录唯一标识获得流程数据
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <returns></returns>
        Task<Dictionary<string, object?>> GetFlowDatasByFlowRecordIDAsync(Guid flowTemplateID, Guid flowRecordID);
        /// <summary>
        /// 根据流程唯一标识获得流程数据
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <returns></returns>
        Task<Dictionary<string, object?>> GetFlowDatasAsync(Guid flowTemplateID, Guid flowID);
    }
}
