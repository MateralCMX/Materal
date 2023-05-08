using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Materal.Oscillator.SqliteADONETRepository
{
    /// <summary>
    /// OscillatorSqlite选项
    /// </summary>
    public class OscillatorDBOption : DBOption
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbConfig"></param>
        public OscillatorDBOption(SqliteConfigModel dbConfig) : base(dbConfig.ConnectionString)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connectionString"></param>
        public OscillatorDBOption(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 获得连接
        /// </summary>
        /// <returns></returns>
        public override IDbConnection GetConnection() => new SqliteConnection(ConnectionString);
    }
}
