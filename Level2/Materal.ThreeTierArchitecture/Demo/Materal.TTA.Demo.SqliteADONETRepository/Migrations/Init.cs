using Materal.TTA.SqliteADONETRepository;

namespace Materal.TTA.Demo.SqliteADONETRepository.Migrations
{
    public sealed class Init : SqliteMigration
    {
        public override int Index => 0;

        public override List<string> GetUpTSQL()
        {
            List<string> tsqls = new()
            {
                @"
CREATE TABLE ""TestDomain"" (
  ""ID"" TEXT NOT NULL,
  ""StringType"" TEXT,
  ""IntType"" INTEGER,
  ""ByteType"" INTEGER,
  ""DecimalType"" INTEGER,
  ""EnumType"" INTEGER,
  ""DateTimeType"" DATETIME,
  PRIMARY KEY (""ID"")
);"
            };
            return tsqls;
        }
    }
}
