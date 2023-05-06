//using Materal.BusinessFlow.Abstractions;
//using Materal.BusinessFlow.Abstractions.Domain;
//using Materal.BusinessFlow.Abstractions.Repositories;
//using Materal.BusinessFlow.ADONETRepository.Repositories;

//namespace Materal.BusinessFlow.SqliteRepository.Repositories
//{
//    public class FlowRecordRepositoryImpl : BaseFlowRecordRepositoryImpl, IFlowRecordRepository
//    {
//        public FlowRecordRepositoryImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
//        {
//        }
//        protected override string GetCreateTableTSQL(Guid flowTemplateID)
//        {
//            string tableName = GetTableName(flowTemplateID);
//            return SqliteRepositoryHelper<FlowRecord>.GetCreateTableTSQL(UnitOfWork, tableName);
//        }
//        protected override string GetTableExistsTSQL(Guid flowTemplateID)
//        {
//            string tableName = GetTableName(flowTemplateID);
//            return SqliteRepositoryHelper.GetTableExistsTSQL(UnitOfWork, tableName);
//        }
//    }
//}
