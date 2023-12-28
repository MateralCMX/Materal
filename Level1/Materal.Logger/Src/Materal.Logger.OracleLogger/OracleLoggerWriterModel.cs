namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle日志写入器模型
    /// </summary>
    public class OracleLoggerWriterModel(LoggerWriterModel model, OracleLoggerTargetConfig target) : BatchLoggerWriterModel(model)
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
        public List<OracleDBFiled> Fileds { get; set; } = target.Fileds.Count <= 0
                ? OracleLoggerTargetConfig.DefaultFileds.Select(m => GetNewSqliteDBFiled(m, model)).ToList()
                : target.Fileds.Select(m => GetNewSqliteDBFiled(m, model)).ToList();
        /// <summary>
        /// 获得新的Sqlite数据库字段
        /// </summary>
        /// <param name="filed"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private static OracleDBFiled GetNewSqliteDBFiled(OracleDBFiled filed, LoggerWriterModel model)
        {
            OracleDBFiled result = new()
            {
                Name = filed.Name,
                Type = filed.Type,
                PK = filed.PK,
                Index = filed.Index,
                IsNull = filed.IsNull
            };
            if (filed.Value is not null && !string.IsNullOrWhiteSpace(filed.Value))
            {
                result.Value = LoggerWriterHelper.FormatMessage(filed.Value, model);
            }
            return result;
        }
    }
}
