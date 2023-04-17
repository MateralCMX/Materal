using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.ADONETRepository;
using Materal.BusinessFlow.SqlServerRepository.Models;
using Microsoft.Data.SqlClient;

namespace Materal.BusinessFlow.SqlServerRepository
{
    public class UnitOfWorkImpl : BaseUnitOfWorkImpl, IUnitOfWork
    {
        public UnitOfWorkImpl(IServiceProvider serviceProvider, SqlServerConfigModel dbConfigModel) : base(serviceProvider, new SqlConnection(dbConfigModel.ConnectionString), "@", "[", "]")
        {
        }
    }
}
