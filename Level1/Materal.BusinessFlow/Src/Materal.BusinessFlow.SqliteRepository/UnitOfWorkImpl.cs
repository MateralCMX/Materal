using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.ADONETRepository;
using Materal.BusinessFlow.SqliteRepository.Models;
using Microsoft.Data.Sqlite;

namespace Materal.BusinessFlow.SqliteRepository
{
    public class UnitOfWorkImpl : BaseUnitOfWorkImpl, IUnitOfWork
    {
        public UnitOfWorkImpl(IServiceProvider serviceProvider, SqliteConfigModel dbConfigModel) : base(serviceProvider, new SqliteConnection(dbConfigModel.ConnectionString), "@", "\"", "\"")
        {
        }
    }
}
