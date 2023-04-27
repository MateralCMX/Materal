using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.SqlClient;

namespace Materal.TTA.SqlServerADONETRepository
{
    /// <summary>
    /// SqlServerADONET工作单元
    /// </summary>
    public class SqlServerADONETUnitOfWorkImpl : ADONETUnitOfWorkImpl, IADONETUnitOfWork
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbConfigModel"></param>
        public SqlServerADONETUnitOfWorkImpl(IServiceProvider serviceProvider, SqlServerConfigModel dbConfigModel) : base(serviceProvider, new SqlConnection(dbConfigModel.ConnectionString), "@", "[", "]")
        {
        }
    }
}
