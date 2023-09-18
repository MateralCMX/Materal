//using System.Data;

//namespace Materal.Logger.Repositories
//{
//    /// <summary>
//    /// 基础仓储
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    public abstract class BaseRepository<T>
//    {
//        /// <summary>
//        /// 数据库链接
//        /// </summary>
//        protected IDbConnection? DBConnection;
//        /// <summary>
//        /// 获得数据库链接
//        /// </summary>
//        protected Func<IDbConnection> GetDBConnection;
//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        /// <param name="getDBConnection"></param>
//        protected BaseRepository(Func<IDbConnection> getDBConnection) => GetDBConnection = getDBConnection;
//        /// <summary>
//        /// 打开数据库链接
//        /// </summary>
//        /// <exception cref="LoggerException"></exception>
//        public virtual IDbConnection OpenDBConnection()
//        {
//            DBConnection ??= GetDBConnection();
//            if (DBConnection.State == ConnectionState.Closed)
//            {
//                DBConnection.Open();
//            }
//            return DBConnection;
//        }
//        /// <summary>
//        /// 关闭数据库链接
//        /// </summary>
//        /// <exception cref="LoggerException"></exception>
//        public virtual void CloseDBConnection()
//        {
//            if (DBConnection == null || DBConnection.State != ConnectionState.Open) return;
//            DBConnection.Close();
//            DBConnection.Dispose();
//            DBConnection = null;
//        }
//        /// <summary>
//        /// 添加
//        /// </summary>
//        /// <param name="models"></param>
//        /// <exception cref="LoggerException"></exception>
//        public abstract void Inserts(T[] models);
//    }
//}
