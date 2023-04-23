using Materal.ConvertHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Materal.DBHelper
{
    public class SqlServerManager : IDBManager<SqlConnection, SqlCommand, SqlParameter, SqlTransaction>
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string _connectionString = string.Empty;

        private static SqlConnection _sqlConnection;

        public bool ConnectionTest()
        {
            return ConnectionTest(GetDBConnection());
        }

        public bool ConnectionTest(string connectionString)
        {
            return ConnectionTest(GetDBConnection(connectionString));
        }

        public bool ConnectionTest(SqlConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

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
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));
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
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            string connectionString = connection.ConnectionString;
            if (parameterValues == null || parameterValues.Length <= 0) return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
            SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(connectionString, spName);
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
            if (commandText == null) throw new ArgumentNullException(nameof(commandText));
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
            SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(transaction.Connection, spName);
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
            return ExecuteDataSet(GetDBConnection(connectionString), commandType, commandText);
        }

        public DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(GetDBConnection(connectionString), commandType, commandText, commandParameters);
        }

        public DataSet ExecuteDataSet(string connectionString, string spName, params object[] parameterValues)
        {
            return ExecuteDataSet(GetDBConnection(connectionString), spName, parameterValues);
        }

        public DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteDataSet(connection, commandType, commandText, null);
        }

        public DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            var dataSet = new DataSet();
            FillDataSet(connection, commandType, commandText, dataSet, commandParameters);
            return dataSet;
        }

        public DataSet ExecuteDataSet(SqlConnection connection, string spName, params object[] parameterValues)
        {
            var dataSet = new DataSet();
            FillDataSet(connection, spName, dataSet, parameterValues);
            return dataSet;
        }

        public DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataSet(transaction, commandType, commandText, null);
        }

        public DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            var dataSet = new DataSet();
            FillDataSet(transaction, commandType, commandText, dataSet, commandParameters);
            return dataSet;
        }

        public DataSet ExecuteDataSet(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            var dataSet = new DataSet();
            FillDataSet(transaction, spName, dataSet, parameterValues);
            return dataSet;
        }

        public DataTable ExecuteDataTable(CommandType commandType, string commandText, int tableIndex = 0)
        {
            return ExecuteDataTable(GetDBConnection(), commandType, commandText, tableIndex);
        }

        public DataTable ExecuteDataTable(CommandType commandType, string commandText, int tableIndex = 0, params SqlParameter[] commandParameters)
        {
            return ExecuteDataTable(GetDBConnection(), commandType, commandText, tableIndex, commandParameters);
        }

        public DataTable ExecuteDataTable(string spName, int tableIndex = 0, params object[] parameterValues)
        {
            return ExecuteDataTable(GetDBConnection(), spName, tableIndex, parameterValues);
        }

        public DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, int tableIndex = 0)
        {
            return ExecuteDataTable(GetDBConnection(connectionString), commandType, commandText, tableIndex);
        }

        public DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, int tableIndex = 0, params SqlParameter[] commandParameters)
        {
            return ExecuteDataTable(GetDBConnection(connectionString), commandType, commandText, tableIndex, commandParameters);
        }

        public DataTable ExecuteDataTable(string connectionString, string spName, int tableIndex = 0, params object[] parameterValues)
        {
            return ExecuteDataTable(GetDBConnection(connectionString), spName, tableIndex, parameterValues);
        }

        public DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, int tableIndex = 0)
        {
            return ExecuteDataTable(connection, commandType, commandText, tableIndex, null);
        }

        public DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, int tableIndex = 0, params SqlParameter[] commandParameters)
        {
            DataSet dataSet = ExecuteDataSet(connection, commandType, commandText, commandParameters);
            if (dataSet.Tables.Count <= tableIndex) throw new IndexOutOfRangeException("索引超出范围。必须为非负值并小于集合大小");
            return dataSet.Tables[tableIndex];
        }

        public DataTable ExecuteDataTable(SqlConnection connection, string spName, int tableIndex = 0, params object[] parameterValues)
        {
            DataSet dataSet = ExecuteDataSet(connection, spName, parameterValues);
            if (dataSet.Tables.Count <= tableIndex) throw new IndexOutOfRangeException("索引超出范围。必须为非负值并小于集合大小");
            return dataSet.Tables[tableIndex];
        }

        public DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText, int tableIndex = 0)
        {
            return ExecuteDataTable(transaction, commandType, commandText, tableIndex, null);
        }

        public DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText, int tableIndex = 0, params SqlParameter[] commandParameters)
        {
            DataSet dataSet = ExecuteDataSet(transaction, commandType, commandText, commandParameters);
            if (dataSet.Tables.Count <= tableIndex) throw new IndexOutOfRangeException("索引超出范围。必须为非负值并小于集合大小");
            return dataSet.Tables[tableIndex];
        }

        public DataTable ExecuteDataTable(SqlTransaction transaction, string spName, int tableIndex = 0, params object[] parameterValues)
        {
            DataSet dataSet = ExecuteDataSet(transaction, spName, parameterValues);
            if (dataSet.Tables.Count <= tableIndex) throw new IndexOutOfRangeException("索引超出范围。必须为非负值并小于集合大小");
            return dataSet.Tables[tableIndex];
        }

        public DataTable ExecuteDataTable(CommandType commandType, string commandText, string tableName)
        {
            return ExecuteDataTable(GetDBConnection(), commandType, commandText, tableName);
        }

        public DataTable ExecuteDataTable(CommandType commandType, string commandText, string tableName, params SqlParameter[] commandParameters)
        {
            return ExecuteDataTable(GetDBConnection(), commandType, commandText, tableName, commandParameters);
        }

        public DataTable ExecuteDataTable(string spName, string tableName, params object[] parameterValues)
        {
            return ExecuteDataTable(GetDBConnection(), spName, tableName, parameterValues);
        }

        public DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, string tableName)
        {
            return ExecuteDataTable(GetDBConnection(connectionString), commandType, commandText, tableName);
        }

        public DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, string tableName, params SqlParameter[] commandParameters)
        {
            return ExecuteDataTable(GetDBConnection(connectionString), commandType, commandText, tableName, commandParameters);
        }

        public DataTable ExecuteDataTable(string connectionString, string spName, string tableName, params object[] parameterValues)
        {
            return ExecuteDataTable(GetDBConnection(connectionString), spName, tableName, parameterValues);
        }

        public DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, string tableName)
        {
            return ExecuteDataTable(connection, commandType, commandText, tableName, null);
        }

        public DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, string tableName, params SqlParameter[] commandParameters)
        {
            DataSet dataSet = ExecuteDataSet(connection, commandType, commandText, commandParameters);
            return dataSet.Tables[tableName];
        }

        public DataTable ExecuteDataTable(SqlConnection connection, string spName, string tableName, params object[] parameterValues)
        {
            DataSet dataSet = ExecuteDataSet(connection, spName, parameterValues);
            return dataSet.Tables[tableName];
        }

        public DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText, string tableName)
        {
            return ExecuteDataTable(transaction, commandType, commandText, tableName, null);
        }

        public DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText, string tableName, params SqlParameter[] commandParameters)
        {
            DataSet dataSet = ExecuteDataSet(transaction, commandType, commandText, commandParameters);
            return dataSet.Tables[tableName];
        }

        public DataTable ExecuteDataTable(SqlTransaction transaction, string spName, string tableName, params object[] parameterValues)
        {
            DataSet dataSet = ExecuteDataSet(transaction, spName, parameterValues);
            return dataSet.Tables[tableName];
        }

        public IList<T> ExecuteList<T>(CommandType commandType, string commandText)
        {
            return ExecuteList<T>(GetDBConnection(), commandType, commandText);
        }

        public IList<T> ExecuteList<T>(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteList<T>(GetDBConnection(), commandType, commandText, commandParameters);
        }

        public IList<T> ExecuteList<T>(string spName, params object[] parameterValues)
        {
            return ExecuteList<T>(GetDBConnection(), spName, parameterValues);
        }

        public IList<T> ExecuteList<T>(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteList<T>(GetDBConnection(connectionString), commandType, commandText);
        }

        public IList<T> ExecuteList<T>(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteList<T>(GetDBConnection(connectionString), commandType, commandText, commandParameters);
        }

        public IList<T> ExecuteList<T>(string connectionString, string spName, params object[] parameterValues)
        {
            return ExecuteList<T>(GetDBConnection(connectionString), spName, parameterValues);
        }

        public IList<T> ExecuteList<T>(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteList<T>(connection, commandType, commandText, null);
        }

        public IList<T> ExecuteList<T>(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataTable dataTable = ExecuteDataTable(connection, commandType, commandText, 0, commandParameters);
            return dataTable != null ? dataTable.ToList<T>() : throw new MateralDBHelperException("无结果");
        }

        public IList<T> ExecuteList<T>(SqlConnection connection, string spName, params object[] parameterValues)
        {
            DataTable dataTable = ExecuteDataTable(connection, spName, 0, parameterValues);
            return dataTable != null ? dataTable.ToList<T>() : throw new MateralDBHelperException("无结果");
        }

        public IList<T> ExecuteList<T>(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteList<T>(transaction, commandType, commandText, null);
        }

        public IList<T> ExecuteList<T>(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataTable dataTable = ExecuteDataTable(transaction, commandType, commandText, 0, commandParameters);
            return dataTable != null ? dataTable.ToList<T>() : throw new MateralDBHelperException("无结果");
        }

        public IList<T> ExecuteList<T>(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            DataTable dataTable = ExecuteDataTable(transaction, spName, 0, parameterValues);
            return dataTable != null ? dataTable.ToList<T>() : throw new MateralDBHelperException("无结果");
        }

        public T[] ExecuteArray<T>(CommandType commandType, string commandText)
        {
            return ExecuteArray<T>(GetDBConnection(), commandType, commandText);
        }

        public T[] ExecuteArray<T>(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteArray<T>(GetDBConnection(), commandType, commandText, commandParameters);
        }

        public T[] ExecuteArray<T>(string spName, params object[] parameterValues)
        {
            return ExecuteArray<T>(GetDBConnection(), spName, parameterValues);
        }

        public T[] ExecuteArray<T>(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteArray<T>(GetDBConnection(connectionString), commandType, commandText);
        }

        public T[] ExecuteArray<T>(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteArray<T>(GetDBConnection(connectionString), commandType, commandText, commandParameters);
        }

        public T[] ExecuteArray<T>(string connectionString, string spName, params object[] parameterValues)
        {
            return ExecuteArray<T>(GetDBConnection(connectionString), spName, parameterValues);
        }

        public T[] ExecuteArray<T>(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteArray<T>(connection, commandType, commandText, null);
        }

        public T[] ExecuteArray<T>(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataTable dataTable = ExecuteDataTable(connection, commandType, commandText, 0, commandParameters);
            return dataTable != null ? dataTable.ToArray<T>() : throw new MateralDBHelperException("无结果");
        }

        public T[] ExecuteArray<T>(SqlConnection connection, string spName, params object[] parameterValues)
        {
            DataTable dataTable = ExecuteDataTable(connection, spName, 0, parameterValues);
            return dataTable != null ? dataTable.ToArray<T>() : throw new MateralDBHelperException("无结果");
        }

        public T[] ExecuteArray<T>(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteArray<T>(transaction, commandType, commandText, null);
        }

        public T[] ExecuteArray<T>(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataTable dataTable = ExecuteDataTable(transaction, commandType, commandText, 0, commandParameters);
            return dataTable != null ? dataTable.ToArray<T>() : throw new MateralDBHelperException("无结果");
        }

        public T[] ExecuteArray<T>(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            DataTable dataTable = ExecuteDataTable(transaction, spName, 0, parameterValues);
            return dataTable != null ? dataTable.ToArray<T>() : throw new MateralDBHelperException("无结果");
        }

        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            return ExecuteScalar(GetDBConnection(), commandType, commandText);
        }

        public object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(GetDBConnection(), commandType, commandText, commandParameters);
        }

        public object ExecuteScalar(string spName, params object[] parameterValues)
        {
            return ExecuteScalar(GetDBConnection(), spName, parameterValues);
        }

        public object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteScalar(GetDBConnection(connectionString), commandType, commandText);
        }

        public object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(GetDBConnection(connectionString), commandType, commandText, commandParameters);
        }

        public object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            return ExecuteScalar(GetDBConnection(connectionString), spName, parameterValues);
        }

        public object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connection, commandType, commandText, null);
        }

        public object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));
            var cmd = new SqlCommand();
            bool mustCloseConnection = PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
            object retval = cmd.ExecuteScalar();  
            cmd.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }
            return retval;
        }

        public object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            if (parameterValues == null || parameterValues.Length <= 0) return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(connection, spName);
            AssignParameterValues(commandParameters, parameterValues);
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        public object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteScalar(transaction, commandType, commandText, null);
        }

        public object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            var cmd = new SqlCommand();
            PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
            object retval = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return retval;
        }

        public object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            if (parameterValues == null || parameterValues.Length <= 0) return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(transaction.Connection, spName);
            AssignParameterValues(commandParameters, parameterValues);
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);

        }

        public T ExecuteScalar<T>(CommandType commandType, string commandText)
        {
            return ExecuteScalar<T>(GetDBConnection(), commandType, commandText);
        }

        public T ExecuteScalar<T>(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar<T>(GetDBConnection(), commandType, commandText, commandParameters);
        }

        public T ExecuteScalar<T>(string spName, params object[] parameterValues)
        {
            return ExecuteScalar<T>(GetDBConnection(), spName, parameterValues);
        }

        public T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteScalar<T>(GetDBConnection(connectionString), commandType, commandText);
        }

        public T ExecuteScalar<T>(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar<T>(GetDBConnection(connectionString), commandType, commandText, commandParameters);
        }

        public T ExecuteScalar<T>(string connectionString, string spName, params object[] parameterValues)
        {
            return ExecuteScalar<T>(GetDBConnection(connectionString), spName, parameterValues);
        }

        public T ExecuteScalar<T>(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalar<T>(connection, commandType, commandText, null);
        }

        public T ExecuteScalar<T>(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            object result = ExecuteScalar(connection, commandType, commandText, commandParameters);
            if (result is T tResult)
            {
                return tResult;
            }
            return default(T);
        }

        public T ExecuteScalar<T>(SqlConnection connection, string spName, params object[] parameterValues)
        {
            object result = ExecuteScalar(connection, spName, parameterValues);
            if (result is T tResult)
            {
                return tResult;
            }
            return default(T);
        }

        public T ExecuteScalar<T>(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteScalar<T>(transaction, commandType, commandText, null);
        }

        public T ExecuteScalar<T>(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            object result = ExecuteScalar(transaction, commandType, commandText, commandParameters);
            if (result is T tResult)
            {
                return tResult;
            }
            return default(T);
        }

        public T ExecuteScalar<T>(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            object result = ExecuteScalar(transaction, spName, parameterValues);
            if (result is T tResult)
            {
                return tResult;
            }
            return default(T);
        }

        public DataRow ExecuteFirst(CommandType commandType, string commandText)
        {
            return ExecuteFirst(GetDBConnection(), commandType, commandText);
        }

        public DataRow ExecuteFirst(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteFirst(GetDBConnection(), commandType, commandText, commandParameters);
        }

        public DataRow ExecuteFirst(string spName, params object[] parameterValues)
        {
            return ExecuteFirst(GetDBConnection(), spName, parameterValues);
        }

        public DataRow ExecuteFirst(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteFirst(GetDBConnection(connectionString), commandType, commandText);
        }

        public DataRow ExecuteFirst(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteFirst(GetDBConnection(connectionString), commandType, commandText, commandParameters);
        }

        public DataRow ExecuteFirst(string connectionString, string spName, params object[] parameterValues)
        {
            return ExecuteFirst(GetDBConnection(connectionString), spName, parameterValues);
        }

        public DataRow ExecuteFirst(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteFirst(connection, commandType, commandText, null);
        }

        public DataRow ExecuteFirst(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataTable dataTable = ExecuteDataTable(connection, commandType, commandText, 0, commandParameters);
            if (dataTable != null && dataTable.Rows.Count>0)
            {
                return dataTable.Rows[0];
            }
            return null;
        }

        public DataRow ExecuteFirst(SqlConnection connection, string spName, params object[] parameterValues)
        {
            DataTable dataTable = ExecuteDataTable(connection, spName, 0, parameterValues);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0];
            }
            return null;
        }

        public DataRow ExecuteFirst(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteFirst(transaction, commandType, commandText, null);
        }

        public DataRow ExecuteFirst(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataTable dataTable = ExecuteDataTable(transaction, commandType, commandText, 0, commandParameters);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0];
            }
            return null;
        }

        public DataRow ExecuteFirst(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            DataTable dataTable = ExecuteDataTable(transaction, spName, 0, parameterValues);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                return dataTable.Rows[0];
            }
            return null;
        }

        public T ExecuteFirst<T>(CommandType commandType, string commandText)
        {
            return ExecuteFirst<T>(GetDBConnection(), commandType, commandText);
        }

        public T ExecuteFirst<T>(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteFirst<T>(GetDBConnection(), commandType, commandText, commandParameters);
        }

        public T ExecuteFirst<T>(string spName, params object[] parameterValues)
        {
            return ExecuteFirst<T>(GetDBConnection(), spName, parameterValues);
        }

        public T ExecuteFirst<T>(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteFirst<T>(GetDBConnection(connectionString), commandType, commandText);
        }

        public T ExecuteFirst<T>(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteFirst<T>(GetDBConnection(connectionString), commandType, commandText, commandParameters);
        }

        public T ExecuteFirst<T>(string connectionString, string spName, params object[] parameterValues)
        {
            return ExecuteFirst<T>(GetDBConnection(connectionString), spName, parameterValues);
        }

        public T ExecuteFirst<T>(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteFirst<T>(connection, commandType, commandText, null);
        }

        public T ExecuteFirst<T>(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataRow dataRow = ExecuteFirst(connection, commandType, commandText, commandParameters);
            return dataRow != null ? dataRow.ToObject<T>() : default(T);
        }

        public T ExecuteFirst<T>(SqlConnection connection, string spName, params object[] parameterValues)
        {
            DataRow dataRow = ExecuteFirst(connection, spName, parameterValues);
            return dataRow != null ? dataRow.ToObject<T>() : default(T);
        }

        public T ExecuteFirst<T>(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteFirst<T>(transaction, commandType, commandText, null);
        }

        public T ExecuteFirst<T>(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataRow dataRow = ExecuteFirst(transaction, commandType, commandText, commandParameters);
            return dataRow != null ? dataRow.ToObject<T>() : default(T);
        }

        public T ExecuteFirst<T>(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            DataRow dataRow = ExecuteFirst(transaction, spName, parameterValues);
            return dataRow != null ? dataRow.ToObject<T>() : default(T);
        }

        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return ExecuteReader(GetDBConnection(), commandType, commandText);
        }

        public IDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(GetDBConnection(), commandType, commandText, commandParameters);
        }

        public IDataReader ExecuteReader(string spName, params object[] parameterValues)
        {
            return ExecuteReader(GetDBConnection(), spName, parameterValues);
        }

        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteReader(GetDBConnection(connectionString), commandType, commandText);
        }

        public IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(GetDBConnection(connectionString), commandType, commandText, commandParameters);
        }

        public IDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            return ExecuteReader(GetDBConnection(connectionString), spName, parameterValues);
        }

        public IDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteReader(connection, commandType, commandText, null);
        }

        public IDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(connection, null, commandType, commandText, commandParameters, DBConnectionOwnership.External);
        }

        public IDataReader ExecuteReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            if (parameterValues == null || parameterValues.Length <= 0) return ExecuteReader(connection, CommandType.StoredProcedure, spName);
            SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(connection, spName);
            AssignParameterValues(commandParameters, parameterValues);
            return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        public IDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteReader(transaction, commandType, commandText, null);
        }

        public IDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, DBConnectionOwnership.External);
        }

        public IDataReader ExecuteReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            if (parameterValues == null || parameterValues.Length <= 0) return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
            SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(transaction.Connection, spName);
            AssignParameterValues(commandParameters, parameterValues);
            return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        public IDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, DBConnectionOwnership connectionOwnership)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            var cmd = new SqlCommand();
            var mustCloseConnection = false;
            try
            {
                mustCloseConnection = PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);
                SqlDataReader dataReader = connectionOwnership == DBConnectionOwnership.External ? cmd.ExecuteReader() : cmd.ExecuteReader(CommandBehavior.CloseConnection);
                var canClear = true;
                foreach (SqlParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }
                if (canClear)
                {
                    cmd.Parameters.Clear();
                }
                return dataReader;
            }
            catch
            {
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
        }

        public void FillDataSet(CommandType commandType, string commandText, DataSet dataSet)
        {
            FillDataSet(GetDBConnection(), commandType, commandText, dataSet);
        }

        public void FillDataSet(CommandType commandType, string commandText, DataSet dataSet, params SqlParameter[] commandParameters)
        {
            FillDataSet(GetDBConnection(), commandType, commandText, dataSet, commandParameters);
        }

        public void FillDataSet(string spName, DataSet dataSet, params object[] parameterValues)
        {
            FillDataSet(GetDBConnection(), spName, dataSet, parameterValues);
        }

        public void FillDataSet(string connectionString, CommandType commandType, string commandText, DataSet dataSet)
        {
            FillDataSet(GetDBConnection(connectionString), commandType, commandText, dataSet);
        }

        public void FillDataSet(string connectionString, CommandType commandType, string commandText, DataSet dataSet, params SqlParameter[] commandParameters)
        {
            FillDataSet(GetDBConnection(connectionString), commandType, commandText, dataSet, commandParameters);
        }

        public void FillDataSet(string connectionString, string spName, DataSet dataSet, params object[] parameterValues)
        {
            FillDataSet(GetDBConnection(connectionString), spName, dataSet, parameterValues);
        }

        public void FillDataSet(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet)
        {
            FillDataSet(connection, commandType, commandText, dataSet, (SqlParameter)null);
        }

        public void FillDataSet(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, params SqlParameter[] commandParameters)
        {
            FillDataSet(connection, null, commandType, commandText, dataSet, commandParameters);
        }

        public void FillDataSet(SqlConnection connection, string spName, DataSet dataSet, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            if (parameterValues == null || parameterValues.Length <= 0) FillDataSet(connection, CommandType.StoredProcedure, spName, dataSet);
            SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(connection, spName);
            AssignParameterValues(commandParameters, parameterValues);
            FillDataSet(connection, CommandType.StoredProcedure, spName, dataSet, commandParameters);
        }

        public void FillDataSet(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet)
        {
            FillDataSet(transaction, commandType, commandText, dataSet, (SqlParameter)null);
        }

        public void FillDataSet(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, params SqlParameter[] commandParameters)
        {
            FillDataSet(transaction.Connection, transaction, commandType, commandText, dataSet, commandParameters);
        }

        public void FillDataSet(SqlTransaction transaction, string spName, DataSet dataSet, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            if (parameterValues == null || parameterValues.Length <= 0) FillDataSet(transaction, CommandType.StoredProcedure, spName, dataSet);
            else
            {
                SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(commandParameters, parameterValues);
                FillDataSet(transaction, CommandType.StoredProcedure, spName, dataSet, commandParameters);
            }
        }

        public void FillDataSet(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentException("message", nameof(commandText));
            if (dataSet == null) dataSet = new DataSet();
            var command = new SqlCommand();
            bool mustCloseConnection  = PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
            using (var dataAdapter = new SqlDataAdapter(command))
            {
                dataAdapter.Fill(dataSet); 
                command.Parameters.Clear();
                if (mustCloseConnection)
                {
                    connection.Close();
                }
            }
        }

        public void FillDataSet(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            FillDataSet(GetDBConnection(), commandType, commandText, dataSet, tableNames, null);
        }

        public void FillDataSet(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            FillDataSet(GetDBConnection(), commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public void FillDataSet(string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            FillDataSet(GetDBConnection(), spName, dataSet, tableNames, parameterValues);
        }

        public void FillDataSet(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            FillDataSet(GetDBConnection(connectionString), commandType, commandText, dataSet, tableNames, null);
        }

        public void FillDataSet(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            FillDataSet(GetDBConnection(connectionString), commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public void FillDataSet(string connectionString, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            FillDataSet(GetDBConnection(connectionString), spName, dataSet, tableNames, parameterValues);
        }

        public void FillDataSet(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            FillDataSet(connection, commandType, commandText, dataSet, tableNames, null);
        }

        public void FillDataSet(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            FillDataSet(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public void FillDataSet(SqlConnection connection, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection)); if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            if (parameterValues == null || parameterValues.Length <= 0) FillDataSet(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
            else
            {
                SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(commandParameters, parameterValues);
                FillDataSet(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
        }

        public void FillDataSet(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            FillDataSet(transaction, commandType, commandText, dataSet, tableNames, null);
        }

        public void FillDataSet(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            FillDataSet(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public void FillDataSet(SqlTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            if (parameterValues == null || parameterValues.Length <= 0) FillDataSet(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
            else
            {
                SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(commandParameters, parameterValues);
                FillDataSet(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
            }
        }

        public void FillDataSet(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentException("message", nameof(commandText));
            if (dataSet == null) dataSet = new DataSet();
            var command = new SqlCommand();
            bool mustCloseConnection = PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
            using (var dataAdapter = new SqlDataAdapter(command))
            {  
                if (tableNames != null && tableNames.Length > 0)
                {
                    var tableName = "Table";
                    for (var index = 0; index < tableNames.Length; index++)
                    {
                        if (tableNames[index] == null || tableNames[index].Length == 0) throw new ArgumentException("表名错误", nameof(tableNames));
                        dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                        tableName += (index + 1).ToString();
                    }
                }  
                dataAdapter.Fill(dataSet);  
                command.Parameters.Clear();
                if (mustCloseConnection)
                {
                    connection.Close();
                }
            }
        }

        public void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null) throw new ArgumentNullException(nameof(insertCommand));
            if (deleteCommand == null) throw new ArgumentNullException(nameof(deleteCommand));
            if (updateCommand == null) throw new ArgumentNullException(nameof(updateCommand));
            if (string.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));  
            using (var dataAdapter = new SqlDataAdapter())
            { 
                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;
                dataAdapter.Update(dataSet, tableName);  
                dataSet.AcceptChanges();
            }
        }

        public void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataTable dataTable)
        {
            if (insertCommand == null) throw new ArgumentNullException(nameof(insertCommand));
            if (deleteCommand == null) throw new ArgumentNullException(nameof(deleteCommand));
            if (updateCommand == null) throw new ArgumentNullException(nameof(updateCommand));
            if (dataTable == null) throw new ArgumentNullException(nameof(dataTable));
            using (var dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.UpdateCommand = updateCommand;
                dataAdapter.InsertCommand = insertCommand;
                dataAdapter.DeleteCommand = deleteCommand;
                dataAdapter.Update(dataTable);
                dataTable.AcceptChanges();
            }
        }

        public SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
            var cmd = new SqlCommand(spName, connection) {CommandType = CommandType.StoredProcedure};
            if ((sourceColumns == null) || (sourceColumns.Length <= 0)) return cmd;
            SqlParameter[] commandParameters = SqlServerParameterCache.GetSpParameterSet(connection, spName);
            for (var index = 0; index < sourceColumns.Length; index++)
            {
                commandParameters[index].SourceColumn = sourceColumns[index];
            }
            AttachParameters(cmd, commandParameters);
            return cmd;
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
