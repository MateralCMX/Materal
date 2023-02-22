using Materal.ConvertHelper;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Logger.Repositories;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// SqlServer日志处理器
    /// </summary>
    public class SqlServerLoggerHandler : BufferLoggerHandler<DBDataModel>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        public SqlServerLoggerHandler(LoggerRuleConfigModel rule, LoggerTargetConfigModel target) : base(rule, target)
        {
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="scope"></param>
        /// <param name="dateTime"></param>
        /// <param name="exception"></param>
        /// <param name="threadID"></param>
        public override void Handler(LogLevel logLevel, string message, string? categoryName, LoggerScope? scope, DateTime dateTime, Exception? exception, string threadID)
        {
            if (Target.ConnectionString == null || string.IsNullOrWhiteSpace(Target.ConnectionString)) return;
            LogModel data = GetMateralLogModel(logLevel, message, categoryName, scope, dateTime, exception, threadID);
            PushData(new DBDataModel
            {
                Model = data,
                DBConnectionString = Target.ConnectionString
            });
            SendMessage(data.ToJson(), logLevel);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="datas"></param>
        protected override void SaveData(DBDataModel[] datas)
        {
            IGrouping<string, DBDataModel>[] groupDatas = datas.GroupBy(m => m.DBConnectionString).ToArray();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    LoggerSqlServerRepository repository = new(item.Key);
                    repository.Init();
                    LogModel[] datas = item.Select(m => m.Model).ToArray();
                    repository.Inserts(datas);
                }
                catch (Exception exception)
                {
                    LoggerLog.LogError("保存SqlServer数据库失败：", exception);
                }
            });
        }
    }
}
