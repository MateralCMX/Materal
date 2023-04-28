using Materal.TTA.SqlServerADONETRepository;

namespace Materal.TTA.Demo.SqlServerADONETRepository.Migrations
{
    public sealed class Init : SqlServerMigration
    {
        public override int Index => 0;

        public override List<string> GetUpTSQL()
        {
            List<string> tsqls = new()
            {
                @"
CREATE TABLE [TestDomain](
	[ID] [uniqueidentifier] NOT NULL,
	[StringType] [varchar](max) NULL,
	[IntType] [int] NULL,
	[ByteType] [tinyint] NULL,
	[DecimalType] [decimal](18, 2) NULL,
	[EnumType] [tinyint] NULL,
	[DateTimeType] [datetime2](7) NULL,
 CONSTRAINT [PK_TestDomain] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]"
            };
            return tsqls;
        }
    }
}
