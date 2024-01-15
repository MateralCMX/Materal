namespace Materal.Logger.LoggerWriter
{
    /// <summary>
    /// 数据库日志写入器模型
    /// </summary>
    public abstract class DBLoggerWriterModel<TLoggerWriter, TTargetConfig, TDBFiled>(LoggerWriterModel model, TTargetConfig target) : BatchLoggerWriterModel(model)
        where TTargetConfig : DBLoggerTargetConfig<TLoggerWriter, TDBFiled>
        where TLoggerWriter : ILoggerWriter
        where TDBFiled : IDBFiled, new()
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; } = LoggerWriterHelper.FormatPath(target.ConnectionString, model);
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; } = LoggerWriterHelper.FormatText(target.TableName, model);
        /// <summary>
        /// 字段
        /// </summary>
        public List<IDBFiled> Fileds { get; set; } = target.Fileds.Count <= 0
                ? target.DefaultFileds.Select(m => m.GetNewDBFiled(model)).ToList()
                : target.Fileds.Select(m => m.GetNewDBFiled(model)).ToList();
    }
}
