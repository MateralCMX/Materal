using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.Sqlite;

namespace Materal.TTA.SqliteADONETRepository
{
    /// <summary>
    /// SqliteADONET工作单元
    /// </summary>
    public class SqliteADONETUnitOfWorkImpl : ADONETUnitOfWorkImpl, IADONETUnitOfWork
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbConfigModel"></param>
        public SqliteADONETUnitOfWorkImpl(IServiceProvider serviceProvider, SqliteConfigModel dbConfigModel) : base(serviceProvider, new SqliteConnection(dbConfigModel.ConnectionString), "@", "\"", "\"")
        {
        }
    }
}
