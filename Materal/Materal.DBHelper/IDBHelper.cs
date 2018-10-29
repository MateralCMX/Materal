using System.Collections.Generic;
using System.Data;

namespace Materal.DBHelper
{
    public interface IDBHelper
    {
        #region SetDbConnection
        /// <summary>
        /// 设置数据库连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        void SetDBConnection(string connectionString);
        #endregion

        #region GetDBConnection
        /// <summary>
        /// 获得数据库连接对象
        /// </summary>
        /// <returns>连接对象</returns>
        IDbConnection GetDBConnection();
        /// <summary>
        /// 获得数据库连接对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>连接对象</returns>
        IDbConnection GetDBConnection(string connectionString);
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText);
        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(string spName, params object[] parameterValues);
        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText);
        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText);
        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(IDbConnection connection, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行非查询命令(带事务)
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText);
        /// <summary>
        /// 执行非查询命令(带事务)
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行非查询命令(带事务)
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(IDbTransaction transaction, string spName, params object[] parameterValues);
        #endregion

        #region ExecuteDataSet
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(string connectionString, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(IDbConnection connection, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataSet ExecuteDataSet(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(IDbConnection connection, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(IDbTransaction transaction, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataSet ExecuteDataSet(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(IDbTransaction transaction, string spName, params object[] parameterValues);
        #endregion

        #region ExecuteDataTable
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(string connectionString, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(IDbConnection connection, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataTable ExecuteDataTable(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(IDbConnection connection, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(IDbTransaction transaction, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataTable ExecuteDataTable(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(IDbTransaction transaction, string spName, params object[] parameterValues);
        #endregion

        #region ExecuteList
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(string connectionString, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        IList<T> ExecuteList<T>(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(string connectionString, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(IDbConnection connection, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        IList<T> ExecuteList<T>(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(IDbConnection connection, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(IDbTransaction transaction, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        IList<T> ExecuteList<T>(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(IDbTransaction transaction, string spName, params object[] parameterValues);
        #endregion

        #region ExecuteArray
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(string connectionString, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        T[] ExecuteArray<T>(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(string connectionString, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(IDbConnection connection, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        T[] ExecuteArray<T>(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(IDbConnection connection, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(IDbTransaction transaction, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        T[] ExecuteArray<T>(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(IDbTransaction transaction, string spName, params object[] parameterValues);
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(string connectionString, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(IDbConnection connection, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(IDbConnection connection, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(IDbTransaction transaction, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(IDbTransaction transaction, string spName, params object[] parameterValues);
        #endregion

        #region ExecuteFirst
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(string connectionString, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        T ExecuteFirst<T>(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(string connectionString, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(IDbConnection connection, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        T ExecuteFirst<T>(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(IDbConnection connection, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(IDbTransaction transaction, CommandType commandType, string commandText);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        T ExecuteFirst<T>(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(IDbTransaction transaction, string spName, params object[] parameterValues);
        #endregion

        #region ExecuteReader
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(CommandType commandType, string commandText);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(string spName, params object[] parameterValues);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(IDbConnection connection, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(IDbTransaction transaction, string spName, params object[] parameterValues);
        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="connectionOwnership">提供者</param>
        /// <returns></returns>
        IDataReader ExecuteReader(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters, DBConnectionOwnership connectionOwnership);
        #endregion

        #region FillDataset
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="tableNames">表名</param>
        void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableNames"></param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableNames"></param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataset(string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="tableNames">表名</param>
        void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableNames"></param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableNames"></param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataset(string connectionString, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="tableNames">表名</param>
        void FillDataset(IDbConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableNames"></param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataset(IDbConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableNames"></param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataset(IDbConnection connection, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="tableNames">表名</param>
        void FillDataset(IDbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableNames"></param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataset(IDbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params IDbDataParameter[] commandParameters);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableNames"></param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataset(IDbTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);
        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="tableNames">表名</param>
        /// <param name="commandParameters">命令参数</param>
        void FillDataset(IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params IDbDataParameter[] commandParameters);
        #endregion

        #region UpdateDataset
        /// <summary>
        /// 更新数据集
        /// </summary>
        /// <param name="insertCommand">添加命令</param>
        /// <param name="deleteCommand">删除命令</param>
        /// <param name="updateCommand">修改命令</param>
        /// <param name="dataSet">数据集对象</param>
        /// <param name="tableName">表名</param>
        void UpdateDataset(IDbCommand insertCommand, IDbCommand deleteCommand, IDbCommand updateCommand, DataSet dataSet, string tableName);
        /// <summary>
        /// 更新数据集
        /// </summary>
        /// <param name="insertCommand">添加命令</param>
        /// <param name="deleteCommand">删除命令</param>
        /// <param name="updateCommand">修改命令</param>
        /// <param name="dataTable">数据表对象</param>
        void UpdateDataset(IDbCommand insertCommand, IDbCommand deleteCommand, IDbCommand updateCommand, DataTable dataTable);
        #endregion

        #region CreateCommand
        /// <summary>
        /// 创建命令对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="sourceColumns">源列名</param>
        /// <returns></returns>
        IDbCommand CreateCommand(IDbConnection connection, string spName, params string[] sourceColumns);
        #endregion
    }
}
