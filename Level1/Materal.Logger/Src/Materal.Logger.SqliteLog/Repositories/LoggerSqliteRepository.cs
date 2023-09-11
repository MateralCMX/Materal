//using Materal.Logger.Models;
//using Microsoft.Data.Sqlite;

//namespace Materal.Logger.Repositories
//{
//    /// <summary>
//    /// 日志Sqlite仓储
//    /// </summary>
//    public class LoggerSqliteRepository : SqliteBaseRepository<LogModel>
//    {
//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        /// <param name="path"></param>
//        public LoggerSqliteRepository(string path) : base(path)
//        {
//        }
//        /// <summary>
//        /// 表名称
//        /// </summary>
//        protected override string TableName => "MateralLogs";
//        /// <summary>
//        /// 获得创建表语句
//        /// </summary>
//        /// <returns></returns>
//        protected override string GetCreateTableTSQL() => $@"CREATE TABLE ""{TableName}"" (
//""{nameof(LogModel.ID)}"" TEXT(36) NOT NULL,
//""{nameof(LogModel.CreateTime)}"" DATE NOT NULL,
//""{nameof(LogModel.Level)}"" TEXT NOT NULL,
//""{nameof(LogModel.ProgressID)}"" TEXT NOT NULL,
//""{nameof(LogModel.ThreadID)}"" TEXT NOT NULL,
//""{nameof(LogModel.Scope)}"" TEXT NOT NULL,
//""{nameof(LogModel.MachineName)}"" TEXT,
//""{nameof(LogModel.CategoryName)}"" TEXT,
//""{nameof(LogModel.Application)}"" TEXT NOT NULL,
//""{nameof(LogModel.Message)}"" TEXT NOT NULL,
//""{nameof(LogModel.Error)}"" TEXT,
//""{nameof(LogModel.CustomInfo)}"" TEXT NOT NULL,
//PRIMARY KEY (""{nameof(LogModel.ID)}"")
//);
//CREATE INDEX ""{nameof(LogModel.CreateTime)}Index""
//ON ""{TableName}"" (
//  ""{nameof(LogModel.CreateTime)}"" DESC
//);
//";
//        /// <summary>
//        /// 获得插入命令文本
//        /// </summary>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        protected override string GetInsertCommandText(Type type) => @$"Insert into ""{TableName}""(""ID"",""CreateTime"",""Level"",""ProgressID"",""ThreadID"",""Scope"",""MachineName"",""CategoryName"",""Application"",""Message"",""Error"",""CustomInfo"")
//Values(@ID,@CreateTime,@Level,@ProgressID,@ThreadID,@Scope,@MachineName,@CategoryName,@Application,@Message,@Error,@CustomInfo);";
//        /// <summary>
//        /// 填充参数
//        /// </summary>
//        /// <param name="cmd"></param>
//        /// <param name="domain"></param>
//        protected override void FillParams(SqliteCommand cmd, LogModel domain)
//        {
//            cmd.Parameters.Clear();
//            SetParams(cmd, "ID", domain.ID.ToString());
//            SetParams(cmd, "CreateTime", domain.CreateTime);
//            SetParams(cmd, "Level", domain.Level);
//            SetParams(cmd, "ProgressID", domain.ProgressID);
//            SetParams(cmd, "ThreadID", domain.ThreadID);
//            SetParams(cmd, "Scope", domain.Scope);
//            SetParams(cmd, "MachineName", domain.MachineName);
//            SetParams(cmd, "CategoryName", domain.CategoryName);
//            SetParams(cmd, "Application", domain.Application);
//            SetParams(cmd, "Message", domain.Message);
//            SetParams(cmd, "Error", domain.Error);
//            SetParams(cmd, "CustomInfo", domain.CustomInfo);
//        }
//        private void SetParams(SqliteCommand cmd, string name, object? value)
//        {
//            if (value is null)
//            {
//                cmd.Parameters.AddWithValue($"@{name}", DBNull.Value);
//            }
//            else
//            {
//                cmd.Parameters.AddWithValue($"@{name}", value);
//            }
//        }
//    }
//}
