namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle日志写入器模型
    /// </summary>
    public class OracleLoggerWriterModel : BatchLoggerWriterModel
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public List<OracleDBFiled> Fileds { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public OracleLoggerWriterModel(LoggerWriterModel model, OracleLoggerTargetConfig target) : base(model)
        {
            ConnectionString = LoggerWriterHelper.FormatPath(target.ConnectionString, model);
            TableName = LoggerWriterHelper.FormatText(target.TableName, model);
            Fileds = target.Fileds.Count <= 0
                ? OracleLoggerTargetConfig.DefaultFileds.Select(m => GetNewSqliteDBFiled(m, model)).ToList()
                : target.Fileds.Select(m => GetNewSqliteDBFiled(m, model)).ToList();
        }
        /// <summary>
        /// 获得新的Sqlite数据库字段
        /// </summary>
        /// <param name="filed"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private OracleDBFiled GetNewSqliteDBFiled(OracleDBFiled filed, LoggerWriterModel model)
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
