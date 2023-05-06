using Materal.TTA.SqlServerADONETRepository;

namespace Materal.BusinessFlow.SqlServerRepository.Migrates
{
    public class Init : SqlServerMigration
    {
        public override int Index => 0;

        public override List<string> GetUpTSQL() => new()
        {
            @"CREATE TABLE [dbo].[DataModel](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[Description] [varchar](max) NULL,
	[CreateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DataModel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
            @"CREATE TABLE [dbo].[DataModelField](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[DataModelID] [uniqueidentifier] NOT NULL,
	[DataType] [tinyint] NOT NULL,
	[Data] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[CreateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DataModelField] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
            @"CREATE TABLE [dbo].[FlowTemplate](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[DataModelID] [uniqueidentifier] NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_FlowTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]",
            @"CREATE TABLE [dbo].[FlowUser](
	[ID] [uniqueidentifier] NOT NULL,
	[FlowTemplateID] [uniqueidentifier] NOT NULL,
	[FlowID] [uniqueidentifier] NOT NULL,
	[FlowRecordID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_FlowUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]",
            @"CREATE TABLE [dbo].[Node](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[StepID] [uniqueidentifier] NOT NULL,
	[HandleType] [tinyint] NOT NULL,
	[RunConditionExpression] [varchar](max) NULL,
	[Data] [varchar](max) NULL,
	[HandleData] [varchar](max) NULL,
	[CreateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Node] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]",
            @"CREATE TABLE [dbo].[Step](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[FlowTemplateID] [uniqueidentifier] NOT NULL,
	[NextID] [uniqueidentifier] NULL,
	[UpID] [uniqueidentifier] NULL,
	[CreateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Step] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]",
            @"CREATE TABLE [dbo].[User](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](40) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]",
        };
    }
}
