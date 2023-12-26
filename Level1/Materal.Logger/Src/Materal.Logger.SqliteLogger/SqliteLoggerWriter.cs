namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqlite日志写入器
    /// </summary>
    public class SqliteLoggerWriter(SqliteLoggerTargetConfig targetConfig) : BatchLoggerWriter<SqliteLoggerWriterModel, SqliteLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(SqliteLoggerWriterModel[] models)
        {
            IGrouping<string, SqliteLoggerWriterModel>[] groupDatas = models.GroupBy(m => m.Path).ToArray();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    SqliteRepository repository = new(item.Key);
                    repository.Inserts([.. item]);
                }
                catch (Exception exception)
                {
                    LoggerHost.LoggerLog?.LogError($"日志记录到Sqlite[{item.Key}]失败：", exception);
                }
            });
            await Task.CompletedTask;
        }
    }
}
