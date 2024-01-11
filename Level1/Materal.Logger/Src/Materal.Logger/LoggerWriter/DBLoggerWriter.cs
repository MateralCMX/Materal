using Materal.Logger.Repositories;

namespace Materal.Logger.LoggerWriter
{
    /// <summary>
    /// 数据库日志写入器
    /// </summary>
    /// <typeparam name="TLoggerWriter"></typeparam>
    /// <typeparam name="TLoggerWriterModel"></typeparam>
    /// <typeparam name="TTargetConfig"></typeparam>
    /// <typeparam name="TDBFiled"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    /// <param name="targetConfig"></param>
    public abstract class DBLoggerWriter<TLoggerWriter, TLoggerWriterModel, TTargetConfig, TDBFiled, TRepository>(TTargetConfig targetConfig) : BatchLoggerWriter<TLoggerWriterModel, TTargetConfig>(targetConfig), ILoggerWriter
        where TTargetConfig : DBLoggerTargetConfig<TLoggerWriter, TDBFiled>
        where TLoggerWriterModel : DBLoggerWriterModel<TLoggerWriter, TTargetConfig, TDBFiled>
        where TLoggerWriter : ILoggerWriter
        where TDBFiled : IDBFiled, new()
        where TRepository : BaseRepository<TLoggerWriterModel>
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public abstract string DBName { get; }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(TLoggerWriterModel[] models)
        {
            IGrouping<string, TLoggerWriterModel>[] groupDatas = models.GroupBy(m => m.ConnectionString).ToArray();
            Parallel.ForEach(groupDatas, item =>
            {
                try
                {
                    TRepository repository = typeof(TRepository).Instantiation<TRepository>(item.Key);
                    repository.Inserts([.. item]);
                }
                catch (Exception exception)
                {
                    LoggerHost.LoggerLog?.LogError($"日志记录到{DBName}[{item.Key}]失败：", exception);
                }
            });
            await Task.CompletedTask;
        }
    }
}
