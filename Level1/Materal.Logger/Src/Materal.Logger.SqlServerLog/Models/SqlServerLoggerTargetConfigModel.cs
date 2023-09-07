using Materal.Logger.LoggerHandlers;

namespace Materal.Logger.Models
{
    /// <summary>
    /// SqlServer日志目标配置模型
    /// </summary>
    public class SqlServerLoggerTargetConfigModel : LoggerTargetConfigModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "SqlServer";
        /// <summary>
        /// 获得日志处理器
        /// </summary>
        public override ILoggerHandler GetLoggerHandler() => new SqlServerLoggerHandler();
        private string _connectionString = "Data Source=.;Initial Catalog=LogDB;Persist Security Info=True;User ID=sa;Password=123456";
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value)) throw new LoggerException("连接字符串不能为空");
                _connectionString = value;
            }
        }
    }
}
