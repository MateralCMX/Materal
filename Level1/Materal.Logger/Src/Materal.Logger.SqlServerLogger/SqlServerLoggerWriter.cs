namespace Materal.Logger.SqlServerLogger
{
    /// <summary>
    /// SqlServer日志写入器
    /// </summary>
    public class SqlServerLoggerWriter(SqlServerLoggerTargetConfig targetConfig) : BatchLoggerWriter<SqlServerLoggerWriterModel, SqlServerLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(SqlServerLoggerWriterModel[] models)
        {
            IGrouping<string, SqlServerLoggerWriterModel>[] groupDatas = models.GroupBy(m => m.ConnectionString).ToArray();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    SqlServerRepository repository = new(item.Key);
                    repository.Inserts([.. item]);
                }
                catch (Exception exception)
                {
                    LoggerHost.LoggerLog?.LogError($"日志记录到SqlServer[{item.Key}]失败：", exception);
                }
            });
            await Task.CompletedTask;
        }
    }
}
