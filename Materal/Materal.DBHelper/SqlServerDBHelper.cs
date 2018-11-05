using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Materal.DBHelper
{
    public class SqlServerDBHelper : IDBHelper<SqlConnection, SqlCommand, SqlParameter, SqlTransaction>
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string _connectionString = string.Empty;

        private static SqlConnection _sqlConnection;

        public void SetDBConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetDBConnection()
        {
            return GetDBConnection(_connectionString);
        }

        public SqlConnection GetDBConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
            if (_connectionString == connectionString)
            {
                return _sqlConnection ?? (_sqlConnection = new SqlConnection(connectionString));
            }
            return new SqlConnection(connectionString);
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(GetDBConnection(), commandType, commandText);
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(GetDBConnection(), commandType, commandText, commandParameters);
        }

        public int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            return ExecuteNonQuery(GetDBConnection(), spName, parameterValues);
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(GetDBConnection(connectionString), commandType, commandText);
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(GetDBConnection(connectionString), commandType, commandText, commandParameters);
        }

        public int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            return ExecuteNonQuery(GetDBConnection(connectionString), spName, parameterValues);
        }

        public int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connection, commandType, commandText, null);
        }

        public int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand();
            bool mustCloseConnection = PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
            int retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }
            return retval;
        }

        public int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            string connectionString = connection.ConnectionString;
            if (parameterValues == null || parameterValues.Length <= 0) return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            SqlParameter[] commandParameters = SqlServerHelperParameterCache.GetSpParameterSet(connectionString, spName);
            AssignParameterValues(commandParameters, parameterValues);
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);

        }

        public int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        public int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            var cmd = new SqlCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters); 
            int retval = cmd.ExecuteNonQuery(); 
            cmd.Parameters.Clear();
            return retval;
        }

        public int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            if (parameterValues == null || parameterValues.Length <= 0) return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] commandParameters = SqlServerHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
            AssignParameterValues(commandParameters, parameterValues);
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            return ExecuteDataSet(GetDBConnection(), commandType, commandText);
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(GetDBConnection(), commandType, commandText, commandParameters);
        }

        public DataSet ExecuteDataSet(string spName, params object[] parameterValues)
        {
            return ExecuteDataSet(GetDBConnection(), spName, parameterValues);
        }

        public DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(string connectionString, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(SqlConnection connection, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(string connectionString, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(SqlConnection connection, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteDataTable(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(string connectionString, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(string connectionString, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(SqlConnection connection, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(SqlConnection connection, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> ExecuteList<T>(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(string connectionString, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(string connectionString, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(SqlConnection connection, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(SqlConnection connection, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T[] ExecuteArray<T>(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(string connectionString, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(SqlConnection connection, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(SqlConnection connection, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T ExecuteScalar<T>(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(string connectionString, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(string connectionString, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(SqlConnection connection, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(SqlConnection connection, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public T ExecuteFirst<T>(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public IDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType,
            string commandText, SqlParameter[] commandParameters, DBConnectionOwnership connectionOwnership)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet,
            string[] tableNames)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet,
            string[] tableNames, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(string connectionString, string spName, DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet,
            string[] tableNames)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet,
            string[] tableNames, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(SqlConnection connection, string spName, DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet,
            string[] tableNames)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet,
            string[] tableNames, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(SqlTransaction transaction, string spName, DataSet dataSet, string[] tableNames,
            params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public void FillDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText,
            DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet,
            string tableName)
        {
            throw new NotImplementedException();
        }

        public void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        public SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
        {
            throw new NotImplementedException();
        }

        /// <summary>   
        /// 将SqlParameter参数数组(参数值)分配给SqlCommand命令.   
        /// 这个方法将给任何一个参数分配DBNull.Value;   
        /// 该操作将阻止默认值的使用.   
        /// </summary>   
        /// <param name="command">命令名</param>   
        /// <param name="commandParameters">SqlParameters数组</param>   
        private void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (commandParameters == null) return;
            foreach (SqlParameter p in commandParameters)
            {
                if (p == null) continue;  
                if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && p.Value == null)
                {
                    p.Value = DBNull.Value;
                }
                command.Parameters.Add(p);
            }
        }

        /// <summary>   
        /// 预处理用户提供的命令,数据库连接/事务/命令类型/参数   
        /// </summary>   
        /// <param name="command">要处理的SqlCommand</param>   
        /// <param name="connection">数据库连接</param>   
        /// <param name="transaction">一个有效的事务或者是null值</param>   
        /// <param name="commandType">命令类型 (存储过程,命令文本, 其它.)</param>   
        /// <param name="commandText">存储过程名或都T-SQL命令文本</param>   
        /// <param name="commandParameters">和命令相关联的SqlParameter参数数组,如果没有参数为'null'</param>
        /// <returns>如果连接是打开的,则为true,其它情况下为false.</returns>
        private bool PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            bool mustCloseConnection;
            if (command == null) throw new ArgumentNullException(nameof(command));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText)); 
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            command.Connection = connection;  
            command.CommandText = commandText;
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("事务已被回滚或提交，请提供一个打开的事务.", nameof(transaction));
                command.Transaction = transaction;
            } 
            command.CommandType = commandType;
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            return mustCloseConnection;
        }

        /// <summary>   
        /// 将一个对象数组分配给SqlParameter参数数组.   
        /// </summary>   
        /// <param name="commandParameters">要分配值的SqlParameter参数数组</param>   
        /// <param name="parameterValues">将要分配给存储过程参数的对象数组</param>   
        private static void AssignParameterValues(IReadOnlyList<SqlParameter> commandParameters, IReadOnlyList<object> parameterValues)
        {
            if (commandParameters == null || parameterValues == null)
            {
                return;
            } 
            if (commandParameters.Count != parameterValues.Count)
            {
                throw new ArgumentException("参数值个数与参数不匹配.");
            }
            for (int i = 0, j = commandParameters.Count; i < j; i++)
            {
                switch (parameterValues[i])
                {
                    case IDbDataParameter _:
                    {
                        var paramInstance = (IDbDataParameter)parameterValues[i];
                        commandParameters[i].Value = paramInstance.Value ?? DBNull.Value;
                        break;
                    }
                    case null:
                        commandParameters[i].Value = DBNull.Value;
                        break;
                    default:
                        commandParameters[i].Value = parameterValues[i];
                        break;
                }
            }
        }
    }
}
