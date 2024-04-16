using System.Data;

namespace Materal.Logger.DBLogger.Repositories
{
    /// <summary>
    /// 基础仓储
    /// </summary>
    /// <typeparam name="TDBLog"></typeparam>
    /// <typeparam name="TLoggerTargetOptions"></typeparam>
    /// <typeparam name="TDBFiled"></typeparam>
    public abstract class BaseRepository<TDBLog, TLoggerTargetOptions, TDBFiled>(Func<IDbConnection> getDBConnection)
        where TLoggerTargetOptions : DBLoggerTargetOptions<TDBFiled>
        where TDBFiled : IDBFiled, new()
        where TDBLog : DBLog<TLoggerTargetOptions, TDBFiled>
    {
        /// <summary>
        /// 数据库链接
        /// </summary>
        protected IDbConnection? DBConnection;
        /// <summary>
        /// 获得数据库链接
        /// </summary>
        protected Func<IDbConnection> GetDBConnection = getDBConnection;
        /// <summary>
        /// 打开数据库链接
        /// </summary>
        /// <exception cref="LoggerException"></exception>
        public virtual IDbConnection OpenDBConnection()
        {
            DBConnection ??= GetDBConnection();
            if (DBConnection.State == ConnectionState.Closed)
            {
                DBConnection.Open();
            }
            return DBConnection;
        }
        /// <summary>
        /// 关闭数据库链接
        /// </summary>
        /// <exception cref="LoggerException"></exception>
        public virtual void CloseDBConnection()
        {
            if (DBConnection == null || DBConnection.State != ConnectionState.Open) return;
            DBConnection.Close();
            DBConnection.Dispose();
            DBConnection = null;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="models"></param>
        public abstract void Inserts(TDBLog[] models);
    }
}
