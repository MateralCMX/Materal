using Materal.Logger.BatchLogger;
using Materal.Logger.DBLogger.Repositories;

namespace Materal.Logger.DBLogger
{
    /// <summary>
    /// 数据库日志写入器
    /// </summary>
    /// <typeparam name="TLoggerTargetOptions"></typeparam>
    /// <typeparam name="TDBFiled"></typeparam>
    /// <typeparam name="TDBLog"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public abstract class DBLoggerWriter<TLoggerTargetOptions, TDBFiled, TDBLog, TRepository>(IOptionsMonitor<LoggerOptions> options, ILoggerInfo loggerInfo) : BatchLoggerWriter<TLoggerTargetOptions>(options, loggerInfo)
        where TLoggerTargetOptions : DBLoggerTargetOptions<TDBFiled>
        where TDBFiled : IDBFiled, new()
        where TDBLog : DBLog<TLoggerTargetOptions, TDBFiled>
        where TRepository : BaseRepository<TDBLog, TLoggerTargetOptions, TDBFiled>
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public abstract string DBName { get; }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="batchLogs"></param>
        /// <returns></returns>
        public override async Task LogAsync(BatchLog<TLoggerTargetOptions>[] batchLogs)
        {
            Type type = typeof(TDBLog);
            TDBLog[] dbLogs = batchLogs.Select(m => type.Instantiation<TDBLog>([m, Options.CurrentValue])).ToArray();
            IGrouping<string, TDBLog>[] groupDatas = dbLogs.GroupBy(m => m.ConnectionString).ToArray();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    TRepository repository = typeof(TRepository).Instantiation<TRepository>([item.Key]);
                    repository.Inserts([.. item]);
                }
                catch (Exception exception)
                {
                    LoggerInfo.LogError($"日志记录到{DBName}[{item.Key}]失败：", exception);
                }
            });
            await Task.CompletedTask;
        }
    }
}
