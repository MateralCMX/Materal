using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.BusinessFlow.SqliteRepository.Repositories
{
    public class FlowRecordRepositoryImpl : BaseFlowRecordRepositoryImpl, IFlowRecordRepository
    {
        public FlowRecordRepositoryImpl(IBusinessFlowUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        protected override string GetCreateTableTSQL(Guid flowTemplateID)
        {
            string tableName = GetTableName(flowTemplateID);
            return SqliteRepositoryHelper<FlowRecord>.GetCreateTableTSQL(tableName);
        }
        protected override string GetTableExistsTSQL(Guid flowTemplateID)
        {
            string tableName = GetTableName(flowTemplateID);
            return SqliteRepositoryHelper.GetTableExistsTSQL(tableName);
        }
    }
}
