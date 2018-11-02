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
            if (_connectionString == connectionString)
            {
                return _sqlConnection ?? (_sqlConnection = new SqlConnection(connectionString));
            }
            else
            {
                return new SqlConnection(connectionString);
            }
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText,
            params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataSet(string spName, params object[] parameterValues)
        {
            throw new NotImplementedException();
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
    }
}
