using Materal.TTA.SqlServerADONETRepository;

namespace Materal.Oscillator.SqlServerRepository.Migrates
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
            @"CREATE TABLE [dbo].[Answer](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ScheduleID] [uniqueidentifier] NOT NULL,
	[WorkEvent] [nvarchar](40) NOT NULL,
	[Index] [int] NOT NULL,
	[Description] [nvarchar](400) NULL,
	[AnswerType] [nvarchar](100) NOT NULL,
	[AnswerData] [nvarchar](4000) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[UpdateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]",
            @"CREATE TABLE [dbo].[Plan](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ScheduleID] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](400) NULL,
	[PlanTriggerType] [nvarchar](100) NOT NULL,
	[PlanTriggerData] [nvarchar](4000) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[UpdateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Plan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]",
            @"CREATE TABLE [dbo].[Schedule](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Territory] [nvarchar](100) NOT NULL,
	[Enable] [bit] NOT NULL,
	[Description] [nvarchar](400) NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[UpdateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]",
            @"CREATE TABLE [dbo].[ScheduleWork](
	[ID] [uniqueidentifier] NOT NULL,
	[ScheduleID] [uniqueidentifier] NOT NULL,
	[WorkID] [uniqueidentifier] NOT NULL,
	[Index] [int] NOT NULL,
	[SuccessEvent] [nvarchar](100) NOT NULL,
	[FailEvent] [nvarchar](100) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[UpdateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ScheduleWork] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]",
            @"CREATE TABLE [dbo].[Work](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[WorkType] [nvarchar](100) NOT NULL,
	[WorkData] [nvarchar](4000) NOT NULL,
	[Description] [nvarchar](400) NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[UpdateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Work] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]",
        };
    }
}
