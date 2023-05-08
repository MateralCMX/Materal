using Materal.TTA.SqlServerADONETRepository;

namespace Materal.Oscillator.DRSqlServerADONETRepository.Migrates
{
    /// <summary>
    /// 初始化数据库
    /// </summary>
    public class Init : SqlServerMigration
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
            @"CREATE TABLE [dbo].[Flow](
	[ID] [uniqueidentifier] NOT NULL,
	[JobKey] [nvarchar](max) NOT NULL,
	[ScheduleData] [nvarchar](max) NOT NULL,
	[ScheduleID] [uniqueidentifier] NOT NULL,
	[WorkID] [uniqueidentifier] NULL,
	[WorkResults] [nvarchar](max) NULL,
	[AuthenticationCode] [uniqueidentifier] NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[UpdateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Flow] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
        };
    }
}
