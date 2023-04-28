using Materal.TTA.Common;
using Materal.TTA.Common.Model;
using System.Data;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// 数据库选项
    /// </summary>
    public abstract class DBOption
    {
        private readonly string? _connectionString;
        /// <summary>
        /// 数据库配置
        /// </summary>
        public IDBConfigModel? DBConfig { get; set; }
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (_connectionString != null && !string.IsNullOrWhiteSpace(_connectionString)) return _connectionString;
                if (DBConfig != null) return DBConfig.ConnectionString;
                throw new TTAException("未设置链接字符串");
            }
        }
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns></returns>
        public abstract IDbConnection GetConnection();
        /// <summary>
        /// 数据库选项
        /// </summary>
        /// <param name="connectionString"></param>
        protected DBOption(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// 数据库选项
        /// </summary>
        /// <param name="dbConfig"></param>
        protected DBOption(IDBConfigModel dbConfig)
        {
            DBConfig = dbConfig;
        }
    }
}
