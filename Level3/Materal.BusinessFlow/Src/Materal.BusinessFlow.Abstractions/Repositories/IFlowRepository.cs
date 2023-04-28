using Materal.BusinessFlow.Abstractions.Domain;

namespace Materal.BusinessFlow.Abstractions.Repositories
{
    public interface IFlowRepository : IBaseRepository
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="startStepID"></param>
        /// <param name="initiatorID"></param>
        /// <returns></returns>
        Guid Add(Guid flowTemplateID, Guid startStepID, Guid initiatorID);
        /// <summary>
        /// 完结流程
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <returns></returns>
        void ComplateFlow(Guid flowTemplateID, Guid flowID);
        /// <summary>
        /// 设置步骤
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        void SetStep(Guid flowTemplateID, Guid flowID, Guid stepID);
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <param name="datas"></param>
        void SaveData(Guid flowTemplateID, Guid flowID, Dictionary<string, object?> datas);
        /// <summary>
        /// 初始化流程
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <returns></returns>
        void Init(FlowTemplate flowTemplate);
        /// <summary>
        /// 添加表字段
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <param name="dataModelField"></param>
        void AddTableField(FlowTemplate flowTemplate, DataModelField dataModelField);
        /// <summary>
        /// 修改表字段
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <param name="oldDataModelField"></param>
        /// <param name="newDataModelField"></param>
        void EditTableField(FlowTemplate flowTemplate, DataModelField oldDataModelField, DataModelField newDataModelField);
        /// <summary>
        /// 删除表字段
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <param name="dataModelField"></param>
        void DeleteTableField(FlowTemplate flowTemplate, DataModelField dataModelField);
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="dataModelFields"></param>
        /// <returns></returns>
        Dictionary<string, object?> GetData(Guid flowTemplateID, Guid flowID, List<DataModelField> dataModelFields);
        /// <summary>
        /// 获得创建人唯一标识
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <returns></returns>
        Guid GetInitiatorID(Guid flowTemplateID, Guid flowID);
        /// <summary>
        /// 初始化流程
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <returns></returns>
        Task InitAsync(FlowTemplate flowTemplate);
        /// <summary>
        /// 添加表字段
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <param name="dataModelField"></param>
        /// <returns></returns>
        Task AddTableFieldAsync(FlowTemplate flowTemplate, DataModelField dataModelField);
        /// <summary>
        /// 修改表字段
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <param name="oldDataModelField"></param>
        /// <param name="newDataModelField"></param>
        /// <returns></returns>
        Task EditTableFieldAsync(FlowTemplate flowTemplate, DataModelField oldDataModelField, DataModelField newDataModelField);
        /// <summary>
        /// 删除表字段
        /// </summary>
        /// <param name="flowTemplate"></param>
        /// <param name="dataModelField"></param>
        /// <returns></returns>
        Task DeleteTableFieldAsync(FlowTemplate flowTemplate, DataModelField dataModelField);
        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="flowID"></param>
        /// <param name="dataModelFields"></param>
        /// <returns></returns>
        Task<Dictionary<string, object?>> GetDataAsync(Guid flowTemplateID, Guid flowID, List<DataModelField> dataModelFields);
        /// <summary>
        /// 获得创建人唯一标识
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <returns></returns>
        Task<Guid> GetInitiatorIDAsync(Guid flowTemplateID, Guid flowID);
    }
}
