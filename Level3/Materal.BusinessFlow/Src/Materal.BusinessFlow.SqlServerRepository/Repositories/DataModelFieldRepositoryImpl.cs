using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.TTA.ADONETRepository;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class DataModelFieldRepositoryImpl : BusinessFlowRepositoryImpl<DataModelField>, IDataModelFieldRepository
    {
        public DataModelFieldRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
