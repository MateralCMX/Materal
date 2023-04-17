using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.BusinessFlow.ADONETRepository.Repositories;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class FlowRecordRepositoryImpl : BaseFlowRecordRepositoryImpl, IFlowRecordRepository
    {
        public FlowRecordRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        protected override string GetCreateTableTSQL(Guid flowTemplateID)
        {
            string tableName = GetTableName(flowTemplateID);
            return SqlServerRepositoryHelper<FlowRecord>.GetCreateTableTSQL(UnitOfWork, tableName);
        }

        protected override string GetTableExistsTSQL(Guid flowTemplateID)
        {
            string tableName = GetTableName(flowTemplateID);
            return SqlServerRepositoryHelper.GetTableExistsTSQL(UnitOfWork, tableName);
        }
    }
}
