using Materal.TTA.ADONETRepository;
using Microsoft.Data.Sqlite;

namespace Materal.TTA.SqliteADONETRepository
{
    /// <summary>
    /// SqliteADONET工作单元
    /// </summary>
    public abstract class SqliteADONETUnitOfWorkImpl<TDBOption> : ADONETUnitOfWorkImpl<TDBOption>, IADONETUnitOfWork
        where TDBOption : DBOption
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="connectionString"></param>
        public SqliteADONETUnitOfWorkImpl(IServiceProvider serviceProvider, string connectionString) : base(serviceProvider, new SqliteConnection(connectionString))
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbOption"></param>
        public SqliteADONETUnitOfWorkImpl(IServiceProvider serviceProvider, TDBOption dbOption) : base(serviceProvider, dbOption.GetConnection())
        {
        }
        /// <summary>
        /// 获得TSQL字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public override string GetTSQLField(string field) => SqliteRepositoryHelper.GetTSQLField(field);
    }
    /// <summary>
    /// SqliteADONET工作单元
    /// </summary>
    public abstract class SqliteADONETUnitOfWorkImpl<TDBOption, TPrimaryKeyType> : ADONETUnitOfWorkImpl<TDBOption, TPrimaryKeyType>, IADONETUnitOfWork<TPrimaryKeyType>
        where TDBOption : DBOption
        where TPrimaryKeyType : struct
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="connectionString"></param>
        protected SqliteADONETUnitOfWorkImpl(IServiceProvider serviceProvider, string connectionString) : base(serviceProvider, new SqliteConnection(connectionString))
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbOption"></param>
        protected SqliteADONETUnitOfWorkImpl(IServiceProvider serviceProvider, TDBOption dbOption) : base(serviceProvider, dbOption.GetConnection())
        {
        }
        /// <summary>
        /// 获得TSQL字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public override string GetTSQLField(string field) => SqliteRepositoryHelper.GetTSQLField(field);
    }
}
