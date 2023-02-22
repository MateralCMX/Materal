using Materal.Logger.DBHelper;
using Materal.Logger.Models;
using System.Data.SQLite;

namespace Materal.Logger.Repositories
{
    /// <summary>
    /// 日志Sqlite仓储
    /// </summary>
    public class LoggerSqliteRepository : SqliteBaseRepository<LogModel>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="path"></param>
        public LoggerSqliteRepository(string path) : base(path)
        {
        }
        /// <summary>
        /// 表名称
        /// </summary>
        protected override string TableName => "MateralLogs";
        /// <summary>
        /// 获得创建表语句
        /// </summary>
        /// <returns></returns>
        protected override string GetCreateTableTSQL()
        {
            return $@"CREATE TABLE ""{TableName}"" (
""{nameof(LogModel.ID)}"" TEXT NOT NULL,
""{nameof(LogModel.CreateTime)}"" DATE NOT NULL,
""{nameof(LogModel.Level)}"" TEXT NOT NULL,
""{nameof(LogModel.ProgressID)}"" TEXT NOT NULL,
""{nameof(LogModel.ThreadID)}"" TEXT NOT NULL,
""{nameof(LogModel.Scope)}"" TEXT NOT NULL,
""{nameof(LogModel.MachineName)}"" TEXT,
""{nameof(LogModel.CategoryName)}"" TEXT,
""{nameof(LogModel.Application)}"" TEXT NOT NULL,
""{nameof(LogModel.Message)}"" TEXT NOT NULL,
""{nameof(LogModel.Error)}"" TEXT,
""{nameof(LogModel.CustomInfo)}"" TEXT NOT NULL,
PRIMARY KEY (""{nameof(LogModel.ID)}"")
);
CREATE INDEX ""{nameof(LogModel.CreateTime)}Index""
ON ""{TableName}"" (
  ""{nameof(LogModel.CreateTime)}"" DESC
);
";
        }
        const string insertTSQL = @"Insert into ""MateralLogs""(""ID"",""CreateTime"",""Level"",""ProgressID"",""ThreadID"",""Scope"",""MachineName"",""CategoryName"",""Application"",""Message"",""Error"",""CustomInfo"")
Values(@ID,@CreateTime,@Level,@ProgressID,@ThreadID,@Scope,@MachineName,@CategoryName,@Application,@Message,@Error,@CustomInfo);";
        /// <summary>
        /// 获得插入命令文本
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected override string GetInsertCommandText(Type type) => insertTSQL;
        /// <summary>
        /// 填充参数
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="domain"></param>
        protected override void FillParams(SQLiteCommand cmd, LogModel domain)
        {
            cmd.Parameters.AddWithValue("@ID", domain.ID.ToString());
            cmd.Parameters.AddWithValue("@CreateTime", domain.CreateTime);
            cmd.Parameters.AddWithValue("@Level", domain.Level);
            cmd.Parameters.AddWithValue("@ProgressID", domain.ProgressID);
            cmd.Parameters.AddWithValue("@ThreadID", domain.ThreadID);
            cmd.Parameters.AddWithValue("@Scope", domain.Scope);
            cmd.Parameters.AddWithValue("@MachineName", domain.MachineName);
            cmd.Parameters.AddWithValue("@CategoryName", domain.CategoryName);
            cmd.Parameters.AddWithValue("@Application", domain.Application);
            cmd.Parameters.AddWithValue("@Message", domain.Message);
            cmd.Parameters.AddWithValue("@Error", domain.Error);
            cmd.Parameters.AddWithValue("@CustomInfo", domain.CustomInfo);
        }
    }
}
