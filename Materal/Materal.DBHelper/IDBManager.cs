using System.Collections.Generic;
using System.Data;

namespace Materal.DBHelper
{
    public interface IDBManager<TConnection, TCommand, in TParams, in TTransaction> where TConnection : IDbConnection where TCommand : IDbCommand where TParams : IDbDataParameter where TTransaction : IDbTransaction
    {
        #region ConnectionTest
        /// <summary>
        /// 链接测试
        /// </summary>
        /// <returns>测试结果</returns>
        bool ConnectionTest();

        /// <summary>
        /// 链接测试
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>测试结果</returns>
        bool ConnectionTest(string connectionString);

        /// <summary>
        /// 链接测试
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <returns>测试结果</returns>
        bool ConnectionTest(TConnection connection);
        #endregion
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
        TConnection GetDBConnection();

        /// <summary>
        /// 获得数据库连接对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>连接对象</returns>
        TConnection GetDBConnection(string connectionString);

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
        int ExecuteNonQuery(CommandType commandType, string commandText, params TParams[] commandParameters);

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
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params TParams[] commandParameters);

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
        int ExecuteNonQuery(TConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(TConnection connection, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行非查询命令
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(TConnection connection, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行非查询命令(带事务)
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(TTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// 执行非查询命令(带事务)
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(TTransaction transaction, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行非查询命令(带事务)
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>影响的行数</returns>
        int ExecuteNonQuery(TTransaction transaction, string spName, params object[] parameterValues);

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
        DataSet ExecuteDataSet(CommandType commandType, string commandText, params TParams[] commandParameters);

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
        DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params TParams[] commandParameters);

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
        DataSet ExecuteDataSet(TConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataSet ExecuteDataSet(TConnection connection, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(TConnection connection, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(TTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataSet ExecuteDataSet(TTransaction transaction, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数据集对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据集</returns>
        DataSet ExecuteDataSet(TTransaction transaction, string spName, params object[] parameterValues);

        #endregion

        #region ExecuteDataTable

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableIndex"></param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText, int tableIndex = 0);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableIndex"></param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText, int tableIndex = 0, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableIndex"></param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(string spName, int tableIndex = 0, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableIndex"></param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, int tableIndex = 0);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableIndex"></param>
        /// <param name="commandParameters">命令参数</param>
        DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, int tableIndex = 0, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableIndex"></param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(string connectionString, string spName, int tableIndex = 0, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableIndex"></param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(TConnection connection, CommandType commandType, string commandText, int tableIndex = 0);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableIndex"></param>
        /// <param name="commandParameters">命令参数</param>
        DataTable ExecuteDataTable(TConnection connection, CommandType commandType, string commandText, int tableIndex = 0, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableIndex"></param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(TConnection connection, string spName, int tableIndex = 0, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableIndex"></param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(TTransaction transaction, CommandType commandType, string commandText, int tableIndex = 0);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableIndex"></param>
        /// <param name="commandParameters">命令参数</param>
        DataTable ExecuteDataTable(TTransaction transaction, CommandType commandType, string commandText, int tableIndex = 0, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableIndex"></param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(TTransaction transaction, string spName, int tableIndex = 0, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableName"></param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText, string tableName);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableName"></param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText, string tableName, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableName"></param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(string spName, string tableName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableName"></param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, string tableName);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableName"></param>
        /// <param name="commandParameters">命令参数</param>
        DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, string tableName, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableName"></param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(string connectionString, string spName, string tableName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableName"></param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(TConnection connection, CommandType commandType, string commandText, string tableName);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableName"></param>
        /// <param name="commandParameters">命令参数</param>
        DataTable ExecuteDataTable(TConnection connection, CommandType commandType, string commandText, string tableName, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableName"></param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(TConnection connection, string spName, string tableName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableName"></param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(TTransaction transaction, CommandType commandType, string commandText, string tableName);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableName"></param>
        /// <param name="commandParameters">命令参数</param>
        DataTable ExecuteDataTable(TTransaction transaction, CommandType commandType, string commandText, string tableName, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数据表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableName"></param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据表</returns>
        DataTable ExecuteDataTable(TTransaction transaction, string spName, string tableName, params object[] parameterValues);

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
        IList<T> ExecuteList<T>(CommandType commandType, string commandText, params TParams[] commandParameters);

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
        IList<T> ExecuteList<T>(string connectionString, CommandType commandType, string commandText, params TParams[] commandParameters);

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
        IList<T> ExecuteList<T>(TConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        IList<T> ExecuteList<T>(TConnection connection, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(TConnection connection, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(TTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        IList<T> ExecuteList<T>(TTransaction transaction, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回列表对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>列表</returns>
        IList<T> ExecuteList<T>(TTransaction transaction, string spName, params object[] parameterValues);

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
        T[] ExecuteArray<T>(CommandType commandType, string commandText, params TParams[] commandParameters);

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
        T[] ExecuteArray<T>(string connectionString, CommandType commandType, string commandText, params TParams[] commandParameters);

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
        T[] ExecuteArray<T>(TConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        T[] ExecuteArray<T>(TConnection connection, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(TConnection connection, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(TTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        T[] ExecuteArray<T>(TTransaction transaction, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回数组对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数组</returns>
        T[] ExecuteArray<T>(TTransaction transaction, string spName, params object[] parameterValues);

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(string connectionString, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(string connectionString, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(TConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(TConnection connection, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(TConnection connection, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(TTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(TTransaction transaction, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一行第一列</returns>
        object ExecuteScalar(TTransaction transaction, string spName, params object[] parameterValues);

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
        T ExecuteScalar<T>(CommandType commandType, string commandText, params TParams[] commandParameters);

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
        T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, params TParams[] commandParameters);

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
        T ExecuteScalar<T>(TConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(TConnection connection, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(TConnection connection, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(TTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(TTransaction transaction, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一行第一列
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一行第一列</returns>
        T ExecuteScalar<T>(TTransaction transaction, string spName, params object[] parameterValues);

        #endregion

        #region ExecuteFirst

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一个</returns>
        DataRow ExecuteFirst(CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>第一个</returns>
        DataRow ExecuteFirst(CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一个</returns>
        DataRow ExecuteFirst(string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一个</returns>
        DataRow ExecuteFirst(string connectionString, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataRow ExecuteFirst(string connectionString, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一个</returns>
        DataRow ExecuteFirst(string connectionString, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一个</returns>
        DataRow ExecuteFirst(TConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataRow ExecuteFirst(TConnection connection, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一个</returns>
        DataRow ExecuteFirst(TConnection connection, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一个</returns>
        DataRow ExecuteFirst(TTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        DataRow ExecuteFirst(TTransaction transaction, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一个</returns>
        DataRow ExecuteFirst(TTransaction transaction, string spName, params object[] parameterValues);

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
        T ExecuteFirst<T>(CommandType commandType, string commandText, params TParams[] commandParameters);

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
        T ExecuteFirst<T>(string connectionString, CommandType commandType, string commandText, params TParams[] commandParameters);

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
        T ExecuteFirst<T>(TConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        T ExecuteFirst<T>(TConnection connection, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(TConnection connection, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(TTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        T ExecuteFirst<T>(TTransaction transaction, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行命令,返回第一个对象
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>第一个</returns>
        T ExecuteFirst<T>(TTransaction transaction, string spName, params object[] parameterValues);

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
        IDataReader ExecuteReader(CommandType commandType, string commandText, params TParams[] commandParameters);

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
        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params TParams[] commandParameters);

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
        IDataReader ExecuteReader(TConnection connection, CommandType commandType, string commandText);

        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(TConnection connection, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(TConnection connection, string spName, params object[] parameterValues);

        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(TTransaction transaction, CommandType commandType, string commandText);

        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(TTransaction transaction, CommandType commandType, string commandText, params TParams[] commandParameters);

        /// <summary>
        /// 执行读取
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>数据读取对象</returns>
        IDataReader ExecuteReader(TTransaction transaction, string spName, params object[] parameterValues);

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
        IDataReader ExecuteReader(TConnection connection, TTransaction transaction, CommandType commandType, string commandText, TParams[] commandParameters, DBConnectionOwnership connectionOwnership);

        #endregion

        #region FillDataSet

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        void FillDataSet(CommandType commandType, string commandText, DataSet dataSet);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(CommandType commandType, string commandText, DataSet dataSet, params TParams[] commandParameters);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(string spName, DataSet dataSet, params object[] parameterValues);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        void FillDataSet(string connectionString, CommandType commandType, string commandText, DataSet dataSet);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(string connectionString, CommandType commandType, string commandText, DataSet dataSet, params TParams[] commandParameters);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(string connectionString, string spName, DataSet dataSet, params object[] parameterValues);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        void FillDataSet(TConnection connection, CommandType commandType, string commandText, DataSet dataSet);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(TConnection connection, CommandType commandType, string commandText, DataSet dataSet, params TParams[] commandParameters);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(TConnection connection, string spName, DataSet dataSet, params object[] parameterValues);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        void FillDataSet(TTransaction transaction, CommandType commandType, string commandText, DataSet dataSet);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(TTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, params TParams[] commandParameters);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(TTransaction transaction, string spName, DataSet dataSet, params object[] parameterValues);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="commandParameters">命令参数</param>
        void FillDataSet(TConnection connection, TTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, params TParams[] commandParameters);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="tableNames">表名</param>
        void FillDataSet(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableNames"></param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params TParams[] commandParameters);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableNames"></param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="tableNames">表名</param>
        void FillDataSet(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableNames"></param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params TParams[] commandParameters);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableNames"></param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(string connectionString, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="tableNames">表名</param>
        void FillDataSet(TConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableNames"></param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(TConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params TParams[] commandParameters);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableNames"></param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(TConnection connection, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="dataSet">数据集</param>
        /// <param name="tableNames">表名</param>
        void FillDataSet(TTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="tableNames"></param>
        /// <param name="commandParameters">命令参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(TTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params TParams[] commandParameters);

        /// <summary>
        /// 填充数据集
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="tableNames"></param>
        /// <param name="parameterValues">参数</param>
        /// <param name="dataSet"></param>
        void FillDataSet(TTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);

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
        void FillDataSet(TConnection connection, TTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params TParams[] commandParameters);

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
        void UpdateDataset(TCommand insertCommand, TCommand deleteCommand, TCommand updateCommand, DataSet dataSet, string tableName);

        /// <summary>
        /// 更新数据集
        /// </summary>
        /// <param name="insertCommand">添加命令</param>
        /// <param name="deleteCommand">删除命令</param>
        /// <param name="updateCommand">修改命令</param>
        /// <param name="dataTable">数据表对象</param>
        void UpdateDataset(TCommand insertCommand, TCommand deleteCommand, TCommand updateCommand, DataTable dataTable);

        #endregion

        #region CreateCommand

        /// <summary>
        /// 创建命令对象
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="sourceColumns">源列名</param>
        /// <returns></returns>
        TCommand CreateCommand(TConnection connection, string spName, params string[] sourceColumns);

        #endregion
    }
}
