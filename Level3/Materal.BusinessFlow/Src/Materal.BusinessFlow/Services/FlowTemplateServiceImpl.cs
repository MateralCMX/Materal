using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.FlowTemplate;
using Materal.Utils.Model;

namespace Materal.BusinessFlow.Services
{
    public class FlowTemplateServiceImpl : BaseServiceImpl<FlowTemplate, FlowTemplateDTO, IFlowTemplateRepository, AddFlowTemplateModel, EditFlowTemplateModel, QueryFlowTemplateModel>, IFlowTemplateService
    {
        private readonly IDataModelRepository _dataModelRepository;
        private readonly IFlowRepository _flowRepository;
        private readonly IStepRepository _stepRepository;
        private readonly INodeRepository _nodeRepository;
        public FlowTemplateServiceImpl(IServiceProvider serviceProvider, IDataModelRepository dataModelRepository, IFlowRepository flowRepository, IStepRepository stepRepository, INodeRepository nodeRepository) : base(serviceProvider)
        {
            _dataModelRepository = dataModelRepository;
            _flowRepository = flowRepository;
            _stepRepository = stepRepository;
            _nodeRepository = nodeRepository;
        }
        public override async Task EditAsync(EditFlowTemplateModel model)
        {
            model.Validation();
            FlowTemplate domain = await DefaultRepository.FirstAsync(model.ID);
            if (domain.DataModelID != model.DataModelID) throw new BusinessFlowException("不能修改绑定的数据模型");
            model.CopyProperties(domain);
            domain.Validation();
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
        public override async Task DeleteAsync(Guid id)
        {
            if(_flowRepository.CanUse(id)) throw new BusinessFlowException("流程已被执行，不能删除");
            FlowTemplate domain = await DefaultRepository.FirstAsync(id);
            UnitOfWork.RegisterDelete(domain);
            List<Step> steps = await _stepRepository.FindAsync(m => m.FlowTemplateID == domain.ID);
            foreach (Step step in steps)
            {
                UnitOfWork.RegisterDelete(step);
            }
            List<Guid> allStepIDs = steps.Select(m => m.ID).ToList();
            List<Node> nodes = await _nodeRepository.FindAsync(m => allStepIDs.Contains(m.StepID));
            foreach (Node node in nodes)
            {
                UnitOfWork.RegisterDelete(node);
            }
            await UnitOfWork.CommitAsync();
        }
        public override async Task<FlowTemplateDTO> GetInfoAsync(Guid id)
        {
            FlowTemplateDTO result = await base.GetInfoAsync(id);
            DataModel dataModel = await _dataModelRepository.FirstAsync(m => m.ID == result.DataModelID);
            result.DataModelName = dataModel.Name;
            return result;
        }
        public override async Task<List<FlowTemplateDTO>> GetListAsync(QueryFlowTemplateModel? queryModel = null)
        {
            List<FlowTemplateDTO> result = await base.GetListAsync(queryModel);
            await BindDTOInfoAsync(result);
            return result;
        }
        public override async Task<(List<FlowTemplateDTO> data, PageModel pageInfo)> PagingAsync(QueryFlowTemplateModel? queryModel = null)
        {
            (List<FlowTemplateDTO> result, PageModel pageInfo) = await base.PagingAsync(queryModel);
            await BindDTOInfoAsync(result);
            return (result, pageInfo);
        }
        /// <summary>
        /// 绑定DTO信息
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task BindDTOInfoAsync(List<FlowTemplateDTO> result)
        {
            List<Guid> allDataModelIDs = result.Select(m => m.DataModelID).ToList();
            List<DataModel> dataModels = await _dataModelRepository.FindAsync(m => allDataModelIDs.Contains(m.ID));
            foreach (FlowTemplateDTO item in result)
            {
                item.DataModelName = dataModels.First(m => m.ID == item.DataModelID).Name;
            }
        }
    }
}
