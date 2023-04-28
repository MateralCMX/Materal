using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.SqlClient;

namespace Materal.TTA.SqlServerADONETRepository
{
    /// <summary>
    /// SqlServerADONET工作单元
    /// </summary>
    public class SqlServerADONETUnitOfWorkImpl<TDBOption> : ADONETUnitOfWorkImpl<TDBOption>, IADONETUnitOfWork
        where TDBOption : DBOption
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="connectionString"></param>
        public SqlServerADONETUnitOfWorkImpl(IServiceProvider serviceProvider, string connectionString) : base(serviceProvider, new SqlConnection(connectionString), SqlServerConfigModel.ParamsPrefix, SqlServerConfigModel.FieldPrefix, SqlServerConfigModel.FieldSuffix)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbOption"></param>
        public SqlServerADONETUnitOfWorkImpl(IServiceProvider serviceProvider, TDBOption dbOption) : this(serviceProvider, dbOption.ConnectionString)
        {
        }
    }
    /// <summary>
    /// SqlServerADONET工作单元
    /// </summary>
    public class SqlServerADONETUnitOfWorkImpl<TDbConfig, TPrimaryKeyType> : ADONETUnitOfWorkImpl<TDbConfig, TPrimaryKeyType>, IADONETUnitOfWork<TPrimaryKeyType>
        where TPrimaryKeyType : struct
        where TDbConfig : DBOption
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="connectionString"></param>
        public SqlServerADONETUnitOfWorkImpl(IServiceProvider serviceProvider, string connectionString) : base(serviceProvider, new SqlConnection(connectionString), SqlServerConfigModel.ParamsPrefix, SqlServerConfigModel.FieldPrefix, SqlServerConfigModel.FieldSuffix)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbConfig"></param>
        public SqlServerADONETUnitOfWorkImpl(IServiceProvider serviceProvider, TDbConfig dbConfig) : this(serviceProvider, dbConfig.ConnectionString)
        {
        }
    }
}
