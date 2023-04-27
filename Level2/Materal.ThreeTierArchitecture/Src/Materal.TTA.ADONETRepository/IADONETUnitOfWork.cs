using Materal.TTA.Common;
using System.Data;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// ADONET工作单元
    /// </summary>
    public interface IADONETUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 注册命令
        /// </summary>
        /// <param name="addFunc"></param>
        void RegisterCommand(Func<IDbConnection, IDbTransaction, IDbCommand?> addFunc);
        /// <summary>
        /// 操作数据库
        /// </summary>
        /// <param name="action"></param>
        void OperationDB(Action<IDbConnection> action);
        /// <summary>
        /// 操作数据库
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        /// <exception cref="TTAException"></exception>
        Task OperationDBAsync(Func<IDbConnection, Task> func);
        /// <summary>
        /// 获得TSQL字段
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        string GetTSQLField(string field);
    }
    /// <summary>
    /// ADONET工作单元
    /// </summary>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    public interface IADONETUnitOfWork<TPrimaryKeyType> : IADONETUnitOfWork, IUnitOfWork<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
