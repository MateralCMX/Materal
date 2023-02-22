using Materal.ConvertHelper;
using Materal.Logger.Models;
using System.Data;
using System.Data.SqlClient;

namespace Materal.Logger.DBHelper
{
    /// <summary>
    /// Sqlserver基础仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SqlServerBaseRepository<T> : BaseRepository<T>
    {
        private readonly static object _createTableLockObj = new();
        private static bool _canCreateTable = true;
        private readonly string _connectionString;
        /// <summary>
        /// Sqlserver基础仓储
        /// </summary>
        /// <param name="connectionString"></param>
        protected SqlServerBaseRepository(string connectionString) : base(() =>
        {
            SqlConnectionStringBuilder builder = new(connectionString);
            return new SqlConnection(builder.ConnectionString);
        })
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <exception cref="LoggerException"></exception>
        public override void Init()
        {
            if (string.IsNullOrEmpty(_connectionString)) throw new LoggerException("未指定数据库链接字符串");
            CreateTable();
        }
        /// <summary>
        /// 创建数据库表
        /// </summary>
        /// <exception cref="LoggerException"></exception>
        protected virtual void CreateTable()
        {
            if (!_canCreateTable) return;
            DBConnection ??= GetDBConnection();
            lock (_createTableLockObj)
            {
                if (!_canCreateTable) return;
                OpenDBConnection();
                IDbCommand cmd = DBConnection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT COUNT([name]) as [TableCount] FROM [SysObjects] WHERE [name] = '{TableName}'";
                object? dataResult = cmd.ExecuteScalar();
                cmd.Dispose();
                int tableCount = 0;
                if (dataResult != null && dataResult is int tableCountResult)
                {
                    tableCount = tableCountResult;
                }
                if (tableCount > 0)
                {
                    CloseDBConnection();
                    return;
                }
                IDbTransaction dbTransaction = DBConnection.BeginTransaction();
                cmd = DBConnection.CreateCommand();
                cmd.Transaction = dbTransaction;
                try
                {
                    cmd.CommandText = GetCreateTableTSQL();
                    int result = cmd.ExecuteNonQuery();
                    dbTransaction.Commit();
                    cmd.Dispose();
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    throw;
                }
                finally
                {
                    dbTransaction.Dispose();
                    _canCreateTable = false;
                }
                dbTransaction = DBConnection.BeginTransaction();
                cmd = DBConnection.CreateCommand();
                cmd.Transaction = dbTransaction;
                try
                {
                    cmd.CommandText = $@"CREATE NONCLUSTERED INDEX [IX_MateralLogs] ON [{TableName}]
(
	[{nameof(LogModel.CreateTime)}] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
                    int result = cmd.ExecuteNonQuery();
                    dbTransaction.Commit();
                    cmd.Dispose();
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    throw;
                }
                finally
                {
                    dbTransaction.Dispose();
                    CloseDBConnection();
                }
            }
        }
        /// <summary>
        /// 获取创建表sql文本
        /// </summary>
        /// <returns></returns>
        protected abstract string GetCreateTableTSQL();
        /// <summary>
        /// 插入多个
        /// </summary>
        /// <param name="domains"></param>
        /// <exception cref="LoggerException"></exception>
        public override async void Inserts(T[] domains)
        {
            DBConnection ??= GetDBConnection();
            OpenDBConnection();
            try
            {
                if (DBConnection is not SqlConnection sqlConnection) throw new LoggerException("连接对象不是SqlServer连接对象");
                using SqlBulkCopy sqlBulkCopy = new(sqlConnection);
                sqlBulkCopy.DestinationTableName = TableName;
                DataTable dt = domains.ToDataTable();
                sqlBulkCopy.BatchSize = dt.Rows.Count;
                foreach (DataColumn column in dt.Columns)
                {
                    sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }
                await sqlBulkCopy.WriteToServerAsync(dt);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseDBConnection();
            }
        }
    }
}
