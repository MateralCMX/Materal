using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class NodeRepositoryImpl : SqlServerBaseRepositoryImpl<Node>, INodeRepository
    {
        public NodeRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
