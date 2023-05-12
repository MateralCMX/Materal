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
                Step upStep = DefaultRepository.First(domain.UpID.Value);
                upStep.NextID = domain.ID;
                UnitOfWork.RegisterEdit(upStep);
            }
            if (domain.NextID != null)
            {
                Step nextStep = DefaultRepository.First(domain.NextID.Value);
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
            Step domain = DefaultRepository.First(id);
            if (domain.UpID != null)
            {
                Step upStep = DefaultRepository.First(domain.UpID.Value);
                upStep.NextID = domain.NextID;
                UnitOfWork.RegisterEdit(upStep);
            }
            if (domain.NextID != null)
            {
                Step nextStep = DefaultRepository.First(domain.NextID.Value);
                nextStep.UpID = domain.UpID;
                UnitOfWork.RegisterEdit(nextStep);
            }
            UnitOfWork.RegisterDelete(domain);
            await UnitOfWork.CommitAsync();

        }
    }
}
