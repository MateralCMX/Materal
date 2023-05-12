using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models.Step;

namespace Materal.BusinessFlow.Services
{
    public class StepServiceImpl : BaseServiceImpl<Step, Step, IStepRepository, AddStepModel, EditStepModel, QueryStepModel>, IStepService
    {
        private readonly INodeRepository _nodeRepository;
        public StepServiceImpl(IServiceProvider serviceProvider, INodeRepository nodeRepository) : base(serviceProvider)
        {
            _nodeRepository = nodeRepository;
        }
        public override async Task<Guid> AddAsync(AddStepModel model)
        {
            model.Validation();
            Step domain = model.CopyProperties<Step>();
            domain.Validation();
            if (domain.UpID != null)
            {
                Step upStep = await DefaultRepository.FirstAsync(domain.UpID.Value);
                upStep.NextID = domain.ID;
                UnitOfWork.RegisterEdit(upStep);
            }
            if (domain.NextID != null)
            {
                Step nextStep = await DefaultRepository.FirstAsync(domain.NextID.Value);
                nextStep.UpID = domain.ID;
                UnitOfWork.RegisterEdit(nextStep);
            }
            UnitOfWork.RegisterAdd(domain);
            await UnitOfWork.CommitAsync();
            return domain.ID;
        }
        public override async Task DeleteAsync(Guid id)
        {
            List<Node> nodes = await _nodeRepository.FindAsync(m => m.StepID == id);
            foreach (Node node in nodes)
            {
                UnitOfWork.RegisterDelete(node);
            }
            Step domain = await DefaultRepository.FirstAsync(id);
            if (domain.UpID != null)
            {
                Step upStep = await DefaultRepository.FirstAsync(domain.UpID.Value);
                upStep.NextID = domain.NextID;
                UnitOfWork.RegisterEdit(upStep);
            }
            if (domain.NextID != null)
            {
                Step nextStep = await DefaultRepository.FirstAsync(domain.NextID.Value);
                nextStep.UpID = domain.UpID;
                UnitOfWork.RegisterEdit(nextStep);
            }
            UnitOfWork.RegisterDelete(domain);
            await UnitOfWork.CommitAsync();

        }
        public async Task<List<Step>> GetListByFlowTemplateIDAsync(Guid flowTemplateID)
        {
            List<Step> result = new();
            List<Step> allSteps = await DefaultRepository.FindAsync(m => m.FlowTemplateID == flowTemplateID);
            if (allSteps.Count == 0) return result;
            Guid? nextID = null;
            Step step;
            do
            {
                step = GetNextStep(allSteps, nextID);
                nextID = step.ID;
                result.Add(step);
            } while (step.NextID != null);
            return result;
        }
        /// <summary>
        /// 获得下一个步骤
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="nowID"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        private Step GetNextStep(List<Step> steps, Guid? nowID)
        {
            Step? step = steps.FirstOrDefault(m => m.UpID == nowID);
            if (step == null) throw new BusinessFlowException("未找到对应步骤");
            return step;
        }
    }
}
