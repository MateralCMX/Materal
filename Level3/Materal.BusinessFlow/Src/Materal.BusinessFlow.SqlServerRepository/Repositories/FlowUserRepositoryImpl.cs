using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Repositories;
using Materal.TTA.ADONETRepository;
using Materal.TTA.ADONETRepository.Extensions;
using System.Data;

namespace Materal.BusinessFlow.SqlServerRepository.Repositories
{
    public class FlowUserRepositoryImpl : BusinessFlowRepositoryImpl<FlowUser>, IFlowUserRepository
    {
        public FlowUserRepositoryImpl(IADONETUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Guid> GetUserFlowTemplateIDs(Guid userID)
        {
            List<Guid> result = new();
            UnitOfWork.OperationDB(connection =>
            {
                IDbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT DISTINCT [FlowTemplateID] FROM [FlowUser] WHERE [UserID] = @UserID";
                command.AddParameter("@UserID", userID);
                using IDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    result.Add(dr.GetGuid(0));
                }
            });
            return result;
        }
    }
}
