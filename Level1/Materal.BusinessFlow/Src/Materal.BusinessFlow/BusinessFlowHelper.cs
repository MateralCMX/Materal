using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Enums;
using Materal.BusinessFlow.Abstractions.Expressions;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Materal.BusinessFlow
{
    public class BusinessFlowHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataModelFieldRepository _dataFieldRepository;
        private readonly IFlowTemplateRepository _flowTemplateRepository;
        private readonly IStepRepository _stepRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly IFlowRepository _flowRepository;
        private readonly IFlowRecordRepository _flowRecordRepository;
        private readonly IFlowUserRepository _flowUserRepository;
        public BusinessFlowHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dataFieldRepository = unitOfWork.GetRepository<IDataModelFieldRepository>();
            _flowTemplateRepository = unitOfWork.GetRepository<IFlowTemplateRepository>();
            _stepRepository = unitOfWork.GetRepository<IStepRepository>();
            _nodeRepository = unitOfWork.GetRepository<INodeRepository>();
            _flowRepository = unitOfWork.GetRepository<IFlowRepository>();
            _flowRecordRepository = unitOfWork.GetRepository<IFlowRecordRepository>();
            _flowUserRepository = unitOfWork.GetRepository<IFlowUserRepository>();
        }
        /// <summary>
        /// 完成节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="userID"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public async Task<List<Guid>> ComplateNodeAsync(Guid flowTemplateID, Guid flowRecordID, Guid? userID, string? jsonData, bool saveFlowData = true)
        {
            List<Guid> autoNodeIDs = new();
            (List<FlowRecord> flowRecords, FlowRecord flowRecord, Dictionary<string, object?> changeDatas) = await EditSameBatchFlowRecordAsync(flowTemplateID, flowRecordID, userID, jsonData, FlowRecordStateEnum.Success);
            if (saveFlowData && jsonData != null && !string.IsNullOrWhiteSpace(jsonData))
            {
                changeDatas = await SaveFlowDataAsync(flowTemplateID, flowRecord, jsonData);
            }
            if (flowRecords.Any(m => m.State != FlowRecordStateEnum.Success)) return autoNodeIDs;
            Step step = await _stepRepository.FirstAsync(flowRecord.StepID);
            Guid initiatorID = await _flowRepository.GetInitiatorIDAsync(flowTemplateID, flowRecord.FlowID);
            if (step.NextID != null)//有下一步
            {
                (autoNodeIDs, bool hasNode) = await StartStepAsync(flowTemplateID, flowRecord.FlowID, step.NextID.Value, initiatorID, changeDatas);
                if (!hasNode)//所有Node均不符合执行条件
                {
                    _flowRepository.ComplateFlow(flowTemplateID, flowRecord.FlowID);
                }
            }
            else//无下一步
            {
                _flowRepository.ComplateFlow(flowTemplateID, flowRecord.FlowID);
            }
            return autoNodeIDs;
        }
        /// <summary>
        /// 完成自动节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<Guid>> ComplateAutoNodeAsync(Guid flowTemplateID, Guid flowRecordID, string? jsonData)
        {
            return await ComplateNodeAsync(flowTemplateID, flowRecordID, null, jsonData, false);
        }
        /// <summary>
        /// 修改同一批次的流程记录
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="userID"></param>
        /// <param name="jsonData"></param>
        /// <param name="flowRecordState"></param>
        /// <param name="resultMessage"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        public async Task<(List<FlowRecord> flowRecords, FlowRecord flowRecord, Dictionary<string, object?> changeDatas)> EditSameBatchFlowRecordAsync(Guid flowTemplateID, Guid flowRecordID, Guid? userID, FlowRecordStateEnum flowRecordState, string? resultMessage = null)
        {
            return await EditSameBatchFlowRecordAsync(flowTemplateID, flowRecordID, userID, null, flowRecordState, resultMessage);
        }
        /// <summary>
        /// 修改同一批次的流程记录
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="userID"></param>
        /// <param name="jsonData"></param>
        /// <param name="flowRecordState"></param>
        /// <param name="resultMessage"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        public async Task<(List<FlowRecord> flowRecords, FlowRecord flowRecord, Dictionary<string, object?> changeDatas)> EditSameBatchFlowRecordAsync(Guid flowTemplateID, Guid flowRecordID, Guid? userID, string? jsonData, FlowRecordStateEnum flowRecordState, string? resultMessage = null)
        {
            Dictionary<string, object?> changeDatas = new();
            FlowRecord flowRecord = await _flowRecordRepository.FirstAsync(flowTemplateID, flowRecordID);
            if (flowRecord.State != FlowRecordStateEnum.Wait && flowRecord.State != FlowRecordStateEnum.Fail) throw new BusinessFlowException("该节点已操作，不能修改数据");
            List<FlowRecord> flowRecords = await _flowRecordRepository.GetListAsync(new QueryFlowRecordModel
            {
                FlowTemplateID = flowTemplateID,
                FlowID = flowRecord.FlowID,
                StepID = flowRecord.StepID,
                SortIndex = flowRecord.SortIndex
            });
            foreach (FlowRecord item in flowRecords.Where(m => m.NodeID == flowRecord.NodeID))
            {
                item.State = flowRecordState;
                if (item.ID == flowRecord.ID)
                {
                    item.OperationUserID = userID;
                    item.Data = jsonData;
                    item.ResultMessage = resultMessage;
                }
                _flowRecordRepository.Edit(flowTemplateID, item);
                if(item.UserID != null)
                {
                    await EditFlowUserAsync(flowTemplateID, flowRecord.FlowID, item.ID, item.UserID.Value, item.State);
                }
            }
            return (flowRecords, flowRecord, changeDatas);
        }
        /// <summary>
        /// 启动步骤
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public async Task<(List<Guid> autoNodeIDs, bool hasNode)> StartStepAsync(Guid flowTemplateID, Guid flowID, Guid stepID, Guid initiatorID, Dictionary<string, object?> changeDatas)
        {
            _flowRepository.SetStep(flowTemplateID, flowID, stepID);
            List<Node> allNodes = await _nodeRepository.GetListAsync(new QueryNodeModel
            {
                StepID = stepID
            });
            int index = await _flowRecordRepository.GetMaxSortIndexAsync(flowTemplateID, flowID);
            List<Guid> autoNodeIDs = new();
            int addNodeCount = 0;
            FlowTemplate flowTemplate = await _flowTemplateRepository.FirstAsync(flowTemplateID);
            List<DataModelField> dataModelFields = await _dataFieldRepository.GetListAsync(m => m.DataModelID == flowTemplate.DataModelID);
            Dictionary<string, object?> datas = await _flowRepository.GetDataAsync(flowTemplateID, flowID, dataModelFields);
            foreach (KeyValuePair<string, object?> item in changeDatas)
            {
                if (datas.ContainsKey(item.Key))
                {
                    datas[item.Key] = item.Value;
                }
                else
                {
                    datas.Add(item.Key, item.Value);
                }
            }
            foreach (Node node in allNodes)
            {
                if (!NodeCanAdd(node, datas)) continue;
                Guid id = await AddFlowRecordsByNodeAsync(flowTemplateID, flowID, node, index, initiatorID);
                addNodeCount++;
                if (node.HandleType == NodeHandleTypeEnum.Auto)
                {
                    autoNodeIDs.Add(id);
                }
            }
            return (autoNodeIDs, addNodeCount > 0);
        }
        /// <summary>
        /// 无视条件启动步骤
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <param name="stepID"></param>
        /// <returns></returns>
        public async Task<List<Guid>> DisregardConditionsStartStepAsync(Guid flowTemplateID, Guid flowID, Guid stepID, Guid initiatorID)
        {
            _flowRepository.SetStep(flowTemplateID, flowID, stepID);
            List<Node> allNodes = await _nodeRepository.GetListAsync(new QueryNodeModel
            {
                StepID = stepID
            });
            int index = await _flowRecordRepository.GetMaxSortIndexAsync(flowTemplateID, flowID);
            List<Guid> autoNodeIDs = new();
            foreach (Node node in allNodes)
            {
                Guid id = await AddFlowRecordsByNodeAsync(flowTemplateID, flowID, node, index, initiatorID);
                if (node.HandleType == NodeHandleTypeEnum.Auto)
                {
                    autoNodeIDs.Add(id);
                }
            }
            return autoNodeIDs;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecord"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, object?>> SaveFlowDataAsync(Guid flowTemplateID, FlowRecord flowRecord, string jsonData)
        {
            FlowTemplate flowTemplate = await _flowTemplateRepository.FirstAsync(flowTemplateID);
            List<DataModelField> dataModelFields = await _dataFieldRepository.GetListAsync(m => m.DataModelID == flowTemplate.DataModelID);
            JObject jsonObj = JsonConvert.DeserializeObject<JObject>(jsonData) ?? throw new BusinessFlowException("Json反序列化失败");
            Dictionary<string, object?> changeDatas = new();
            foreach (KeyValuePair<string, JToken?> item in jsonObj)
            {
                if (item.Value == null) continue;
                DataModelField dataModelField = dataModelFields.FirstOrDefault(m => m.Name == item.Key);
                object? value = GetJsonValue(dataModelField, item.Value);
                changeDatas.Add(item.Key, value);
            }
            _flowRepository.SaveData(flowTemplateID, flowRecord.FlowID, changeDatas);
            return changeDatas;
        }
        /// <summary>
        /// 获得Json值
        /// </summary>
        /// <param name="dataModelField"></param>
        /// <param name="jToken"></param>
        /// <returns></returns>
        private object? GetJsonValue(DataModelField? dataModelField, JToken jToken)
        {
            if (dataModelField == null) return null;
            if (dataModelField.DataType == DataTypeEnum.String && jToken.Type == JTokenType.String) return jToken.ToString();
            else if (dataModelField.DataType == DataTypeEnum.Number && jToken.Type == JTokenType.Integer || jToken.Type == JTokenType.Float) return Convert.ToDecimal(jToken);
            else if (dataModelField.DataType == DataTypeEnum.DateTime && jToken.Type == JTokenType.String) return Convert.ToDateTime(jToken);
            else if (dataModelField.DataType == DataTypeEnum.Date && jToken.Type == JTokenType.Date) return Convert.ToDateTime(jToken);
            else if (dataModelField.DataType == DataTypeEnum.Time && jToken.Type == JTokenType.Date) return Convert.ToDateTime(jToken);
            else if (dataModelField.DataType == DataTypeEnum.Boole && jToken.Type == JTokenType.Boolean) return Convert.ToBoolean(jToken);
            else if (dataModelField.DataType == DataTypeEnum.Enum && jToken.Type == JTokenType.String) return jToken.ToString();
            else return null;
        }
        /// <summary>
        /// 根据节点添加流程记录
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <param name="node"></param>
        /// <param name="index"></param>
        /// <exception cref="BusinessFlowException"></exception>
        private async Task<Guid> AddFlowRecordsByNodeAsync(Guid flowTemplateID, Guid flowID, Node node, int index, Guid initiatorID)
        {
            FlowRecord flowRecord = new()
            {
                FlowID = flowID,
                StepID = node.StepID,
                NodeID = node.ID,
                SortIndex = index,
                NodeHandleType = node.HandleType,
                UserID = null,
            };
            switch (node.HandleType)
            {
                case NodeHandleTypeEnum.Auto:
                    return _flowRecordRepository.Add(flowTemplateID, flowRecord);
                case NodeHandleTypeEnum.User:
                    if (node.HandleData == null || string.IsNullOrWhiteSpace(node.HandleData)) throw new BusinessFlowException("未指定处理用户");
                    if (!node.HandleData.IsGuid()) throw new BusinessFlowException("处理数据格式错误");
                    flowRecord.UserID = Guid.Parse(node.HandleData);
                    await EditFlowUserAsync(flowTemplateID, flowRecord.FlowID, flowRecord.ID, flowRecord.UserID.Value, flowRecord.State);
                    return _flowRecordRepository.Add(flowTemplateID, flowRecord);
                case NodeHandleTypeEnum.Initiator:
                    flowRecord.UserID = initiatorID;
                    await EditFlowUserAsync(flowTemplateID, flowRecord.FlowID, flowRecord.ID, flowRecord.UserID.Value, flowRecord.State);
                    return _flowRecordRepository.Add(flowTemplateID, flowRecord);
                default:
                    throw new BusinessFlowException("未知处理类型");
            }
        }
        /// <summary>
        /// 修改流程用户映射
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowID"></param>
        /// <param name="flowRecordID"></param>
        /// <param name="userID"></param>
        /// <param name="flowRecordState"></param>
        private async Task EditFlowUserAsync(Guid flowTemplateID, Guid flowID, Guid flowRecordID, Guid userID, FlowRecordStateEnum flowRecordState)
        {
            switch (flowRecordState)
            {
                case FlowRecordStateEnum.Wait://新增
                    _unitOfWork.RegisterAdd(new FlowUser
                    {
                        FlowTemplateID = flowTemplateID,
                        FlowID = flowID, 
                        FlowRecordID = flowRecordID, 
                        UserID = userID
                    });
                    break;
                default://移除
                    FlowUser? flowuser = await _flowUserRepository.FirstOrDefaultAsync(m => m.FlowTemplateID == flowTemplateID && m.FlowID == flowID && m.FlowRecordID == flowRecordID && m.UserID == userID);
                    if (flowuser == null) break;
                    _unitOfWork.RegisterDelete(flowuser);
                    break;
            }
        }
        /// <summary>
        /// 节点是否添加
        /// </summary>
        /// <param name="node"></param>
        /// <param name="fieldDatas"></param>
        /// <returns></returns>
        private bool NodeCanAdd(Node node, Dictionary<string, object?> fieldDatas)
        {
            if (node.RunConditionExpression == null || string.IsNullOrWhiteSpace(node.RunConditionExpression)) return true;
            ConditionExpression conditionExpression = ConditionExpression.Parse(node.RunConditionExpression);
            object? valueObj = conditionExpression.GetValue(fieldDatas);
            if (valueObj == null || valueObj is not bool result) return false;
            return result;
        }
    }
}
