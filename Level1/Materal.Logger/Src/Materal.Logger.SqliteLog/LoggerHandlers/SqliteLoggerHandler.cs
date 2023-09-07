﻿using Materal.Logger.LoggerHandlers.Models;
using Materal.Logger.Models;
using Materal.Logger.Repositories;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// Sqlite日志处理器
    /// </summary>
    public class SqliteLoggerHandler : BufferLoggerHandler<SqliteLoggerHandlerModel, SqliteLoggerTargetConfigModel>
    {
        /// <summary>
        /// 处理合格的数据
        /// </summary>
        /// <param name="datas"></param>
        protected override void HandlerOKData(SqliteLoggerHandlerModel[] datas)
        {
            IGrouping<string, SqliteLoggerHandlerModel>[] groupDatas = datas.GroupBy(m => m.Path).ToArray();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    LoggerSqliteRepository repository = new(item.Key);
                    repository.Init();
                    LogModel[] datas = item.Select(m => m.LogModel).ToArray();
                    repository.Inserts(datas);
                }
                catch (Exception exception)
                {
                    LoggerLog.LogWarning($"日志记录到Sqlite[{item.Key}]失败：", exception);
                }
            });
        }
    }
}
