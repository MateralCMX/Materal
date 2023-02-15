﻿using Materal.ConvertHelper;
using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Logger.Repositories;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.LoggerHandlers
{
    public class SqliteLoggerHandler : BufferLoggerHandler<DBDataModel>
    {
        public SqliteLoggerHandler(MateralLoggerRuleConfigModel rule, MateralLoggerTargetConfigModel target) : base(rule, target)
        {
        }
        public override void Handler(LogLevel logLevel, string message, string? categoryName, MateralLoggerScope? scope, DateTime dateTime, Exception? exception, string threadID)
        {
            if (string.IsNullOrWhiteSpace(Target.Path)) return;
            MateralLogModel data = GetMateralLogModel(logLevel, message, categoryName, scope, dateTime, exception, threadID);
            string path = FormatPath(Target.Path, logLevel, categoryName, scope, dateTime, threadID);
            PushData(new DBDataModel
            {
                Model = data,
                DBConnectionString = path
            });
            SendMessage(data.ToJson(), logLevel);
        }
        protected override void SaveData(DBDataModel[] datas)
        {
            IGrouping<string, DBDataModel>[] groupDatas = datas.GroupBy(m => m.DBConnectionString).ToArray();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    MateralLoggerSqliteRepository repository = new(item.Key);
                    repository.Init();
                    MateralLogModel[] datas = item.Select(m => m.Model).ToArray();
                    repository.Inserts(datas);
                }
                catch (Exception exception)
                {
                    MateralLoggerLog.LogError("保存Sqlite数据库失败：", exception);
                }
            });
        }
    }
}