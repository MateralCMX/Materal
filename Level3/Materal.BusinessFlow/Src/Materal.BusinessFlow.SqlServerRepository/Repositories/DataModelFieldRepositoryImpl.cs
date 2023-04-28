using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class DataModelFieldRepositoryImpl : SqlServerBaseRepositoryImpl<DataModelField>, IDataModelFieldRepository
    {
        public DataModelFieldRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
