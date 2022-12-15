using Materal.Logger.DBHelper;
using Materal.Logger.Models;
using System.Data.SqlClient;

namespace Materal.Logger.Repositories
{
    public class MateralLoggerSqlServerRepository : SqlServerBaseRepository<MateralLogModel>
    {
        public MateralLoggerSqlServerRepository(string connectionString) : base(connectionString)
        {
        }
        protected override string TableName => "MateralLogs";
        protected override string GetCreateTableTSQL()
        {
            return $@"CREATE TABLE [{TableName}](
	[{nameof(MateralLogModel.ID)}] [uniqueidentifier] NOT NULL,
	[{nameof(MateralLogModel.CreateTime)}] [datetime2](7) NOT NULL,
	[{nameof(MateralLogModel.Level)}] [nvarchar](50) NOT NULL,
	[{nameof(MateralLogModel.ProgressID)}] [nvarchar](20) NOT NULL,
	[{nameof(MateralLogModel.ThreadID)}] [nvarchar](20) NOT NULL,
	[{nameof(MateralLogModel.Scope)}] [nvarchar](100) NOT NULL,
	[{nameof(MateralLogModel.MachineName)}] [nvarchar](100) NULL,
	[{nameof(MateralLogModel.CategoryName)}] [nvarchar](100) NULL,
	[{nameof(MateralLogModel.Application)}] [nvarchar](100) NOT NULL,
	[{nameof(MateralLogModel.Message)}] [nvarchar](max) NOT NULL,
	[{nameof(MateralLogModel.Error)}] [nvarchar](max) NULL,
	[{nameof(MateralLogModel.CustomInfo)}] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MateralLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
";
        }
    }
}
