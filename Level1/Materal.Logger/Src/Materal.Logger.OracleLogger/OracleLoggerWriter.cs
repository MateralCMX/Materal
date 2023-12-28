namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle日志写入器
    /// </summary>
    public class OracleLoggerWriter(OracleLoggerTargetConfig targetConfig) : BatchLoggerWriter<OracleLoggerWriterModel, OracleLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(OracleLoggerWriterModel[] models)
        {
            IGrouping<string, OracleLoggerWriterModel>[] groupDatas = models.GroupBy(m => m.ConnectionString).ToArray();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    OracleRepository repository = new(item.Key);
                    repository.Inserts([.. item]);
                }
                catch (Exception exception)
                {
                    LoggerHost.LoggerLog?.LogError($"日志记录到Oracle[{item.Key}]失败：", exception);
                }
            });
            await Task.CompletedTask;
        }
    }
}
