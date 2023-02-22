using Materal.ConvertHelper;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Logger.Repositories;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// Sqilite日志处理器
    /// </summary>
    public class SqliteLoggerHandler : BufferLoggerHandler<DBDataModel>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        public SqliteLoggerHandler(LoggerRuleConfigModel rule, LoggerTargetConfigModel target) : base(rule, target)
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
            if (Target.Path == null || string.IsNullOrWhiteSpace(Target.Path)) return;
            LogModel data = GetMateralLogModel(logLevel, message, categoryName, scope, dateTime, exception, threadID);
            string path = FormatPath(Target.Path, logLevel, categoryName, scope, dateTime, threadID);
            PushData(new DBDataModel
            {
                Model = data,
                DBConnectionString = path
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
                    LoggerSqliteRepository repository = new(item.Key);
                    repository.Init();
                    LogModel[] datas = item.Select(m => m.Model).ToArray();
                    repository.Inserts(datas);
                }
                catch (Exception exception)
                {
                    LoggerLog.LogError("保存Sqlite数据库失败：", exception);
                }
            });
        }
    }
}
