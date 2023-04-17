using Materal.BusinessFlow.Abstractions.Models;

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
        Task StartNewFlowAsync(Guid flowTemplateID, Guid initiatorID);
        /// <summary>
        /// 根据用户唯一标识获得待办事项
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<List<FlowRecordDTO>> GetBacklogByUserIDAsync(Guid flowTemplateID, Guid userID);
        /// <summary>
        /// 完成节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="userID"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        Task ComplateNodeAsync(Guid flowTemplateID, Guid flowRecordID, Guid userID, string jsonData);
        /// <summary>
        /// 打回节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="userID"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        Task RepulseNodeAsync(Guid flowTemplateID, Guid flowRecordID, Guid userID, string jsonData);
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
    }
}
