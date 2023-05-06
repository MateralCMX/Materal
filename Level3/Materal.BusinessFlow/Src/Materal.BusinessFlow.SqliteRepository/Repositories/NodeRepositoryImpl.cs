using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.TTA.ADONETRepository;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class NodeRepositoryImpl : BusinessFlowRepositoryImpl<Node>, INodeRepository
    {
        public NodeRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
