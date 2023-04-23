using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;

namespace Materal.DBHelper
{
    /// <summary>   
    /// MSSQLHelperParameterCache提供缓存存储过程参数,并能够在运行时从存储过程中探索参数.   
    /// </summary>   
    public static class SQLiteParameterCache
    {
        #region 私有方法,字段,构造函数   
        private static readonly Hashtable ParamCache = Hashtable.Synchronized(new Hashtable());
        /// <summary>   
        /// 探索运行时的存储过程,返回SQLiteParameter参数数组.   
        /// 初始化参数值为 DBNull.Value.   
        /// </summary>   
        /// <param name="connection">一个有效的数据库连接</param>   
        /// <param name="spName">存储过程名称</param>   
        /// <param name="includeReturnValueParameter">是否包含返回值参数</param>   
        /// <returns>返回SQLiteParameter参数数组</returns>   
        private static SQLiteParameter[] DiscoverSpParameterSet(SQLiteConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            var cmd = new SQLiteCommand(spName, connection) {CommandType = CommandType.StoredProcedure};
            connection.Open();
            //SQLiteCommandBuilder.DeriveParameters(cmd); 
            connection.Close();
            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            } 
            var discoveredParameters = new SQLiteParameter[cmd.Parameters.Count];
            cmd.Parameters.CopyTo(discoveredParameters, 0);
            foreach (SQLiteParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }
            return discoveredParameters;
        }

        /// <summary>   
        /// SQLiteParameter参数数组的深层拷贝.   
        /// </summary>   
        /// <param name="originalParameters">原始参数数组</param>   
        /// <returns>返回一个同样的参数数组</returns>   
        private static SQLiteParameter[] CloneParameters(SQLiteParameter[] originalParameters)
        {
            var clonedParameters = new SQLiteParameter[originalParameters.Length];
            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SQLiteParameter)((ICloneable)originalParameters[i]).Clone();
            }
            return clonedParameters;
        }

        #endregion 私有方法,字段,构造函数结束  

        #region 缓存方法  

        /// <summary>   
        /// 追加参数数组到缓存.   
        /// </summary>   
        /// <param name="connectionString">一个有效的数据库连接字符串</param>   
        /// <param name="commandText">存储过程名或SQL语句</param>   
        /// <param name="commandParameters">要缓存的参数数组</param>   
        public static void CacheParameterSet(string connectionString, string commandText, params SQLiteParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));
            string hashKey = connectionString + ":" + commandText;
            ParamCache[hashKey] = commandParameters;
        }

        /// <summary>   
        /// 从缓存中获取参数数组.   
        /// </summary>   
        /// <param name="connectionString">一个有效的数据库连接字符</param>   
        /// <param name="commandText">存储过程名或SQL语句</param>   
        /// <returns>参数数组</returns>   
        public static SQLiteParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));
            string hashKey = connectionString + ":" + commandText;
            return ParamCache[hashKey] is SQLiteParameter[] cachedParameters ? CloneParameters(cachedParameters) : null;
        }

        #endregion 缓存方法结束  

        #region 检索指定的存储过程的参数集  

        /// <summary>   
        /// 返回指定的存储过程的参数集   
        /// </summary>   
        /// <remarks>   
        /// 这个方法将查询数据库,并将信息存储到缓存.   
        /// </remarks>   
        /// <param name="connectionString">一个有效的数据库连接字符</param>   
        /// <param name="spName">存储过程名</param>   
        /// <returns>返回SQLiteParameter参数数组</returns>   
        public static SQLiteParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        /// <summary>   
        /// 返回指定的存储过程的参数集   
        /// </summary>   
        /// <remarks>   
        /// 这个方法将查询数据库,并将信息存储到缓存.   
        /// </remarks>   
        /// <param name="connectionString">一个有效的数据库连接字符.</param>   
        /// <param name="spName">存储过程名</param>   
        /// <param name="includeReturnValueParameter">是否包含返回值参数</param>   
        /// <returns>返回SQLiteParameter参数数组</returns>   
        public static SQLiteParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            using (var connection = new SQLiteConnection(connectionString))
            {
                return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>   
        /// [内部]返回指定的存储过程的参数集(使用连接对象).   
        /// </summary>   
        /// <remarks>   
        /// 这个方法将查询数据库,并将信息存储到缓存.   
        /// </remarks>   
        /// <param name="connection">一个有效的数据库连接字符</param>   
        /// <param name="spName">存储过程名</param>   
        /// <returns>返回SQLiteParameter参数数组</returns>   
        internal static SQLiteParameter[] GetSpParameterSet(SQLiteConnection connection, string spName)
        {
            return GetSpParameterSet(connection, spName, false);
        }

        /// <summary>   
        /// [内部]返回指定的存储过程的参数集(使用连接对象)   
        /// </summary>   
        /// <remarks>   
        /// 这个方法将查询数据库,并将信息存储到缓存.   
        /// </remarks>   
        /// <param name="connection">一个有效的数据库连接对象</param>   
        /// <param name="spName">存储过程名</param>   
        /// <param name="includeReturnValueParameter">   
        /// 是否包含返回值参数   
        /// </param>   
        /// <returns>返回SQLiteParameter参数数组</returns>   
        internal static SQLiteParameter[] GetSpParameterSet(SQLiteConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            using (var clonedConnection = (SQLiteConnection)connection.Clone())
            {
                return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
            }
        }

        /// <summary>   
        /// [私有]返回指定的存储过程的参数集(使用连接对象)   
        /// </summary>   
        /// <param name="connection">一个有效的数据库连接对象</param>   
        /// <param name="spName">存储过程名</param>   
        /// <param name="includeReturnValueParameter">是否包含返回值参数</param>   
        /// <returns>返回SQLiteParameter参数数组</returns>   
        private static SQLiteParameter[] GetSpParameterSetInternal(SQLiteConnection connection, string spName, bool includeReturnValueParameter)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            string hashKey = connection.ConnectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            if (ParamCache[hashKey] is SQLiteParameter[] cachedParameters) return CloneParameters(cachedParameters);
            SQLiteParameter[] spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
            ParamCache[hashKey] = spParameters;
            cachedParameters = spParameters;
            return CloneParameters(cachedParameters);
        }

        #endregion 参数集检索结束  
    }
}
