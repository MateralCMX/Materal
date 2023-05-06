using Materal.TTA.SqliteADONETRepository;

namespace Materal.BusinessFlow.SqliteRepository.Migrates
{
    public class Init : SqliteMigration
    {
        public override int Index => 0;

        public override List<string> GetUpTSQL() => new()
        {
            @"CREATE TABLE ""DataModel"" (
  ""ID"" TEXT NOT NULL,
  ""Name"" TEXT NOT NULL,
  ""Description"" TEXT,
  ""CreateTime"" DATETIME NOT NULL,
  PRIMARY KEY (""ID"")
);",
            @"CREATE TABLE ""DataModelField"" (
  ""ID"" TEXT NOT NULL,
  ""Name"" TEXT NOT NULL,
  ""DataModelID"" TEXT NOT NULL,
  ""DataType"" INTEGER NOT NULL,
  ""Data"" TEXT,
  ""Description"" TEXT,
  ""CreateTime"" DATETIME NOT NULL,
  PRIMARY KEY (""ID"")
);",
            @"CREATE TABLE ""FlowTemplate"" (
  ""ID"" TEXT NOT NULL,
  ""Name"" TEXT NOT NULL,
  ""DataModelID"" TEXT NOT NULL,
  ""CreateTime"" DATETIME NOT NULL,
  PRIMARY KEY (""ID"")
);",
            @"CREATE TABLE ""FlowUser"" (
  ""ID"" TEXT NOT NULL,
  ""FlowTemplateID"" TEXT NOT NULL,
  ""FlowID"" TEXT NOT NULL,
  ""FlowRecordID"" TEXT NOT NULL,
  ""UserID"" TEXT NOT NULL,
  ""CreateTime"" DATETIME NOT NULL,
  PRIMARY KEY (""ID"")
);",
            @"CREATE TABLE ""Node"" (
  ""ID"" TEXT NOT NULL,
  ""Name"" TEXT NOT NULL,
  ""StepID"" TEXT NOT NULL,
  ""HandleType"" INTEGER NOT NULL,
  ""RunConditionExpression"" TEXT,
  ""Data"" TEXT,
  ""HandleData"" TEXT,
  ""CreateTime"" DATETIME NOT NULL,
  PRIMARY KEY (""ID"")
);",
            @"CREATE TABLE ""Step"" (
  ""ID"" TEXT NOT NULL,
  ""Name"" TEXT NOT NULL,
  ""FlowTemplateID"" TEXT NOT NULL,
  ""NextID"" TEXT,
  ""UpID"" TEXT,
  ""CreateTime"" DATETIME NOT NULL,
  PRIMARY KEY (""ID"")
);",
            @"CREATE TABLE ""User"" (
  ""ID"" TEXT NOT NULL,
  ""Name"" TEXT NOT NULL,
  ""CreateTime"" DATETIME NOT NULL,
  PRIMARY KEY (""ID"")
);",
        };
    }
}
