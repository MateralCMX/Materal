using Materal.Logger.DBHelper;
using Materal.Logger.Models;
using System.Data.SQLite;

namespace Materal.Logger.Repositories
{
    public class MateralLoggerSqliteRepository : SqliteBaseRepository<MateralLogModel>
    {
        public MateralLoggerSqliteRepository(string path) : base(path)
        {
        }
        protected override string TableName => "MateralLogs";
        protected override string GetCreateTableTSQL()
        {
            return $@"CREATE TABLE ""{TableName}"" (
""{nameof(MateralLogModel.ID)}"" TEXT NOT NULL,
""{nameof(MateralLogModel.CreateTime)}"" DATE NOT NULL,
""{nameof(MateralLogModel.Level)}"" TEXT NOT NULL,
""{nameof(MateralLogModel.ProgressID)}"" TEXT NOT NULL,
""{nameof(MateralLogModel.ThreadID)}"" TEXT NOT NULL,
""{nameof(MateralLogModel.Scope)}"" TEXT NOT NULL,
""{nameof(MateralLogModel.MachineName)}"" TEXT,
""{nameof(MateralLogModel.CategoryName)}"" TEXT,
""{nameof(MateralLogModel.Application)}"" TEXT NOT NULL,
""{nameof(MateralLogModel.Message)}"" TEXT NOT NULL,
""{nameof(MateralLogModel.Error)}"" TEXT,
""{nameof(MateralLogModel.CustomInfo)}"" TEXT NOT NULL,
PRIMARY KEY (""{nameof(MateralLogModel.ID)}"")
);
CREATE INDEX ""{nameof(MateralLogModel.CreateTime)}Index""
ON ""{TableName}"" (
  ""{nameof(MateralLogModel.CreateTime)}"" DESC
);
";
        }
        const string insertTSQL = @"Insert into ""MateralLogs""(""ID"",""CreateTime"",""Level"",""ProgressID"",""ThreadID"",""Scope"",""MachineName"",""CategoryName"",""Application"",""Message"",""Error"",""CustomInfo"")
Values(@ID,@CreateTime,@Level,@ProgressID,@ThreadID,@Scope,@MachineName,@CategoryName,@Application,@Message,@Error,@CustomInfo);";
        protected override string GetInsertCommandText(Type type) => insertTSQL;

        protected override void FillParams(SQLiteCommand cmd, MateralLogModel domain)
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
