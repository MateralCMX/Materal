using System.Data;
using System.Reflection;
using System.Text;

namespace Materal.Logger.DBHelper
{
    /// <summary>
    /// 基础仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T>
    {
        /// <summary>
        /// 数据库链接
        /// </summary>
        protected IDbConnection? DBConnection;
        /// <summary>
        /// 获得数据库链接
        /// </summary>
        protected Func<IDbConnection> GetDBConnection;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="getDBConnection"></param>
        protected BaseRepository(Func<IDbConnection> getDBConnection)
        {
            GetDBConnection = getDBConnection;
        }
        /// <summary>
        /// 表名称
        /// </summary>
        protected virtual string TableName => typeof(T).Name;
        /// <summary>
        /// 参数前缀
        /// </summary>
        protected virtual string ParameterPrefix => "@";
        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Init();
        /// <summary>
        /// 打开数据库链接
        /// </summary>
        /// <exception cref="LoggerException"></exception>
        public virtual void OpenDBConnection()
        {
            DBConnection ??= GetDBConnection();
            if (DBConnection.State == ConnectionState.Closed)
            {
                DBConnection.Open();
            }
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
        /// <param name="domains"></param>
        /// <exception cref="LoggerException"></exception>
        public virtual void Inserts(T[] domains)
        {
            DBConnection ??= GetDBConnection();
            OpenDBConnection();
            using IDbTransaction transaction = DBConnection.BeginTransaction();
            try
            {
                Inserts(domains, transaction);
            }
            finally
            {
                CloseDBConnection();
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="domains"></param>
        /// <param name="dbTransaction"></param>
        /// <exception cref="LoggerException"></exception>
        public virtual void Inserts(T[] domains, IDbTransaction dbTransaction)
        {
            DBConnection ??= GetDBConnection();
            OpenDBConnection();
            try
            {
                IDbCommand cmd = DBConnection.CreateCommand();
                cmd.Transaction = dbTransaction;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.CommandText = GetInsertsCommandText(domains, cmd);
                try
                {
                    int result = cmd.ExecuteNonQuery();
                    if (result <= 0) throw new LoggerException("添加失败");
                    dbTransaction.Commit();
                }
                finally
                {
                    cmd.Dispose();
                }
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
        /// <summary>
        /// 获得添加Sql文本
        /// </summary>
        /// <param name="domains"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        protected virtual string GetInsertsCommandText(T[] domains, IDbCommand cmd)
        {
            StringBuilder tSqlBuilder = new();
            for (int i = 0; i < domains.Length; i++)
            {
                string tSql = GetInsertCommandText(domains[i], cmd, i);
                tSqlBuilder.AppendLine(tSql);
            }
            return tSqlBuilder.ToString();
        }
        /// <summary>
        /// 获得添加Sql文本
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="cmd"></param>
        /// <param name="insertIndex"></param>
        /// <returns></returns>
        protected virtual string GetInsertCommandText(T domain, IDbCommand cmd, int insertIndex)
        {
            Type tType = typeof(T);
            StringBuilder properties = new();
            StringBuilder values = new();
            foreach (PropertyInfo propertyInfo in tType.GetProperties())
            {
                object? value = propertyInfo.GetValue(domain);
                //if (value == null) continue;
                properties.Append($"\"{propertyInfo.Name}\",");
                string parameterName = $"{ParameterPrefix}{propertyInfo.Name}{insertIndex}";
                values.Append($"{parameterName},");
                IDbDataParameter parameter = cmd.CreateParameter();
                parameter.ParameterName = parameterName;
                parameter.Value = value ?? DBNull.Value;
                cmd.Parameters.Add(parameter);
            }
            string propertiesString = properties.ToString()[0..^1];
            string valuesString = values.ToString()[0..^1];
            string result = $"Insert into \"{TableName}\"({propertiesString}) Values({valuesString});";
            return result;
        }
    }
}
