using Materal.TTA.ADONETRepository;
using Microsoft.Data.SqlClient;

namespace Materal.TTA.SqlServerADONETRepository
{
    /// <summary>
    /// SqlServerADONET工作单元
    /// </summary>
    public abstract class SqlServerADONETUnitOfWorkImpl<TDBOption> : ADONETUnitOfWorkImpl<TDBOption>, IADONETUnitOfWork
        where TDBOption : DBOption
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="connectionString"></param>
        protected SqlServerADONETUnitOfWorkImpl(IServiceProvider serviceProvider, string connectionString) : base(serviceProvider, new SqlConnection(connectionString))
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbOption"></param>
        protected SqlServerADONETUnitOfWorkImpl(IServiceProvider serviceProvider, TDBOption dbOption) : base(serviceProvider, dbOption.GetConnection())
        {
        }
        /// <summary>
        /// 获得TSQL字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public override string GetTSQLField(string field) => SqlServerRepositoryHelper.GetTSQLField(field);
    }
    /// <summary>
    /// SqlServerADONET工作单元
    /// </summary>
    public abstract class SqlServerADONETUnitOfWorkImpl<TDBOption, TPrimaryKeyType> : ADONETUnitOfWorkImpl<TDBOption, TPrimaryKeyType>, IADONETUnitOfWork<TPrimaryKeyType>
        where TPrimaryKeyType : struct
        where TDBOption : DBOption
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="connectionString"></param>
        public SqlServerADONETUnitOfWorkImpl(IServiceProvider serviceProvider, string connectionString) : base(serviceProvider, new SqlConnection(connectionString))
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbOption"></param>
        public SqlServerADONETUnitOfWorkImpl(IServiceProvider serviceProvider, TDBOption dbOption) : base(serviceProvider, dbOption.GetConnection())
        {
        }
        /// <summary>
        /// 获得TSQL字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public override string GetTSQLField(string field) => SqlServerRepositoryHelper.GetTSQLField(field);
    }
}
