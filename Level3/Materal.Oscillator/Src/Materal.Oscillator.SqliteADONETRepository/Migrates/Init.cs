using Materal.TTA.SqliteADONETRepository;

namespace Materal.Oscillator.SqliteRepository.Migrates
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
            @"CREATE TABLE ""Answer"" (
  ""ID"" TEXT NOT NULL,
  ""Name"" TEXT NOT NULL,
  ""ScheduleID"" TEXT NOT NULL,
  ""WorkEvent"" TEXT NOT NULL,
  ""Index"" INTEGER NOT NULL,
  ""Description"" TEXT,
  ""AnswerType"" TEXT NOT NULL,
  ""AnswerData"" TEXT NOT NULL,
  ""CreateTime"" TEXT NOT NULL,
  ""UpdateTime"" TEXT NOT NULL,
  CONSTRAINT ""PK_Answer"" PRIMARY KEY (""ID"")
);",
            @"CREATE TABLE ""Plan"" (
  ""ID"" TEXT NOT NULL,
  ""Name"" TEXT NOT NULL,
  ""ScheduleID"" TEXT NOT NULL,
  ""Description"" TEXT,
  ""PlanTriggerType"" TEXT NOT NULL,
  ""PlanTriggerData"" TEXT NOT NULL,
  ""CreateTime"" TEXT NOT NULL,
  ""UpdateTime"" TEXT NOT NULL,
  CONSTRAINT ""PK_Plan"" PRIMARY KEY (""ID"")
);",
            @"CREATE TABLE ""Schedule"" (
  ""ID"" TEXT NOT NULL,
  ""Name"" TEXT NOT NULL,
  ""Territory"" TEXT NOT NULL,
  ""Enable"" INTEGER NOT NULL,
  ""Description"" TEXT,
  ""CreateTime"" TEXT NOT NULL,
  ""UpdateTime"" TEXT NOT NULL,
  CONSTRAINT ""PK_Schedule"" PRIMARY KEY (""ID"")
);",
            @"CREATE TABLE ""ScheduleWork"" (
  ""ID"" TEXT NOT NULL,
  ""ScheduleID"" TEXT NOT NULL,
  ""WorkID"" TEXT NOT NULL,
  ""Index"" INTEGER NOT NULL,
  ""SuccessEvent"" TEXT NOT NULL,
  ""FailEvent"" TEXT NOT NULL,
  ""CreateTime"" TEXT NOT NULL,
  ""UpdateTime"" TEXT NOT NULL,
  CONSTRAINT ""PK_ScheduleWork"" PRIMARY KEY (""ID"")
);",
            @"CREATE TABLE ""Work"" (
  ""ID"" TEXT NOT NULL,
  ""Name"" TEXT NOT NULL,
  ""WorkType"" TEXT NOT NULL,
  ""WorkData"" TEXT NOT NULL,
  ""Description"" TEXT,
  ""CreateTime"" TEXT NOT NULL,
  ""UpdateTime"" TEXT NOT NULL,
  CONSTRAINT ""PK_Work"" PRIMARY KEY (""ID"")
);",
        };
    }
}
