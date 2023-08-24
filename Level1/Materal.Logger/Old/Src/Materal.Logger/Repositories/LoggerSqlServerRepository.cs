using Materal.Logger.DBHelper;
using Materal.Logger.Models;

namespace Materal.Logger.Repositories
{
	/// <summary>
	/// 日志SqlServer仓储
	/// </summary>
	public class LoggerSqlServerRepository : SqlServerBaseRepository<LogModel>
    {
		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="connectionString"></param>
        public LoggerSqlServerRepository(string connectionString) : base(connectionString)
        {
        }
		/// <summary>
		/// 表名
		/// </summary>
        protected override string TableName => "MateralLogs";
		/// <summary>
		/// 获得创建表语句
		/// </summary>
		/// <returns></returns>
        protected override string GetCreateTableTSQL()
        {
            return $@"CREATE TABLE [{TableName}](
	[{nameof(LogModel.ID)}] [uniqueidentifier] NOT NULL,
	[{nameof(LogModel.CreateTime)}] [datetime2](7) NOT NULL,
	[{nameof(LogModel.Level)}] [nvarchar](50) NOT NULL,
	[{nameof(LogModel.ProgressID)}] [nvarchar](20) NOT NULL,
	[{nameof(LogModel.ThreadID)}] [nvarchar](20) NOT NULL,
	[{nameof(LogModel.Scope)}] [nvarchar](100) NOT NULL,
	[{nameof(LogModel.MachineName)}] [nvarchar](100) NULL,
	[{nameof(LogModel.CategoryName)}] [nvarchar](100) NULL,
	[{nameof(LogModel.Application)}] [nvarchar](100) NOT NULL,
	[{nameof(LogModel.Message)}] [nvarchar](max) NOT NULL,
	[{nameof(LogModel.Error)}] [nvarchar](max) NULL,
	[{nameof(LogModel.CustomInfo)}] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MateralLogs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
";
        }
    }
}
