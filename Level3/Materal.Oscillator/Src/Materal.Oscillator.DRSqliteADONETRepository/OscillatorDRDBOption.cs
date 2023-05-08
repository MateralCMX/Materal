using Materal.TTA.ADONETRepository;
using Materal.TTA.Common.Model;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Materal.Oscillator.DRSqliteADONETRepository
{
    /// <summary>
    /// OscillatorDRSqlite选项
    /// </summary>
    public class OscillatorDRDBOption : DBOption
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbConfig"></param>
        public OscillatorDRDBOption(SqliteConfigModel dbConfig) : base(dbConfig.ConnectionString)
        {
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connectionString"></param>
        public OscillatorDRDBOption(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// 获得连接
        /// </summary>
        /// <returns></returns>
        public override IDbConnection GetConnection() => new SqliteConnection(ConnectionString);
    }
}
