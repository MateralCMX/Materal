using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.AutoNodes;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Enums;
using Materal.BusinessFlow.Abstractions.Models;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services.Models;
using System.Linq.Expressions;

namespace Materal.BusinessFlow
{
    public class BusinessFlowHostImpl : IBusinessFlowHost
    {
        private readonly IAutoNodeBus _autoNodeBus;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFlowTemplateRepository _flowTemplateRepository;
        private readonly IStepRepository _stepRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFlowRepository _flowRepository;
        private readonly IFlowRecordRepository _flowRecordRepository;
        private readonly IFlowUserRepository _flowUserRepository;
        private readonly BusinessFlowHelper _businessFlowHelper;
        public BusinessFlowHostImpl(IAutoNodeBus autoNodeBus, IUnitOfWork unitOfWork, BusinessFlowHelper businessFlowHelper)
        {
            _autoNodeBus = autoNodeBus;
            _unitOfWork = unitOfWork;
            _flowTemplateRepository = unitOfWork.GetRepository<IFlowTemplateRepository>();
            _stepRepository = unitOfWork.GetRepository<IStepRepository>();
            _userRepository = unitOfWork.GetRepository<IUserRepository>();
            _flowRepository = unitOfWork.GetRepository<IFlowRepository>();
            _flowRecordRepository = unitOfWork.GetRepository<IFlowRecordRepository>();
            _flowUserRepository = unitOfWork.GetRepository<IFlowUserRepository>();
            _businessFlowHelper = businessFlowHelper;
        }
        public async Task<Guid> StartNewFlowAsync(Guid flowTemplateID, Guid initiatorID)
        {
            FlowTemplate flowTemplate = await _flowTemplateRepository.FirstAsync(flowTemplateID);
            Step step = await _stepRepository.FirstAsync(m => m.FlowTemplateID == flowTemplateID && m.UpID == null);
            await _flowRepository.InitAsync(flowTemplate);
            await _flowRecordRepository.InitAsync(flowTemplate.ID);
            Guid flowID = _flowRepository.Add(flowTemplateID, step.ID, initiatorID);
            List<Guid> autoNodeIDs = await _businessFlowHelper.DisregardConditionsStartStepAsync(flowTemplate.ID, flowID, step.ID, initiatorID);
            await _unitOfWork.CommitAsync();
            RunAutoNodes(flowTemplateID, autoNodeIDs);
            return flowID;
        }
        public async Task<List<FlowTemplate>> GetBacklogFlowTemplatesByUserIDAsync(Guid userID)
        {
            List<Guid> allFlowTemplateIDs = await GetBacklogFlowTemplateIDsByUserIDAsync(userID);
            List<FlowTemplate> result = await _flowTemplateRepository.GetListAsync(m => allFlowTemplateIDs.Contains(m.ID));
            return result;
        }
        public async Task<List<FlowRecordDTO>> GetBacklogByUserIDAsync(Guid userID)
        {
            List<Guid> allFlowTemplateIDs = await GetBacklogFlowTemplateIDsByUserIDAsync(userID);
            List<FlowRecordDTO> result = new();
            foreach (Guid flowTemplateID in allFlowTemplateIDs)
            {
                List<FlowRecordDTO> flowRecords = await GetBacklogByUserIDAsync(flowTemplateID, userID);
                result.AddRange(flowRecords);
            }
            return result;
        }
        public async Task<List<FlowRecordDTO>> GetBacklogByUserIDAsync(Guid flowTemplateID, Guid userID)
        {
            if (!await _userRepository.ExistingAsync(userID)) throw new BusinessFlowException("用户不存在");
            if (!await _flowTemplateRepository.ExistingAsync(flowTemplateID)) throw new BusinessFlowException("流程模版不存在");
            List<FlowRecordDTO> result = await _flowRecordRepository.GetDTOListAsync(new QueryFlowRecordDTOModel
            {
                FlowTemplateID = flowTemplateID,
                UserID = userID,
                State = FlowRecordStateEnum.Wait
            });
            return result;
        }
        public async Task RunAutoNodeAsync(Guid flowTemplateID, bool runErrorNode = true)
        {
            Expression<Func<FlowRecord, bool>> expression = m => m.NodeHandleType == NodeHandleTypeEnum.Auto;
            if (runErrorNode)
            {
                expression = expression.And(m => m.State == FlowRecordStateEnum.Wait || m.State == FlowRecordStateEnum.Fail);
            }
            else
            {
                expression = expression.And(m => m.State == FlowRecordStateEnum.Wait);
            }
            List<FlowRecord> flowRecords = await _flowRecordRepository.GetListAsync(flowTemplateID, expression);
            List<Guid> autoNodeIDs = flowRecords.Select(m => m.ID).ToList();
            RunAutoNodes(flowTemplateID, autoNodeIDs);
        }
        public async Task ComplateFlowNodeAsync(Guid flowTemplateID, Guid flowRecordID, Guid userID, string jsonData)
        {
            List<Guid> autoNodeIDs = await _businessFlowHelper.ComplateNodeAsync(flowTemplateID, flowRecordID, userID, jsonData);
            await _unitOfWork.CommitAsync();
            RunAutoNodes(flowTemplateID, autoNodeIDs);
        }
        public async Task RepulseFlowNodeAsync(Guid flowTemplateID, Guid flowRecordID, Guid userID, string jsonData)
        {
            (_, FlowRecord flowRecord, _) = await _businessFlowHelper.EditSameBatchFlowRecordAsync(flowTemplateID, flowRecordID, userID, jsonData, FlowRecordStateEnum.Repulse, null);
            Step step = await _stepRepository.FirstAsync(flowRecord.StepID);
            if (step.UpID == null) throw new BusinessFlowException("该节点已是起始步骤，无法打回");
            Guid initiatorID = await _flowRepository.GetInitiatorIDAsync(flowTemplateID, flowRecord.FlowID);
            List<Guid> autoNodeIDs = await _businessFlowHelper.DisregardConditionsStartStepAsync(flowTemplateID, flowRecord.FlowID, step.UpID.Value, initiatorID);
            await _unitOfWork.CommitAsync();
            RunAutoNodes(flowTemplateID, autoNodeIDs);
        }
        public async Task SaveFlowDataAsync(Guid flowTemplateID, Guid flowRecordID, string jsonData)
        {
            FlowRecord flowRecord = await _flowRecordRepository.FirstAsync(flowTemplateID, flowRecordID);
            if (flowRecord.State != FlowRecordStateEnum.Wait) throw new BusinessFlowException("该节点已操作，不能修改数据");
            await _businessFlowHelper.SaveFlowDataAsync(flowTemplateID, flowRecord, jsonData);
            await _unitOfWork.CommitAsync();
        }
        private async Task<List<Guid>> GetBacklogFlowTemplateIDsByUserIDAsync(Guid userID)
        {
            List<FlowUser> flowUsers = await _flowUserRepository.GetListAsync(m => m.UserID == userID);
            List<Guid> result = flowUsers.Select(m => m.FlowTemplateID).Distinct().ToList();
            return result;
        }
        /// <summary>
        /// 运行自动节点
        /// </summary>
        /// <param name="flowTemplateID"></param>
        /// <param name="flowRecordIDs"></param>
        private void RunAutoNodes(Guid flowTemplateID, List<Guid> flowRecordIDs)
        {
            foreach (Guid flowRecordID in flowRecordIDs)
            {
                _autoNodeBus.ExcuteAutoNode(flowTemplateID, flowRecordID);
            }
        }
    }
}
