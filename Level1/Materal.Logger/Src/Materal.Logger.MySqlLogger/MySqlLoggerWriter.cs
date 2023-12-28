namespace Materal.Logger.MySqlLogger
{
    /// <summary>
    /// MySql日志写入器
    /// </summary>
    public class MySqlLoggerWriter(MySqlLoggerTargetConfig targetConfig) : BatchLoggerWriter<MySqlLoggerWriterModel, MySqlLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(MySqlLoggerWriterModel[] models)
        {
            IGrouping<string, MySqlLoggerWriterModel>[] groupDatas = models.GroupBy(m => m.ConnectionString).ToArray();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    MySqlRepository repository = new(item.Key);
                    repository.Inserts([.. item]);
                }
                catch (Exception exception)
                {
                    LoggerHost.LoggerLog?.LogError($"日志记录到MySql[{item.Key}]失败：", exception);
                }
            });
            await Task.CompletedTask;
        }
    }
}
