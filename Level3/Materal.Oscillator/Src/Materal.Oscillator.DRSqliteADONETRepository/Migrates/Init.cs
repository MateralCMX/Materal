using Materal.TTA.SqliteADONETRepository;

namespace Materal.Oscillator.DRSqliteADONETRepository.Migrates
{
    /// <summary>
    /// 初始化数据库
    /// </summary>
    public class Init : SqliteMigration
    {
        /// <summary>
        /// 位序
        /// </summary>
        public override int Index => 0;
        /// <summary>
        /// 获得升级TSQL
        /// </summary>
        /// <returns></returns>
        public override List<string> GetUpTSQL() => new()
        {
            @"CREATE TABLE ""Flow"" (
  ""ID"" TEXT NOT NULL,
  ""JobKey"" TEXT NOT NULL,
  ""ScheduleData"" TEXT NOT NULL,
  ""ScheduleID"" TEXT NOT NULL,
  ""WorkID"" TEXT,
  ""WorkResults"" TEXT,
  ""AuthenticationCode"" TEXT NOT NULL,
  ""CreateTime"" TEXT NOT NULL,
  ""UpdateTime"" TEXT NOT NULL,
  CONSTRAINT ""PK_Flow"" PRIMARY KEY (""ID"")
);",
        };
    }
}
