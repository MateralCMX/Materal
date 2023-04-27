using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.Sqlite;

namespace Materal.TTA.SqliteADONETRepository
{
    /// <summary>
    /// SqliteADONET工作单元
    /// </summary>
    public class SqliteADONETUnitOfWorkImpl<TDbConfig> : ADONETUnitOfWorkImpl<TDbConfig>, IADONETUnitOfWork
        where TDbConfig : DbConfig
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="connectionString"></param>
        public SqliteADONETUnitOfWorkImpl(IServiceProvider serviceProvider, string connectionString) : base(serviceProvider, new SqliteConnection(connectionString), SqliteConfigModel.ParamsPrefix, SqliteConfigModel.FieldPrefix, SqliteConfigModel.FieldSuffix)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbConfig"></param>
        public SqliteADONETUnitOfWorkImpl(IServiceProvider serviceProvider, TDbConfig dbConfig) : this(serviceProvider, dbConfig.ConnectionString)
        {
        }
    }
    /// <summary>
    /// SqliteADONET工作单元
    /// </summary>
    public class SqliteADONETUnitOfWorkImpl<TDbConfig, TPrimaryKeyType> : ADONETUnitOfWorkImpl<TDbConfig, TPrimaryKeyType>, IADONETUnitOfWork<TPrimaryKeyType>
        where TDbConfig : DbConfig
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="connectionString"></param>
        public SqliteADONETUnitOfWorkImpl(IServiceProvider serviceProvider, string connectionString) : base(serviceProvider, new SqliteConnection(connectionString), SqliteConfigModel.ParamsPrefix, SqliteConfigModel.FieldPrefix, SqliteConfigModel.FieldSuffix)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbConfig"></param>
        public SqliteADONETUnitOfWorkImpl(IServiceProvider serviceProvider, TDbConfig dbConfig) : this(serviceProvider, dbConfig.ConnectionString)
        {
        }
    }
}
